import { connectDB, sql } from "./config/connectDB.js"
import express from "express"
import cors from "cors"
import userRouter from "./route/user.route.js"


const app = express()
app.use(express.json())
app.use(cors())

app.get("/", (req, res) => res.json({ message: "Servidor comunicando" }))

const port = 3000
connectDB().then(pool => {
    app.use("/", userRouter(pool, sql))

    app.listen(port, () => {
        console.log("Servidor rodando na porta " + port)
    })
})
