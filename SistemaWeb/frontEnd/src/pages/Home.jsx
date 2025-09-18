import React from 'react'
import { useLocation, useNavigate } from 'react-router-dom'

import folder from "../assets/folder.svg"
import hourglass from "../assets/hourglass.svg"
import correct from "../assets/correct.svg"
import StageCharts from '../components/StageCharts'

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
      <h1>Bem-vindo, {user?.Nome}!</h1>

      <div className="box-buttons">
        <div className="box-button-chamado">
          <button className="button-criar-chamado" onClick={handleCreateTicket}>
            Criar chamado
          </button>
        </div>

        <div className="chamados">
          <button className="button-chamados" onClick={handlePendingTicket}>
            <img src={folder} alt="Pendentes" />
            Chamados Pendentes
          </button>

          <button className="button-chamados" onClick={handleTicketInProgress}>
            <img src={hourglass} alt="Em andamento" />
            Chamados em Andamento
          </button>

          <button className="button-chamados" onClick={handleTicketResolved}>
            <img src={correct} alt="Solucionados" />
            Chamados Solucionados
          </button>
        </div>
      </div>

      {/* GRÁFICO */}
      <div className="chart-container">
        <StageCharts />
      </div>
    </section>
  )
}

export default Home