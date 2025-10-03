import crypto from "crypto"

export class UserController {
    //'pool' (conexão com o banco de dados) e 'sql' (tipos de dados do SQL)
    constructor(pool, sql) {
        this.pool = pool
        this.sql = sql
    }

    createHash(senha) {
        const salt = crypto.randomBytes(16);
        const hash = crypto.pbkdf2Sync(senha, salt, 10000, 20, "sha1")
        return Buffer.concat([salt, hash]).toString("base64")
    }

    async createUser(req, res) {
        const { name, cpf, rg, functionUser, sex, sector, date, email, password, login } = req.body

        if (!name || !cpf || !rg || !functionUser || !sex || !sector || !date || !email || !password || !login) {
            return res.status(400).json({ success: false, message: "Campos obrigatórios faltando" })
        }

        const passwordhash = this.createHash(password)

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
                .query("INSERT INTO Usuario (Nome, CPF, RG, FuncaoUsuario, Sexo, Setor, DataDeNascimento, Email, Senha, Login) OUTPUT INSERTED.* VALUES (@name, @cpf, @rg, @functionUser, @sex, @sector, @date, @email, @passwordhash, @login)")

            const newUser = result.recordset[0]

            return res.status(201).json({ success: true, user: newUser });

        } catch (err) {
            console.log("Erro ao criar usuário, " + err)
            res.status(500).json({ success: false, message: "Erro ao criar usuário" })
        }

        return res.json({ success: true, user: { name, cpf, rg, sector, functionUser, email, login, password, date, sex } })
    }
}