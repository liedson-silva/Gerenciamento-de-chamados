import { connectDB, sql } from "./config/connectDB.js"
import express from "express"
import cors from "cors"
import userRouter from "./routes/user.route.js"

const app = express()
app.use(express.json())
const allowedOrigins = [
    "https://fatal-system.vercel.app",
    "https://gerenciamento-de-chamados.vercel.app",
]

const corsOptions = {
    origin: (origin, callback) => {
        if (!origin) return callback(null, true)
        if (allowedOrigins.indexOf(origin) !== -1) {
            return callback(null, true)
        }
        return callback(new Error("CORS policy: Origin not allowed"))
    },
    methods: ["GET", "POST", "PUT", "DELETE", "OPTIONS"],
    allowedHeaders: ["Content-Type", "Authorization"],
    credentials: true,
}

app.use(cors(corsOptions))
app.options("*", cors(corsOptions))

app.get("/", (req, res) => res.json({ message: "Servidor comunicando" }))

const port = 3000
connectDB().then(pool => {
    app.use("/", userRouter(pool, sql))

    app.listen(port, () => {
        console.log("Servidor rodando na porta " + port)
    })
})
