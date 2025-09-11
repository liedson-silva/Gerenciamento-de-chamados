const express = require("express")
const { connectDB, sql } = require("./config/connectDB")
const app = express()

let pool;
connectDB().then(p => pool = p)

app.use(express.json())
const port = 3000

app.get("/", async (req, res) => {
    try {
        const result = await pool.request().query("SELECT Nome FROM Usuario")
        res.json(result.recordset)
    } catch (err) {
        console.error("Erro ao consultar:", err)
        res.status(500).json({ error: "Erro ao buscar usuários" })
    }
})

app.listen(port, () => {
    console.log("Servidor rodando na porta " + port)
})