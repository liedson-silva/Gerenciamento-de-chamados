import { useNavigate, useLocation } from "react-router-dom"
import { useState, useEffect } from "react"
import api from "../services/api"

const Tickets = () => {
    const location = useLocation()
    const user = location.state?.user
    const navigate = useNavigate()
    const handleViewTicketForm = () => {
        navigate("/view-ticket-form", { state: { user } })
    }

    const [ViewTickets, SetViewTickets] = useState([])

    async function getTickets(){
        const ticketsFromApi = await api.get(`/tickets/${user.IdUsuario}`)
        SetViewTickets(ticketsFromApi.data)
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

            {ViewTickets.map((ticket) => (
                <div className="box-ticket" onClick={handleViewTicketForm}>
                    <ul className="info-ticket">
                        <li className="view-desktop">{ticket.IdChamado}</li>
                        <li>{ticket.Titulo}</li>
                        <li> <span className="circle-orange">ㅤ</span> {ticket.StatusChamado}</li>
                        <li className="view-desktop">{ticket.DataChamado}</li>
                        <li className="view-desktop"> <span className="circle-green">ㅤ</span> {ticket.PrioridadeChamado}</li>
                        <li className="view-desktop">{ticket.Categoria}</li>
                        <li className="view-desktop">{ticket.Descricao}</li>
                    </ul>
                </div>
            ))}

        </section>
    )
}

export default Tickets