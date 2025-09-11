import correct from "../assets/correct.svg"
import { useNavigate, useLocation } from "react-router-dom"

const SuccesTicket = () => {
    const location = useLocation()
    const user = location.state?.user
    const navigate = useNavigate()

    const handleViewTicketForm = () => {
        navigate("/view-ticket-form", { state: { user }} )
    }

    return (
        <section>

            <div className="success-ticket">
                <h1 className="success-title">Chamado enviado com sucesso!</h1>
                <p>Seu chamado foi registrado e será analisado em breve</p>
                <figure>
                    <img src={correct} className="success-img" alt="correct" />
                </figure>
            </div>

            <div className="success-details">
                <p>ID do chamado: XXXXX</p>
                <p>Data: XX/XX/XX</p>
                <p>Hora: XX:XX</p>
            </div>

            <div className='box-vizualizar-chamado'>
                <button className='button-vizualizar-chamado' onClick={handleViewTicketForm}>Vizualizar chamado</button>
            </div>
            
        </section>
    )
}

export default SuccesTicket