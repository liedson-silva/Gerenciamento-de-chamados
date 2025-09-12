const crypto = require("crypto")

function validarSenha(senha, hashSalvo) {
    const hashBytes = Buffer.from(hashSalvo, "base64");
    const salt = hashBytes.slice(0, 16)
    const hashOriginal = hashBytes.slice(16, 36)

    const hash = crypto.pbkdf2Sync(senha, salt, 10000, 20, "sha1")
    return hash.equals(hashOriginal)
}

async function LoginController(req, res, pool, sql) {
    const { name, password } = req.body

    if (!name || !password) {
        return res.status(400).json({ success: false, message: "Nome e senha obrigatórios" });
    }

    try {
        const result = await pool.request()
            .input("name", sql.VarChar, name)
            .query("SELECT Nome, Senha, Email FROM Usuario WHERE Nome = @name")

        if (result.recordset.length === 0) {
            return res.status(401).json({ success: false, message: "Usuário não encontrado" });
        }

        const user = result.recordset[0];

        const validPassword = validarSenha(password, user.Senha)

        if (!validPassword) {
            return res.status(401).json({ success: false, message: "Senha incorreta" });
        }

        res.json({ success: true, user: { Nome: user.Nome, Email: user.Email } });
    } catch (err) {
        console.error("Erro no login:", err);
        res.status(500).json({ success: false, message: "Erro no servidor" });
    }
}

module.exports = LoginController