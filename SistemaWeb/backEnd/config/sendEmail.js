import nodemailer from "nodemailer"
import "dotenv/config"
import Gemini from "./connectIA.js"

const transport = nodemailer.createTransport({
    host: "smtp.gmail.com",
    port: 465,
    secure: true,
    auth: {
        user: process.env.GOOGLE_EMAIL,
        pass: process.env.GOOGLE_PASSWORD,
    }
})

export default async function sendEmail(email, name, ticketId, date, title, description, category, priority, status, affectedPeople, stopWork, happenedBefore) {
    try {
        const mailOptions = {
            from: `Fatal System <${process.env.GOOGLE_EMAIL}>`,
            to: `${email}`,
            subject: "Chamado criado com sucesso!",
            html: `<h1>Prezado(a) ${name},</h1>
           <p>Seu chamado foi registrado com sucesso e será processado em breve pela nossa equipe.</p>
           <hr>
           <p><b>Id:</b> ${ticketId}</p>
           <p><b>Data:</b> ${date}</p>
           <p><b>Título:</b> ${title}</p>
           <p><b>Descrição:</b> ${description}</p>
           <p><b>Categoria:</b> ${category}</p>
           <p><b>Prioridade:</b> ${priority}</p>
           <p><b>Status:</b> ${status}</p>
           <hr>
           <p><b>Pessoas afetadas:</b> ${affectedPeople}</p>
           <p><b>Impede o trabalho:</b> ${stopWork}</p>
           <p><b>Ocorreu anteriormente:</b> ${happenedBefore}</p>
           `
        }

        const proposta = await Gemini(`Você é um sistema de suporte de Nível 1, e eficiente, que pré-analisa chamados técnicos para a equipe de TI. Sua tarefa é analisar a descrição do problema de um usuário e gerar uma **AÇÃO PROPOSTA** clara e concisa para a equipe técnica.
        A **AÇÃO PROPOSTA** deve:
        1.  **Retornar apenas as ações sugeridas** de diagnóstico ou solução (ex: "Verificar logs do servidor X", "Reiniciar serviço Y", "Contatar o usuário para acesso remoto").
        2.  Ser formatada usando **marcadores (bullet points)**.
        3.  **NÃO** incluir cabeçalhos como "Conclusão Proposta" ou "Causa Raiz".
        **Descrição do Chamado:**
        ${description}
        **Ações Sugeridas:**`)

        const mailOptions2 = {
            from: `Fatal System <${process.env.GOOGLE_EMAIL}>`,
            to: `${process.env.GOOGLE_EMAIL}`,
            subject: `Chamado criado pelo(a) ${name}!`,
            html: `<h1>Prezado(a) Equipe de TI,</h1>
           <p>O chamado do(a) ${name} foi registrado com sucesso! Segui o conteúdo e a ação proposta.</p>
           <hr>
           <p><b>Id:</b> ${ticketId}</p>
           <p><b>Data:</b> ${date}</p>
           <p><b>Título:</b> ${title}</p>
           <p><b>Descrição:</b> ${description}</p>
           <p><b>Categoria:</b> ${category}</p>
           <p><b>Prioridade:</b> ${priority}</p>
           <p><b>Status:</b> ${status}</p>
           <hr>
           <p><b>Pessoas afetadas:</b> ${affectedPeople}</p>
           <p><b>Impede o trabalho:</b> ${stopWork}</p>
           <p><b>Ocorreu anteriormente:</b> ${happenedBefore}</p>
           <hr>
           <p><b>Proposta pela IA:</b> ${proposta}</p>
           `
        }
        await transport.sendMail(mailOptions)
        await transport.sendMail(mailOptions2)
        console.log("Email enviado com sucesso!")
    } catch (err) {
        console.error("Erro ao enviar email:", err)
    }
}