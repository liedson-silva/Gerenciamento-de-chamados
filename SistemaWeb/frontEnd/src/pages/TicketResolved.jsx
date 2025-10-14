import { useNavigate, useLocation } from "react-router-dom"
import { useState, useEffect } from "react"
import api from "../services/api"
import { formatDate } from "../components/FormatDate"

const TicketResolved = () => {
    const location = useLocation()
    const user = location.state?.user
    const navigate = useNavigate()
    const handleViewTicketForm = (ticket) => {
        navigate("/view-ticket-form", { state: { user, ticket } })
    }

    const [ViewTicket, SetViewTicket] = useState([])

    async function getTickets() {
        if (user.FuncaoUsuario === "Admin" || user.FuncaoUsuario === "Tecnico") {
            const response = await api.get("/All-tickets")
            const allTickets = response.data.Tickets
            const pendingTickets = allTickets.filter(ticket => ticket.StatusChamado === "Resolvido")
            SetViewTicket(pendingTickets)
        } else {
            const response = await api.get(`/tickets/${user.IdUsuario}`)
            const allTickets = response.data.Tickets
            const pendingTickets = allTickets.filter(ticket => ticket.StatusChamado === "Resolvido")
            SetViewTicket(pendingTickets)
        }
    }

    useEffect(() => {
        getTickets()
    }, [])


    return (
        <main>

            <div className="ticket">
                <ul className="info-ticket">
                    <li className="view-desktop">ID</li>
                    <li>TÍTULO</li>
                    <li>STATUS</li>
                    <li className="view-desktop">DATA</li>
                    <li className="view-desktop">PRIORIDADE</li>
                    <li className="view-desktop">CATEGORIA</li>
                    <li className="view-desktop">DESCRIÇÃO</li>
                </ul>
            </div>

            <section className="scroll-list">
                {ViewTicket.length > 0 ? (
                    ViewTicket.map((ticket) => (
                        <div key={ticket.IdChamado} className="box-ticket" onClick={() => handleViewTicketForm(ticket)}>
                            <ul className="info-ticket">
                                <li className="view-desktop">{ticket.IdChamado}</li>
                                <li>{ticket.Titulo}</li>
                                <li> <span className="circle-green">ㅤ</span> {ticket.StatusChamado}</li>
                                <li className="view-desktop">{formatDate(ticket.DataChamado)}</li>
                                <li className="view-desktop"> <span className="circle-green">ㅤ</span> {ticket.PrioridadeChamado}</li>
                                <li className="view-desktop">{ticket.Categoria}</li>
                                <li className="view-desktop">{ticket.Descricao}</li>
                            </ul>
                        </div>
                    ))
                ) : (
                    <p className="no-call">Nenhum chamado resolvido no momento.</p>
                )}
            </section>

        </main>
    )
}

export default TicketResolved