import crypto from "crypto"

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
                success: true, user: {
                    Login: user.Login, Email: user.Email,
                    Nome: user.Nome, Id: user.IdUsuario,
                }
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
        const { title, description, category, userId } = req.body

        if (!title || !description || !category || !userId) {
            return res.status(400).json({ success: false, message: "Campos obrigatórios faltando" })
        }

        try {
            const result = await this.pool.request()
                .input("title", this.sql.VarChar(30), title)
                .input("priority", this.sql.VarChar(7), "análise")
                .input("description", this.sql.VarChar(500), description)
                .input("ticketDate", this.sql.Date, new Date())
                .input("ticketStatus", this.sql.VarChar(12), "Aberto")
                .input("category", this.sql.VarChar(16), category)
                .input("userId", this.sql.Int, userId)
                .query("INSERT INTO Chamado (Titulo, PrioridadeChamado, Descricao, DataChamado, StatusChamado, Categoria, FK_IdUsuario) OUTPUT INSERTED.* VALUES (@title, @priority, @description, @ticketDate, @ticketStatus, @category, @userId)")

            res.json({ success: true, ticket: result.recordset[0] })
        } catch (err) {
            console.error(err)
            res.status(500).json({ success: false, message: "Erro ao criar chamado" })
        }
    }

    async getTicket(req, res) {
        const { userId } = req.params

        try {
            const result = await this.pool.request()
                .input("userId", this.sql.int, userId)
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
