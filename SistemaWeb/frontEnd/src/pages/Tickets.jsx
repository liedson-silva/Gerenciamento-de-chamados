import { useNavigate, useLocation } from "react-router-dom"
import { useState, useEffect } from "react"
import api from "../services/api"

const Tickets = () => {
    const location = useLocation()
    const user = location.state?.user
    const navigate = useNavigate()
    const handleViewTicketForm = (ticket) => {
        navigate("/view-ticket-form", { state: { user, ticket } })
    }

    const [ViewTickets, SetViewTickets] = useState([])

    async function getTickets() {
        const response = await api.get(`/tickets/${user.IdUsuario}`)
        SetViewTickets(response.data.Tickets)
    }

    useEffect(() => {
        getTickets()
    }, [])


    return (
        <section>

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

            {ViewTickets.length > 0 ? (
                ViewTickets.map((ticket) => (
                    <div key={ticket.IdChamado} className="box-ticket" onClick={() => handleViewTicketForm(ticket)}>
                        <ul className="info-ticket">
                            <li className="view-desktop">{ticket.IdChamado}</li>
                            <li>{ticket.Titulo}</li>
                            <li> <span className="circle-orange">ㅤ</span> {ticket.StatusChamado}</li>
                            <li className="view-desktop">{new Date(ticket.DataChamado).toLocaleDateString('pt-BR')}</li>
                            <li className="view-desktop"> <span className="circle-green">ㅤ</span> {ticket.PrioridadeChamado}</li>
                            <li className="view-desktop">{ticket.Categoria}</li>
                            <li className="view-desktop">{ticket.Descricao}</li>
                        </ul>
                    </div>
                ))
            ) : (
                <p className="no-call">Nenhum chamado no momento.</p>
            )}

        </section>
    )
}

export default Tickets