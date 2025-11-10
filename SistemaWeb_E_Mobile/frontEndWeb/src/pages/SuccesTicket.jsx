import correct from "../assets/correct.svg"
import { useNavigate, useLocation } from "react-router-dom"
import { formatDate } from "../components/FormatDate"
 
const SuccesTicket = () => {
    const location = useLocation()
    const { user, ticket } = location.state || {}
    const navigate = useNavigate()

    const handleViewTicketForm = () => {
        navigate("/view-ticket-form", { state: { user, ticket } })
    }

    const data = new Date(ticket.DataChamado);

    return (
        <main>

            <div className="success-ticket">
                <h1 className="success-title">Chamado enviado com sucesso!</h1>
                <p>Seu chamado foi registrado e será analisado em breve</p>
                <figure>
                    <img src={correct} className="success-img" alt="correct" />
                </figure>
            </div>

            <div className="success-details">
                <p>ID do chamado: {ticket.IdChamado}</p>
                <p>Data chamado: {formatDate(ticket.DataChamado)}</p>
                <p>Email enviado para: {user.Email}</p>
            </div>

            <div className='box-vizualizar-chamado'>
                <button className='button-vizualizar-chamado' onClick={handleViewTicketForm}>Vizualizar chamado</button>
            </div>

        </main>
    )
}

export default SuccesTicket