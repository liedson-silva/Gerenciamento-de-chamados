import { useLocation, useNavigate } from "react-router-dom"

const ViewTicketForm = () => {
  const location = useLocation()
  const {user, ticket, affectedPeople, stopWork, happenedBefore } = location.state || {}
  const navigate = useNavigate()

  const handleHome = () => {
    navigate("/home", { state: { user } })
  }

  return (
    <section className="view-ticket-form">

      <div className="form-data">
        <h1 className="form-title">Dados do formulário</h1>
        <p className="form-info">Criado por : <span className="form-info-data">{user?.Nome}</span></p>
        <p className="form-info">Título: <span className="form-info-data">{ticket.Titulo}</span></p>
        <p className="form-info">Categoria: <span className="form-info-data">{ticket.Categoria}</span></p>
        <p className="form-info">Descrição do problema: <span className="form-info-data">{ticket.Descricao}</span></p>
        <p className="form-info">Arquivo anexo: <span className="form-info-data">...</span></p>
        <p className="form-info">Pessoas afetadas: <span className="form-info-data">{affectedPeople}</span></p>
        <p className="form-info">Problema esta impedindo meu trabalho? <span className="form-info-data">{stopWork}</span></p>
        <p className="form-info">Já ocorreu anteriormente? <span className="form-info-data">{happenedBefore}</span></p>
      </div>

      <div className='box-pagina-inicia'>
        <button className='button-pagina-inicia' onClick={handleHome}>Página inicial</button>
      </div>

    </section>
  )
}

export default ViewTicketForm