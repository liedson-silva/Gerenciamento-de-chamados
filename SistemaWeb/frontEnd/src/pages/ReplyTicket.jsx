import { useLocation } from "react-router-dom"
import { useState, useEffect } from "react"
import api from "../services/api.js"
import { formatDate } from "../components/FormatDate.jsx"

const ReplyTicket = () => {
    const location = useLocation()
    const user = location.state.user
    const [ViewTickets, SetViewTickets] = useState([])

    async function getTicket() {
        const response = await api.get("/All-tickets")
        SetViewTickets(response.data.Tickets)
    }
    useEffect(() => {
        getTicket()
    }, [])

    return (
        <main className="scroll-list">
            <header className="home-header">
                <h1 className="home-welcome">Responder chamado</h1>
            </header>

            <section className="ticket-list">
                <div className="scroll-lists">
                    <table className="ticket-table">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Título</th>
                                <th>Descrição</th>
                                <th>Prioridade</th>
                                <th>Data</th>
                                <th>Status</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            {ViewTickets.map((ticket) => (
                                <tr key={ticket.IdUsuario}>
                                    <td>{ticket.IdChamado}</td>
                                    <td>{ticket.Titulo}</td>
                                    <td>{ticket.Descricao}</td>
                                    <td>{ticket.PrioridadeChamado === "Média" ? (
                                        <> <span className="circle-blue">ㅤ</span> {ticket.PrioridadeChamado}</>
                                    ) : ticket.PrioridadeChamado === "Alta" ? (
                                        <> <span className="circle-red">ㅤ</span> {ticket.PrioridadeChamado}</>
                                    ) : (
                                        <> <span className="circle-green">ㅤ</span> {ticket.PrioridadeChamado}</>
                                    )}</td>
                                    <td>{formatDate(ticket.DataChamado)}</td>
                                    <td>{ticket.StatusChamado === "Pendente" ? (
                                        <> <span className="circle-yellow">ㅤ</span> {ticket.StatusChamado}</>
                                    ) : ticket.StatusChamado === "Em andamento" ? (
                                        <> <span className="circle-orange">ㅤ</span> {ticket.StatusChamado}</>
                                    ) : (
                                        <> <span className="circle-green">ㅤ</span> {ticket.StatusChamado}</>
                                    )}</td>
                                    <td>
                                        <button className="button-reply-ticket">Responder</button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </section>

            <section className="reply-ticket">
                <h2>Detalhes do Chamado #{ }</h2>
                <div className="reply-info-ticket">
                    <p>Id do Solicitante: xx</p>
                    <p>Data de Abertura: xx/xx/xxxx</p>
                    <p>Prioridade: x</p>
                    <p>Titulo: x</p>
                    <p>Descrição: x</p>
                </div>
                <input name="name" className="form-reply-ticket" placeholder="Resposta:" value={""} onChange={""} required />
                <button className="button-confirm-reply" type="submit">Enviar</button>
            </section>
        </main>
    )
}

export default ReplyTicket