import crypto from "crypto"
import Gemini from "../config/connectIA.js";

export class LoginController {
    constructor(pool, sql) {
        this.pool = pool
        this.sql = sql
    }

    validarSenha(senha, hashSalvo) {
        const hashBytes = Buffer.from(hashSalvo, "base64");
        const salt = hashBytes.slice(0, 16)
        const hashOriginal = hashBytes.slice(16, 36)

        const hash = crypto.pbkdf2Sync(senha, salt, 10000, 20, "sha1")
        return hash.equals(hashOriginal)
    }

    async login(req, res) {
        const { login, password } = req.body

        if (!login || !password) {
            return res.status(400).json({ success: false, message: "Login e senha obrigatórios" })
        }

        try {
            const result = await this.pool.request()
                .input("login", this.sql.VarChar, login)
                .query("SELECT * FROM Usuario WHERE Login = @login")

            if (result.recordset.length === 0) {
                return res.status(401).json({ success: false, message: "Usuário não encontrado" });
            }

            const user = result.recordset[0];
            const validPassword = this.validarSenha(password, user.Senha)

            if (!validPassword) {
                return res.status(401).json({ success: false, message: "Senha incorreta" })
            }

            res.json({
                success: true, user: user
            })

        } catch (err) {
            console.error("Erro no login:", err);
            res.status(500).json({ success: false, message: "Erro no servidor" })
        }
    }
}

export class TicketsController {
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
            const result = await this.pool.request()
                .input("title", this.sql.VarChar(30), title)
                .input("priority", this.sql.VarChar(7), "Análise")
                .input("description", this.sql.VarChar(500), description)
                .input("ticketDate", this.sql.Date, new Date())
                .input("ticketStatus", this.sql.VarChar(12), "Pendente")
                .input("category", this.sql.VarChar(16), category)
                .input("userId", this.sql.Int, userId)
                .input("affectedPeople", this.sql.VarChar(15), affectedPeople)
                .input("stopWork", this.sql.VarChar(12), stopWork)
                .input("happenedBefore", this.sql.VarChar(7), happenedBefore)
                .query("INSERT INTO Chamado (Titulo, PrioridadeChamado, Descricao, DataChamado, StatusChamado, Categoria, FK_IdUsuario, PessoasAfetadas, ImpedeTrabalho, OcorreuAnteriormente) OUTPUT INSERTED.* VALUES (@title, @priority, @description, @ticketDate, @ticketStatus, @category, @userId, @affectedPeople, @stopWork, @happenedBefore)")

            const newTicket = result.recordset[0];
            this.updatePriorityByAI(newTicket.IdChamado, description, affectedPeople,
                stopWork, happenedBefore)

            res.json({ success: true, ticket: newTicket })
        } catch (err) {
            console.error(err)
            res.status(500).json({ success: false, message: "Erro ao criar chamado" })
        }
    }

    async updatePriorityByAI(ticketId, description, affectedPeople, stopWork, happenedBefore) {
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

            await this.pool.request()
                .input("priority", this.sql.VarChar(20), priority)
                .input("idChamado", this.sql.Int, ticketId)
                .query("UPDATE Chamado SET PrioridadeChamado = @priority WHERE IdChamado = @idChamado");
        } catch (error) {
            console.error(`Erro ao atualizar a prioridade do chamado ${ticketId} pela IA:`, error);
        }
    }

    async getTicket(req, res) {
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
} 
