import { useState, useEffect } from "react"
import api from "../services/api.js"
import { formatDate } from "../components/FormatDate.jsx"

const ReplyTicket = () => {
    const [ViewTickets, SetViewTickets] = useState([])
    const [successMessage, setSuccessMessage] = useState("")
    const [errorMessage, setErrorMessage] = useState("")
    const [showForm, setShowForm] = useState(false)
    const [formData, setFormData] = useState({
    idTicket: "",
    idUser: "",
    title: "",
    description: "",
    priority: "",
    date: "",
  })

    async function getTicket() {
        try {
            const response = await api.get("/All-tickets")
            if (response.data.success) {
                SetViewTickets(response.data.Tickets)
            } else {
                setErrorMessage("Erro ao carregar chamados.")
            }
        } catch (error) {
            console.error("Erro ao buscar chamados:", error)
            setErrorMessage("Erro ao buscar chamados.")
        }
        setTimeout(() => {
            setSuccessMessage("");
            setErrorMessage("");
        }, 3000);
    }
    useEffect(() => {
        getTicket()
    }, [])

    const handleReplyTicket = (ticket) => {
    setFormData({
      idTicket: ticket.IdChamado,
      idUser: ticket.FK_IdUsuario,
      title: ticket.Titulo,
      description: ticket.Descricao,
      priority: ticket.PrioridadeChamado,
      date: ticket.DataChamado
    })
    setShowForm(true)
  }

    return (
        <main className="scroll-list">
            <header className="home-header">
                <h1 className="home-welcome">Responder chamado</h1>
            </header>

            {errorMessage && <p className="notice">{errorMessage}</p>}
            {successMessage && <p className="success">{successMessage}</p>}

            <section className="ticket-list">
                <div className="scroll-lists">
                    <table className="ticket-table">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Título</th>
                                <th>Descrição</th>
                                <th>Prioridade</th>
                                <th>Data</th>
                                <th>Status</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            {ViewTickets.map((ticket) => (
                                <tr key={ticket.IdChamado}>
                                    <td>{ticket.IdChamado}</td>
                                    <td>{ticket.Titulo}</td>
                                    <td>{ticket.Descricao}</td>
                                    <td>{ticket.PrioridadeChamado === "Média" ? (
                                        <> <span className="circle-blue">ㅤ</span> {ticket.PrioridadeChamado}</>
                                    ) : ticket.PrioridadeChamado === "Alta" ? (
                                        <> <span className="circle-red">ㅤ</span> {ticket.PrioridadeChamado}</>
                                    ) : (
                                        <> <span className="circle-green">ㅤ</span> {ticket.PrioridadeChamado}</>
                                    )}</td>
                                    <td>{formatDate(ticket.DataChamado)}</td>
                                    <td>{ticket.StatusChamado === "Pendente" ? (
                                        <> <span className="circle-yellow">ㅤ</span> {ticket.StatusChamado}</>
                                    ) : ticket.StatusChamado === "Em andamento" ? (
                                        <> <span className="circle-orange">ㅤ</span> {ticket.StatusChamado}</>
                                    ) : (
                                        <> <span className="circle-green">ㅤ</span> {ticket.StatusChamado}</>
                                    )}</td>
                                    <td>
                                        <button className="button-reply-ticket" onClick={() => (handleReplyTicket (ticket))}>Responder</button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </section>

            {showForm && (
                <section className="reply-ticket">
                    <h2>Detalhes do Chamado #{formData.idTicket}</h2>
                    <div className="reply-info-ticket">
                        <p>Id do Solicitante: {formData.idUser}</p>
                        <p>Data de Abertura: {formatDate(formData.date)}</p>
                        <p>Prioridade: {formData.priority}</p>
                        <p>Titulo: {formData.title}</p>
                        <p>Descrição: {formData.description}</p>
                    </div>
                    <input name="name" className="form-reply-ticket" placeholder="Resposta:" value={""} onChange={""} required />
                    <button className="button-confirm-reply" type="submit">Enviar</button>
                </section>
            )}

        </main>
    )
}

export default ReplyTicket