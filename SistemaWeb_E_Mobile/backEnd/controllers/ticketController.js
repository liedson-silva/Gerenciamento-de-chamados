import Gemini from "../config/connectIA.js"
import sendEmail from "../config/sendEmail.js"

export class TicketsController {
    //'pool' (conexão com o banco de dados) e 'sql' (tipos de dados do SQL)
    constructor(pool, sql) {
        this.pool = pool
        this.sql = sql
    }

    async createTicket(req, res) {
        const { title, description, category, userId, affectedPeople, stopWork, happenedBefore } = req.body

        if (!title || !description || !category || !userId || !affectedPeople || !stopWork || !happenedBefore) {
            return res.status(400).json({ success: false, message: "Campos obrigatórios faltando" })
        }

        try {
            const dateforSQL = new Date().toLocaleDateString('en-CA',
                {
                    timeZone: 'America/Sao_Paulo',
                    year: 'numeric',
                    month: '2-digit',
                    day: '2-digit'
                }).replace(/\//g, '-')

            const result = await this.pool.request()
                .input("title", this.sql.VarChar(150), title)
                .input("priority", this.sql.VarChar(20), "Análise")
                .input("description", this.sql.VarChar(500), description)
                .input("ticketDate", this.sql.Date, dateforSQL)
                .input("ticketStatus", this.sql.VarChar(12), "Pendente")
                .input("category", this.sql.VarChar(16), category)
                .input("userId", this.sql.Int, userId)
                .input("affectedPeople", this.sql.VarChar(50), affectedPeople)
                .input("stopWork", this.sql.VarChar(12), stopWork)
                .input("happenedBefore", this.sql.VarChar(7), happenedBefore)
                .query("INSERT INTO Chamado (Titulo, PrioridadeChamado, Descricao, DataChamado, StatusChamado, Categoria, FK_IdUsuario, PessoasAfetadas, ImpedeTrabalho, OcorreuAnteriormente) OUTPUT INSERTED.* VALUES (@title, @priority, @description, @ticketDate, @ticketStatus, @category, @userId, @affectedPeople, @stopWork, @happenedBefore)")

            const newTicket = result.recordset[0]
            this.updatePriorityAndSolutionByAI(newTicket.IdChamado, dateforSQL, description, affectedPeople, stopWork, happenedBefore)

            const userResult = await this.pool.request()
                .input("userId", this.sql.Int, userId)
                .query("SELECT Nome, Email FROM Usuario WHERE IdUsuario = @userId")
            const user = userResult.recordset[0]
            const userEmail = user?.Email
            const userName = user?.Nome
            const d = newTicket.DataChamado;
            const dateTicket = `${String(d.getUTCDate()).padStart(2, '0')}/${String(d.getUTCMonth() + 1).padStart(2, '0')}/${d.getUTCFullYear()}`;
            sendEmail(userEmail, userName, newTicket.IdChamado, dateTicket, title, description, category, newTicket.PrioridadeChamado, newTicket.StatusChamado, affectedPeople, stopWork, happenedBefore)

            res.json({ success: true, ticket: newTicket })
        } catch (err) {
            console.error(err)
            res.status(500).json({ success: false, message: "Erro ao criar chamado" })
        }
    }

    async updatePriorityAndSolutionByAI(ticketId, date, description, affectedPeople, stopWork, happenedBefore) {
        try {
            const priority = await Gemini(`Você é um sistema automatizado de triagem de tickets de TI, especializado em definir a PRIORIDADE final de um chamado com base em critérios técnicos e de impacto.
            O NÍVEL DE PRIORIDADE deve ser classificado em um de três níveis: Baixa, Média ou Alta.
            Instruções:
            1. Analise os quatro campos de entrada fornecidos abaixo.
            2. Com base nas regras (descritas após os campos), retorne *apenas* o Nível de Prioridade final.
            [INÍCIO DOS DADOS]
            **Descrição do Chamado:** [${description}]
            **Pessoas Afetadas:** [${affectedPeople}]
            **Impede o Trabalho:** [${stopWork}]
            **Ocorrência Anterior:** [${happenedBefore}]
            [FIM DOS DADOS]
            Regras de Priorização:
            * **Alta:** O problema afeta a **Empresa inteira** *ou* **Meu setor** e **Impede o Trabalho (Sim)**. Também pode ser Alta se, mesmo afetando apenas você, o trabalho estiver *completamente* parado e for um problema recorrente (**Sim**).
            * **Média:** O problema afeta o **Meu setor** e **Não Impede o Trabalho ou Parcialmente IMPEDE** *ou* afeta **Somente eu** e **Impede o Trabalho (Sim)**. Problemas novos e críticos para um único usuário também se enquadram aqui.
            * **Baixa:** O problema afeta **Somente eu** e **Não Impede o Trabalho** *ou* se a descrição for um pedido de informação/melhoria (não um erro).`)

            const solution = await Gemini(`Você é um sistema de suporte de Nível 1, e eficiente, que pré-analisa chamados técnicos para a equipe de TI. Sua tarefa é analisar a descrição do problema de um usuário e gerar uma **PROPOSTA DE SOLUÇÃO** clara e concisa para a equipe técnica.
            A **PROPOSTA DE SOLUÇÃO** deve:
            1.  **Retornar apenas as ações sugeridas** de diagnóstico ou solução (ex: "Verificar logs do servidor X", "Reiniciar serviço Y", "Contatar o usuário para acesso remoto").
            2.  Conter no máximo 500 caracteres.
            3.  **NÃO** incluir cabeçalhos como "Conclusão Proposta" ou "Causa Raiz".
            **Descrição do Chamado:**
            ${description}
            **Ações Sugeridas:**`)

            const request = this.pool.request()
                .input("priority", this.sql.VarChar(20), priority)
                .input("solution", this.sql.VarChar(500), solution)
                .input("idChamado", this.sql.Int, ticketId)
                .input("solutionDate", this.sql.Date, date)
            await request.query(`
                UPDATE Chamado SET PrioridadeChamado = @priority WHERE IdChamado = @idChamado;
                `)
            await request.query(`
                INSERT INTO Historico (DataSolucao, Solucao, FK_IdChamado, Acao) 
                VALUES (@solutionDate, @solution, @idChamado, 'Nota Interna')
                `)

            console.log("Solução enviada e Prioridade atualizado pela IA")
        } catch (error) {
            console.error(`Erro ao enviar solução e atualizar a prioridade do chamado ${ticketId} pela IA:`, error)
        }
    }

    async getTicketById(req, res) {
        const { userId } = req.params

        try {
            const result = await this.pool.request()
                .input("userId", this.sql.Int, userId)
                .query("SELECT * FROM Chamado WHERE FK_idUsuario = @userId")

            if (result.recordset.length === 0) {
                return res.status(401).json({ success: false, message: "Nenhum chamado encontrado" })
            }

            res.json({ success: true, Tickets: result.recordset })
        } catch (err) {
            console.error(err)
            res.status(500).json({ success: false, message: "Erro ao buscar chamado" })
        }
    }

    async getAllTickets(req, res) {
        try {
            const result = await this.pool.request()
                .query("SELECT * FROM Chamado")

            if (result.recordset.length === 0) {
                return res.status(401).json({ success: false, message: "Nenhu, chamado encontrado" })
            }

            res.json({ success: true, Tickets: result.recordset })
        } catch (err) {
            console.error(err)
            res.status(500).json({ success: false, message: "Erro ao buscar chamados" })
        }
    }

    async updateTicket(req, res) {
        const ticketId = parseInt(req.params.id, 10);
        if (isNaN(ticketId)) {
            return res.status(400).json({ success: false, message: "ID de chamado inválido" });
        }

        const {
            Titulo, PrioridadeChamado, Descricao, DataChamado,
            StatusChamado, Categoria, FK_IdUsuario, PessoasAfetadas,
            ImpedeTrabalho, OcorreuAnteriormente
        } = req.body;

        if (!Descricao || !DataChamado || !StatusChamado || !Categoria || !FK_IdUsuario) {
            return res.status(400).json({ success: false, message: "Campos obrigatórios faltando" });
        }

        try {
            const request = this.pool.request()
                .input("id", this.sql.Int, ticketId)
                .input("Titulo", this.sql.VarChar(150), Titulo)
                .input("PrioridadeChamado", this.sql.VarChar(20), PrioridadeChamado)
                .input("Descricao", this.sql.VarChar(500), Descricao)
                .input("DataChamado", this.sql.Date, DataChamado)
                .input("StatusChamado", this.sql.VarChar(12), StatusChamado)
                .input("Categoria", this.sql.VarChar(16), Categoria)
                .input("FK_IdUsuario", this.sql.Int, FK_IdUsuario)
                .input("PessoasAfetadas", this.sql.VarChar(50), PessoasAfetadas)
                .input("ImpedeTrabalho", this.sql.VarChar(12), ImpedeTrabalho)
                .input("OcorreuAnteriormente", this.sql.VarChar(7), OcorreuAnteriormente);

            const query = `
        UPDATE Chamado SET
          Titulo = @Titulo,
          PrioridadeChamado = @PrioridadeChamado,
          Descricao = @Descricao,
          DataChamado = @DataChamado,
          StatusChamado = @StatusChamado,
          Categoria = @Categoria,
          FK_IdUsuario = @FK_IdUsuario,
          PessoasAfetadas = @PessoasAfetadas,
          ImpedeTrabalho = @ImpedeTrabalho,
          OcorreuAnteriormente = @OcorreuAnteriormente
        WHERE IdChamado = @id
      `;

            await request.query(query);

            return res.status(200).json({ success: true, message: "Chamado atualizado com sucesso" });

        } catch (err) {
            console.error("Erro ao atualizar chamado:", err);
            return res.status(500).json({ success: false, message: "Erro ao atualizar chamado" });
        }
    }

    async createReport(req, res) {
        const { startDate, endDate } = req.query

        if (!startDate || !endDate) {
            return res.status(400).json({ success: false, message: "Campos obrigatórios faltando!" })
        }

        try {
            const result = await this.pool.request()
                .input("startDate", this.sql.Date, startDate)
                .input("endDate", this.sql.Date, endDate)
                .query("SELECT * FROM Chamado WHERE DataChamado >= @startDate AND DataChamado <= @endDate")

            if (result.recordset.length === 0) {
                return res.json({ success: true, message: "Nenhum chamado encontrado no período especificado" })
            }

            res.json({ success: true, Tickets: result.recordset })
        } catch (err) {
            console.error(err)
            res.status(500).json({ success: false, message: "Erro ao buscar chamado" })
        }
    }

    async getSolution(req, res) {
        const { id } = req.params

        try {
            const result = await this.pool.request()
                .input("IdChamado", this.sql.Int, id)
                .query("SELECT Solucao, Acao FROM Historico WHERE FK_IdChamado = @IdChamado")

            res.json({ success: true, Tickets: result.recordset })
        } catch (err) {
            console.error(err)
            res.status(500).json({ success: false, message: "Erro ao buscar solução" })
        }
    }

    async Solution(req, res) {
        const { id, solution, status } = req.body

        try {

            const dateforSQL = new Date().toLocaleDateString('en-CA',
                {
                    timeZone: 'America/Sao_Paulo',
                    year: 'numeric',
                    month: '2-digit',
                    day: '2-digit'
                }).replace(/\//g, '-')

            const request = this.pool.request()
                .input("IdChamado", this.sql.Int, id)
                .input("solutionDate", this.sql.Date, dateforSQL)
                .input("solution", this.sql.VarChar(500), solution)
                .input("newStatus", this.sql.VarChar(50), status)
            await request.query(`
                INSERT INTO Historico (DataSolucao, Solucao, FK_IdChamado, Acao) 
                VALUES (@solutionDate, @solution, @idChamado, 'Solução Aplicada');
                UPDATE Chamado SET StatusChamado = @newStatus WHERE IdChamado = @IdChamado;
                `)

            res.json({ success: true, message: `Solução aplicada e Chamado resolvido.` })
        } catch (err) {
            console.error(err)
            res.status(500).json({ success: false, message: "Erro ao aplicar solução no BD" })
        }
    }
} 
