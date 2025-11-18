import jwt from "jsonwebtoken"

export const authenticateToken = (req, res, next) => {
    const authHeader = req.headers['authorization']
    const token = authHeader && authHeader.startsWith('Bearer ') ? authHeader.slice(7) : authHeader

    if (!token) {
        return res.status(403).json({ success: false, message: "Token não fornecido" })
    }

    jwt.verify(token, process.env.JWT_SECRET, (err, user) => {
        if (err) return res.status(403).json({ success: false, message: "Token inválido" })
        next()
    })
}
