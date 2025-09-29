import { useNavigate, useLocation } from "react-router-dom"

const Tickets = () => {
    const location = useLocation()
    const user = location.state?.user
    const navigate = useNavigate()
    const handleViewTicketForm = () => {
        navigate("/view-ticket-form", { state: { user } })
    }

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

            <div className="box-ticket" onClick={handleViewTicketForm}>
                <ul className="info-ticket">
                    <li className="view-desktop">{}</li>
                    <li>{}</li>
                    <li> <span className="circle-orange">ㅤ</span> {}</li>
                    <li className="view-desktop">{}</li>
                    <li className="view-desktop"> <span className="circle-green">ㅤ</span> {}</li>
                    <li className="view-desktop">{}</li>
                    <li className="view-desktop">{}</li>
                </ul>
            </div>

        </section>
    )
}

export default Tickets