import folder from "../assets/folder.svg"
import hourglass from "../assets/hourglass.svg"
import correct from "../assets/correct.svg"
import { useLocation, useNavigate } from "react-router-dom"

const Home = () => {
  const location = useLocation()
  const user = location.state?.user
  const navigate = useNavigate()

  const handleCreateTicket = () => {
    navigate("/create-ticket", { state: { user } })
  }

  const handlePendingTicket = () => {
    navigate("/pending-ticket", { state: { user } })
  }

  const handleTicketInProgress = () => {
    navigate("/ticket-in-progress", { state: { user } })
  }

  const handleTicketResolved = () => {
    navigate("/ticket-resolved", { state: { user } })
  }

  return (
    <section>
      <h1>Bem-vindo, {user?.name}!</h1>
      <div className="box-buttons">
        <div className="box-button-chamado">
          <button className="button-criar-chamado" onClick={handleCreateTicket}>Criar chamado</button>
        </div>

        <div className="chamados">
          <button className="button-chamados" onClick={handlePendingTicket}><img src={folder} />
            Chamados Pendentes</button>
          <button className="button-chamados" onClick={handleTicketInProgress}><img src={hourglass} />
            Chamados em Andamentos</button>
          <button className="button-chamados" onClick={handleTicketResolved}><img src={correct} />
            Chamados Solucionados</button>
        </div>
      </div>
    </section>
  )
}

export default Home