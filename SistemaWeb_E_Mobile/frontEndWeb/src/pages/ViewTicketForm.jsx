import { useLocation, useNavigate } from "react-router-dom"
import { formatDate } from "../components/FormatDate"
import { useEffect, useState } from "react"
import api from "../services/api.js"

const ViewTicketForm = () => {
  const location = useLocation()
  const { user, ticket } = location.state || {}
  const navigate = useNavigate()
  const [replyText, setReplyText] = useState("")

  async function fetchSuggestedSolution(idTicket) {
    try {
      const response = await api.get(`/get-reply-ticket/${idTicket}`)
      if (response.data.success) {
        const solutionRecord = response.data.Tickets.find(
          (record) => record.Acao === "Solução Aplicada"
        )
        if (solutionRecord) {
          const solutionText = solutionRecord.Solucao
          setReplyText(solutionText)
        } else {
          setReplyText("O ticket ainda não foi resolvido.");
        }
      } else {
        setReplyText("Falha ao buscar o histórico do ticket.")
      }
    } catch (error) {
      setReplyText("Erro de conexão. Tente novamente mais tarde.")
    }
  }
  useEffect(() => {
    if (ticket && ticket.IdChamado) {
      fetchSuggestedSolution(ticket.IdChamado)
    }
  }, [ticket])

  const handleHome = () => {
    if (user.FuncaoUsuario === "Admin") {
      navigate("/admin-home", { state: { user } });
    } else if (user.FuncaoUsuario === "Tecnico") {
      navigate("/tec-home", { state: { user } });
    } else {
      navigate("/home", { state: { user } });
    }
  }

  return (
    <main className="view-ticket-form">

      <div className="scroll-ticket">
      <section className="form-data">
        <h1 className="form-title">Dados do formulário</h1>
        <p className="form-info">Id do chamado: <span className="form-info-data">{ticket.IdChamado}</span></p>
        <p className="form-info">Id do usuário: <span className="form-info-data">{ticket.FK_IdUsuario}</span></p>
        <p className="form-info">Título: <span className="form-info-data">{ticket.Titulo}</span></p>
        <p className="form-info">Data : <span className="form-info-data">{formatDate(ticket?.DataChamado)}</span></p>
        <p className="form-info">Status: <span className="form-info-data">{ticket.StatusChamado}</span></p>
        <p className="form-info">Prioridade: <span className="form-info-data">{ticket.PrioridadeChamado}</span></p>
        <p className="form-info">Descrição do problema: <span className="form-info-data">{ticket.Descricao}</span></p>
        <p className="form-info-desktop">Pessoas afetadas: <span className="form-info-data">{ticket.PessoasAfetadas}</span></p>
        <p className="form-info-desktop">Problema esta impedindo meu trabalho? <span className="form-info-data">{ticket.ImpedeTrabalho}</span></p>
        <p className="form-info-desktop">Já ocorreu anteriormente? <span className="form-info-data">{ticket.OcorreuAnteriormente}</span></p>
        <p className="form-info">Solução Sugerida/Aplicada: <span className="form-info-data">{replyText}</span></p>
      </section>
      </div>

      <div className='box-pagina-inicia'>
        <button className='button-pagina-inicia' onClick={handleHome}>Página inicial</button>
      </div>

    </main>
  )
}

export default ViewTicketForm