import dotenv from "dotenv"
import sql from "mssql"

dotenv.config()

const config = {
    user: process.env.DB_USER,
    password: process.env.DB_PASSWORD,
    server: process.env.DB_SERVER,
    database: process.env.DB_DATABASE,
    options: {
        encrypt: true,
        trustServerCertificate: true
    }
};

export async function connectDB() {
    try {
        let pool = await sql.connect(config);
        console.log("Conectado ao SQL Server!");
        return pool;
    } catch (err) {
        console.error("Erro na conexão com o SQL Server:", err);
    }
}

export { sql };