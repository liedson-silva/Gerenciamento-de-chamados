import { useNavigate, useLocation } from "react-router-dom"
import { useState, useEffect } from "react"
import api from "../services/api"
import { formatDate } from "../components/FormatDate"

const PriorityTicket = () => {
    const location = useLocation()
    const { user, priority } = location.state
    const navigate = useNavigate()
    const handleViewTicketForm = (ticket) => {
        navigate("/view-ticket-form", { state: { user, ticket } })
    }

    const [ViewTicket, SetViewTicket] = useState([])

    async function getTickets() {
        const response = await api.get("/All-tickets")
        const allTickets = response.data.Tickets
        const priorityTickets = allTickets.filter(ticket => ticket.PrioridadeChamado === priority)
        SetViewTicket(priorityTickets)
    }

    useEffect(() => {
        getTickets()
    }, [])

    function statusDetail(statusTicket) {
        if (statusTicket === "Pendente") {
            return (<><span className="circle-yellow">ㅤ</span> {statusTicket}</>)
        } else if (statusTicket === "Em andamento") {
            return (<><span className="circle-orange">ㅤ</span> {statusTicket}</>)
        } else {
            return (<><span className="circle-green">ㅤ</span> {statusTicket}</>)
        }
    }

    function priorityDetail(priority) {
        if (priority === "Alta") {
            return (<><span className="circle-red">ㅤ</span> {priority}</>)
        } else if (priority === "Média") {
            return (<><span className="circle-blue">ㅤ</span> {priority}</>)
        } else {
            return (<><span className="circle-green">ㅤ</span> {priority}</>)
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
                                <li className="view-desktop">{priorityDetail(ticket.PrioridadeChamado)}</li>
                                <li className="view-desktop">{ticket.Categoria}</li>
                                <li className="view-desktop">{ticket.Descricao}</li>
                            </ul>
                        </div>
                    ))
                ) : (
                    priority === "Baixa" ?
                        <p className="no-call">Nenhum chamado com prioridade baixa no momento.</p> :
                        priority === "Média" ?
                            <p className="no-call">Nenhum chamado com prioridade média no momento.</p> :
                            <p className="no-call">Nenhum chamado com prioridade alta no momento.</p>
                )
                }
            </section>

        </main>
    )
}

export default PriorityTicket