import { useNavigate, useLocation } from "react-router-dom"
import { useState, useEffect } from "react"
import api from "../services/api"
import { formatDate } from "../components/FormatDate"

const MediumTicket = () => {
    const location = useLocation()
    const user = location.state?.user
    const navigate = useNavigate()
    const handleViewTicketForm = (ticket) => {
        navigate("/view-ticket-form", { state: { user, ticket } })
    }

    const [ViewTicket, SetViewTicket] = useState([])

    async function getTickets() {
        const response = await api.get("/manage-tickets");
        const allTickets = response.data.Tickets
        const pendingTickets = allTickets.filter(ticket => ticket.PrioridadeChamado === "Média")
        SetViewTicket(pendingTickets)
    }

    useEffect(() => {
        getTickets()
    }, [])

    function statusDetail(StatusChamado) {
        if (StatusChamado === "Pendente") {
            return (<><span className="circle-yellow">ㅤ</span> {StatusChamado}</>)
        } else if (StatusChamado === "Em andamento") {
            return (<><span className="circle-orange">ㅤ</span> {StatusChamado}</>)
        } else {
            return (<><span className="circle-green">ㅤ</span> {StatusChamado}</>)
        }
    }

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
                                <li>{statusDetail(ticket.StatusChamado)}</li>
                                <li className="view-desktop">{formatDate(ticket.DataChamado)}</li>
                                <li className="view-desktop"> <span className="circle-blue">ㅤ</span> {ticket.PrioridadeChamado}</li>
                                <li className="view-desktop">{ticket.Categoria}</li>
                                <li className="view-desktop">{ticket.Descricao}</li>
                            </ul>
                        </div>
                    ))
                ) : (
                    <p className="no-call">Nenhum chamado com prioridade média no momento.</p>
                )}
            </section>

        </main>
    )
}

export default MediumTicket