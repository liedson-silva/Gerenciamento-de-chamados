import { connectDB, sql } from "./config/connectDB.js"
import express from "express"
import cors from "cors"
import userRouter from "./routes/user.route.js"

const app = express()
app.use(express.json())

const allowedOrigins = [
    "https://fatal-system.vercel.app",
    "http://localhost:5173",
]

app.use(cors({
    origin: (origin, cb) => cb(null, !origin || allowedOrigins.includes(origin)),
    methods: ["GET", "POST", "PUT", "DELETE", "OPTIONS"],
    allowedHeaders: ["Content-Type", "Authorization"],
    credentials: true,
}))

app.get("/", (req, res) => res.json({ message: "Servidor comunicando" }))

const port = process.env.PORT || 3000
connectDB().then(pool => {
    app.use("/", userRouter(pool, sql))
    app.listen(port, () => console.log("Servidor rodando na porta " + port))
})