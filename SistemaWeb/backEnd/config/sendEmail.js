import nodemailer from "nodemailer"
import "dotenv/config"

const transport = nodemailer.createTransport({
    host: "smtp.gmail.com",
    port: 465,
    secure: true,
    auth: {
        user: "fatalsystem.unip@gmail.com",
        pass: process.env.GOOGLE_PASSWORD,
    }
})

export default async function sendEmail(email, name) {
    try {
        const mailOptions = {
            from: "Fatal System <fatalsystem.unip@gmail.com>",
            to: `${email}`,
            subject: "Chamado criado com sucesso!",
            html: `<h1>Prezado(a) ${name},</h1>
           <p>Seu chamado foi registrado com sucesso e será processado em breve pela nossa equipe.</p>`
        }
        await transport.sendMail(mailOptions) 
        console.log("Email enviado com sucesso!")
    } catch (err) {
        console.error("Erro ao enviar email:", err)
    }
}