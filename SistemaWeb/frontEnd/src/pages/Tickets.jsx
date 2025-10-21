import { useNavigate, useLocation } from "react-router-dom"
import { useState, useEffect } from "react"
import api from "../services/api"
import { formatDate } from "../components/FormatDate"

const Tickets = () => {
    const location = useLocation()
    const user = location.state?.user
    const [ViewTickets, SetViewTickets] = useState([])
    const navigate = useNavigate()
    const handleViewTicketForm = (ticket) => {
        navigate("/view-ticket-form", { state: { user, ticket } })
    }

    async function getTickets() {
        if (user.FuncaoUsuario === "Admin" || user.FuncaoUsuario === "Tecnico") {
            const response = await api.get("/all-tickets")
            SetViewTickets(response.data.Tickets)
        } else {
            const response = await api.get(`/tickets/${user.IdUsuario}`)
            SetViewTickets(response.data.Tickets)
        }
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

    function priorityDetail(PrioridadeChamado) {
        if (PrioridadeChamado === "Alta") {
            return (<><span className="circle-red">ㅤ</span> {PrioridadeChamado}</>)
        } else if (PrioridadeChamado === "Média") {
            return (<><span className="circle-blue">ㅤ</span> {PrioridadeChamado}</>)
        } else {
            return (<><span className="circle-green">ㅤ</span> {PrioridadeChamado}</>)
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
                {ViewTickets.length > 0 ? (
                    ViewTickets.map((ticket) => (
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
                    <p className="no-call">Nenhum chamado no momento.</p>
                )}
            </section>

        </main>
    )
}

export default Tickets