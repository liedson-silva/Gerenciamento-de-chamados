export class ManageTicketsController {
  constructor(pool, sql) {
    this.pool = pool;
    this.sql = sql;
  }

  // Criar chamado
  async createTicket(req, res) {
    const {
      Titulo, PrioridadeChamado, Descricao, DataChamado,
      StatusChamado, Categoria, FK_IdUsuario, PessoasAfetadas,
      ImpedeTrabalho, OcorreuAnteriormente
    } = req.body;

    if (!Descricao || !DataChamado || !StatusChamado || !Categoria || !FK_IdUsuario) {
      return res.status(400).json({ success: false, message: "Campos obrigatórios faltando" });
    }

    try {
      const result = await this.pool.request()
        .input("Titulo", this.sql.VarChar(150), Titulo)
        .input("PrioridadeChamado", this.sql.VarChar(20), PrioridadeChamado)
        .input("Descricao", this.sql.VarChar(500), Descricao)
        .input("DataChamado", this.sql.Date, DataChamado)
        .input("StatusChamado", this.sql.VarChar(12), StatusChamado)
        .input("Categoria", this.sql.VarChar(16), Categoria)
        .input("FK_IdUsuario", this.sql.Int, FK_IdUsuario)
        .input("PessoasAfetadas", this.sql.VarChar(50), PessoasAfetadas)
        .input("ImpedeTrabalho", this.sql.VarChar(12), ImpedeTrabalho)
        .input("OcorreuAnteriormente", this.sql.VarChar(7), OcorreuAnteriormente)
        .query(`
          INSERT INTO Chamado
            (Titulo, PrioridadeChamado, Descricao, DataChamado, StatusChamado, Categoria,
             FK_IdUsuario, PessoasAfetadas, ImpedeTrabalho, OcorreuAnteriormente)
          OUTPUT INSERTED.*
          VALUES
            (@Titulo, @PrioridadeChamado, @Descricao, @DataChamado, @StatusChamado, @Categoria,
             @FK_IdUsuario, @PessoasAfetadas, @ImpedeTrabalho, @OcorreuAnteriormente)
        `);

      const newTicket = result.recordset[0];
      return res.status(201).json({ success: true, ticket: newTicket });

    } catch (err) {
      console.error("Erro ao criar chamado:", err);
      return res.status(500).json({ success: false, message: "Erro ao criar chamado" });
    }
  }

  // Buscar todos os chamados
  async getAllTickets(req, res) {
    try {
      const result = await this.pool.request()
        .query("SELECT * FROM Chamado");

      return res.status(200).json({ success: true, tickets: result.recordset });
    } catch (err) {
      console.error("Erro ao buscar chamados:", err);
      return res.status(500).json({ success: false, message: "Erro ao buscar chamados" });
    }
  }

  // Buscar chamado por ID
  async getTicketById(req, res) {
    const ticketId = parseInt(req.params.id, 10);
    if (isNaN(ticketId)) {
      return res.status(400).json({ success: false, message: "ID de chamado inválido" });
    }

    try {
      const result = await this.pool.request()
        .input("id", this.sql.Int, ticketId)
        .query("SELECT * FROM Chamado WHERE IdChamado = @id");

      if (result.recordset.length === 0) {
        return res.status(404).json({ success: false, message: "Chamado não encontrado" });
      }

      return res.status(200).json({ success: true, ticket: result.recordset[0] });

    } catch (err) {
      console.error("Erro ao buscar chamado:", err);
      return res.status(500).json({ success: false, message: "Erro ao buscar chamado" });
    }
  }

  // Atualizar chamado
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
}