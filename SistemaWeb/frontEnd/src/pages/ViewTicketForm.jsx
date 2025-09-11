import { useLocation, useNavigate } from "react-router-dom"

const ViewTicketForm = () => {
  const location = useLocation()
  const user = location.state?.user
  const navigate = useNavigate()

  const handleHome = () => {
    navigate("/home", { state: { user } })
  }

  return (
    <section className="view-ticket-form">

      <div className="form-data">
        <h1 className="form-title">Dados do formulário</h1>
        <p className="form-info">Criado em: <span className="form-info-data">2 horas atrás por {user?.name}</span></p>
        <p className="form-info">Título: <span className="form-info-data">Impressora não liga</span></p>
        <p className="form-info">Categoria: <span className="form-info-data">Hardware</span></p>
        <p className="form-info">Descrição do problema: <span className="form-info-data">A impressora está ligada e corretamente conectada ao computador/rede, porém não esta realizando impressões. Os documentos estão em fila.</span></p>
        <p className="form-info">Arquivo anexo: <span className="form-info-data">img.png</span></p>
        <p className="form-info">Pessoas afetadas: <span className="form-info-data">Somente eu</span></p>
        <p className="form-info">Problema esta impedindo meu trabalho? <span className="form-info-data">Sim</span></p>
        <p className="form-info">Já ocorreu anteriormente? <span className="form-info-data">Não sei</span></p>
      </div>

      <div className='box-pagina-inicia'>
        <button className='button-pagina-inicia' onClick={handleHome}>Página inicial</button>
      </div>

    </section>
  )
}

export default ViewTicketForm