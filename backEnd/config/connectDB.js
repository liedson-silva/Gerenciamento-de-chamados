require("dotenv").config();
const sql = require("mssql");

const config = {
    user: process.env.DB_User,
    password: process.env.DB_PASSWORD,
    server: process.env.DB_SERVER,
    database: process.env.DB_DATABASE,
    options: {
        encrypt: true,
        trustServerCertificate: false
    }
};

async function connectDB() {
    try {
        let pool = await sql.connect(config);
        console.log("Conectado ao SQL Server!");
        return pool;
    } catch (err) {
        console.error("Erro na conexão:", err);
    }
}

module.exports = { sql, connectDB };