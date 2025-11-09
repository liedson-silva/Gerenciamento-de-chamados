import crypto from "crypto";

export class UserController {
  constructor(pool, sql) {
    this.pool = pool;
    this.sql = sql;
  }

  createHash(password) {
    const salt = crypto.randomBytes(16);
    const hash = crypto.pbkdf2Sync(password, salt, 10000, 20, "sha1");
    return Buffer.concat([salt, hash]).toString("base64");
  }

  // Criar usuário
  async createUser(req, res) {
    const {
      name, cpf, rg, functionUser, sex, sector, date,
      email, password, login
    } = req.body;

    if (!name || !cpf || !rg || !functionUser || !sex || !sector || !date || !email || !password || !login) {
      return res.status(400).json({ success: false, message: "Campos obrigatórios faltando" });
    }

    const passwordhash = this.createHash(password);

    try {
      const result = await this.pool.request()
        .input("name", this.sql.VarChar(50), name)
        .input("cpf", this.sql.VarChar(14), cpf)
        .input("rg", this.sql.VarChar(12), rg)
        .input("functionUser", this.sql.VarChar(14), functionUser)
        .input("sex", this.sql.VarChar(12), sex)
        .input("sector", this.sql.VarChar(25), sector)
        .input("date", this.sql.Date, date)
        .input("email", this.sql.VarChar(35), email)
        .input("passwordhash", this.sql.VarChar(200), passwordhash)
        .input("login", this.sql.VarChar(50), login)
        .query(`INSERT INTO Usuario 
          (Nome, CPF, RG, FuncaoUsuario, Sexo, Setor, DataDeNascimento, Email, Senha, Login)
          OUTPUT INSERTED.*
          VALUES (@name, @cpf, @rg, @functionUser, @sex, @sector, @date, @email, @passwordhash, @login)`);

      const newUser = result.recordset[0];
      console.log("Usuário criado com sucesso.");
      return res.status(201).json({ success: true, user: newUser });

    } catch (err) {
      console.error("Erro ao criar usuário:", err);
      res.status(500).json({ success: false, message: "Erro ao criar usuário" });
    }
  }

  // Buscar todos os usuários
  async getAllUsers(req, res) {
    try {
      const result = await this.pool.request()
        .query(`SELECT * FROM Usuario`);

      return res.status(200).json({ success: true, users: result.recordset });
    } catch (err) {
      console.error("Erro ao buscar usuários:", err);
      return res.status(500).json({ success: false, message: "Erro ao buscar usuários" });
    }
  }

  // Atualizar usuário
  async updateUser(req, res) {
    const userId = parseInt(req.params.id, 10);

    if (isNaN(userId)) {
      return res.status(400).json({ success: false, message: "ID de usuário inválido" });
    }

    const {
      name, cpf, rg, functionUser, sex, sector, date,
      email, password, login
    } = req.body;

    if (!name || !cpf || !rg || !functionUser || !sex || !sector || !date || !email || !login) {
      return res.status(400).json({ success: false, message: "Campos obrigatórios faltando" });
    }

    try {
      const request = this.pool.request()
        .input("id", this.sql.Int, userId)
        .input("name", this.sql.VarChar(50), name)
        .input("cpf", this.sql.VarChar(14), cpf)
        .input("rg", this.sql.VarChar(12), rg)
        .input("functionUser", this.sql.VarChar(14), functionUser)
        .input("sex", this.sql.VarChar(12), sex)
        .input("sector", this.sql.VarChar(25), sector)
        .input("date", this.sql.Date, date)
        .input("email", this.sql.VarChar(35), email)
        .input("login", this.sql.VarChar(50), login);

      let query = `
        UPDATE Usuario SET 
          Nome = @name,
          CPF = @cpf,
          RG = @rg,
          FuncaoUsuario = @functionUser,
          Sexo = @sex,
          Setor = @sector,
          DataDeNascimento = @date,
          Email = @email,
          Login = @login`;

      // Se a senha foi enviada, atualiza também
      if (password && password.trim() !== "") {
        const passwordhash = this.createHash(password);
        request.input("passwordhash", this.sql.VarChar(200), passwordhash);
        query += `, Senha = @passwordhash`;
      }

      query += ` WHERE IdUsuario = @id`;

      await request.query(query);

      return res.status(200).json({ success: true, message: "Usuário atualizado com sucesso" });

    } catch (err) {
      console.error("Erro ao atualizar usuário:", err);
      return res.status(500).json({ success: false, message: "Erro ao atualizar usuário" });
    }
  }
}