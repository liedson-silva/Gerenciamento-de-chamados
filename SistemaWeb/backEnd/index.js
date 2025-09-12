const { connectDB, sql } = require("./config/connectDB")
const express = require("express")
const cors = require("cors")
const crypto = require("crypto")
const LoginController = require("./controllers/user.controller.js")

const app = express()
let pool;
connectDB().then(p => pool = p)

app.use(express.json())
app.use(cors())
const port = 3000

app.post("/login", (req, res) => LoginController(req, res, pool, sql))

app.listen(port, () => {
    console.log("Servidor rodando na porta " + port)
})