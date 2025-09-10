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
                    <li className="view-desktop">4856</li>
                    <li>Impressora não liga</li>
                    <li> <span className="circle-orange">ㅤ</span> Pendente</li>
                    <li className="view-desktop">05/07/2004</li>
                    <li className="view-desktop"> <span className="circle-green">ㅤ</span> Baixa</li>
                    <li className="view-desktop">Hardware</li>
                    <li className="view-desktop">A impressora está ligada e corretamente conectada ao computador/rede, porém não esta realizando impressões. Os documentos estão em fila.</li>
                </ul>
            </div>

        </section>
    )
}

export default Tickets