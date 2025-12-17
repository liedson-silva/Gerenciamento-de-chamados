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


const port = process.env.PORT || 3000

connectDB().then(pool => {
    app.get("/", async (req, res) => {
        try {
            await pool.request().query("SELECT 1");
            res.json({
                status: "Online",
                message: "Servidor comunicando",
                database: "Conectado com sucesso ao Azure SQL"
            });
        } catch (err) {
            res.status(500).json({
                status: "Erro",
                message: "Servidor rodando, mas banco de dados inacessível",
                error: err.message
            });
        }
    });

    app.use("/", userRouter(pool, sql))

    app.listen(port, () => console.log("Servidor rodando na porta " + port))
}).catch(err => {
    console.error("Falha fatal ao iniciar o servidor:", err);
});