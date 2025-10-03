import crypto from "crypto"

export class LoginController {
    //'pool' (conexão com o banco de dados) e 'sql' (tipos de dados do SQL)
    constructor(pool, sql) {
        this.pool = pool
        this.sql = sql
    }

    validatePassword(senha, hashSalvo) {
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
            const validPassword = this.validatePassword(password, user.Senha)

            if (!validPassword) {
                return res.status(401).json({ success: false, message: "Senha incorreta" })
            }

            res.json({
                success: true, user: user
            })

        } catch (err) {
            console.error("Erro no login:", err);
            res.status(500).json({ success: false, message: "Erro no login" })
        }
    }
}