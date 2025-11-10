import { useState, useEffect } from "react"
import { useNavigate, useLocation } from 'react-router-dom';
import api from "../services/api.js"
import { formatDate } from "../components/FormatDate.jsx"

const ReplyTicket = () => {
    const location = useLocation()
    const user = location.state?.user
    const navigate = useNavigate()

    const [tickets, setTickets] = useState([])
    const [replyText, setReplyText] = useState("")
    const [successMessage, setSuccessMessage] = useState("")
    const [errorMessage, setErrorMessage] = useState("")
    const [showForm, setShowForm] = useState(false)
    const [selectedTicket, setSelectedTicket] = useState({
        idTicket: "", idUser: "", title: "", description: "",
        priority: "", status: "", date: "",
    })

    async function fetchTickets() {
        try {
            const response = await api.get("/All-tickets")
            if (response.data.success) {
                const allTickets = response.data.Tickets.filter(ticket => ticket.StatusChamado === "Pendente")

                const orderedTickets = allTickets.sort((a, b) => {
                    const priorityOrder = { "Alta": 1, "Média": 2, "Baixa": 3 }
                    const priorityA = priorityOrder[a.PrioridadeChamado] || 99
                    const priorityB = priorityOrder[b.PrioridadeChamado] || 99

                    return priorityA - priorityB
                })
                setTickets(orderedTickets)
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
        fetchTickets()
    }, [])

    async function fetchSuggestedSolution(idTicket) {
        try {
            const response = await api.get(`/get-reply-ticket/${idTicket}`)
            if (response.data.success && response.data.Tickets && response.data.Tickets.length > 0) {
                if (response.data.Tickets[0].Acao === "Nota Interna") {
                    const solutionText = response.data.Tickets[0].Solucao
                    setReplyText(solutionText)
                }
            } else {
                setErrorMessage("Erro ao carregar solução.")
                setReplyText("Nenhuma proposta de solução automática encontrada.")
            }
        } catch (error) {
            setErrorMessage("Erro ao buscar solução.")
            setReplyText("Nenhuma proposta de solução automática encontrada.")
        }
        setTimeout(() => {
            setSuccessMessage("");
            setErrorMessage("");
        }, 3000);
    }

    const handleSubmitReply = async ( { status } ) => {
        const id = selectedTicket.idTicket
        const solution = replyText

        if (!id || !solution.trim()) {
            setErrorMessage("O campo de resposta não pode estar vazio.")
            setTimeout(() => setErrorMessage(""), 3000)
            return
        }
        try {
            const { data } = await api.post("/reply-ticket", { id, solution, status })

            if (data.success) {
                setSuccessMessage("Solução enviada!")
                setTimeout(() => setSuccessMessage(""), 3000)
                setShowForm(false)
                setReplyText("")
                fetchTickets()
            }
        } catch (err) {
            setErrorMessage("Erro ao conectar com o servidor para enviar a solução.")
            setTimeout(() => setErrorMessage(""), 3000)
        }
    }

    const handleSelectTicket = (ticket) => {
        setSelectedTicket({
            idTicket: ticket.IdChamado,
            idUser: ticket.FK_IdUsuario,
            title: ticket.Titulo,
            description: ticket.Descricao,
            priority: ticket.PrioridadeChamado,
            status: ticket.StatusChamado,
            date: ticket.DataChamado
        })
        setShowForm(true)
        fetchSuggestedSolution(ticket.IdChamado)
    }

    const handleBack = () => {
        navigate("/tec-home", { state: { user } });
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
                                <th>Usuário</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            {tickets.map((ticket) => (
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
                                    <td>{ticket.FK_IdUsuario}</td>
                                    <td>
                                        <button className="button-reply-ticket" onClick={() => (handleSelectTicket(ticket))}>Responder</button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </section>

            {showForm && (
                <section className="reply-ticket">
                    <h2>Detalhes do Chamado #{selectedTicket.idTicket}</h2>
                    <div className="reply-info-ticket">
                        <p>Id do Solicitante: {selectedTicket.idUser}</p>
                        <p>Data de Abertura: {formatDate(selectedTicket.date)}</p>
                        <p>Prioridade: {selectedTicket.priority}</p>
                        <p>Status: {selectedTicket.status}</p>
                        <p>Titulo: {selectedTicket.title}</p>
                        <p>Descrição: {selectedTicket.description}</p>
                    </div>
                    <div className="form-group">
                        <textarea
                            id="reply"
                            className="input-reply-ticket"
                            value={replyText}
                            onChange={(e) => setReplyText(e.target.value)}
                            required
                        />
                        <label htmlFor="reply">Resposta</label>
                    </div>
                </section>
            )}

            <div className="box-button-back-reply">
                <button onClick={handleBack} className='button-back' >
                    Voltar
                </button>
                {showForm && (<>
                    <button className="button-confirm-reply" onClick={() => handleSubmitReply({ status: 'Resolvido' })} type="submit">Enviar</button>

                    <button className="button-confirm-reply" onClick={() => handleSubmitReply({ status: 'Em andamento' })}>Enviar/Alterar status</button>
                </>)}
            </div>

        </main>
    )
}

export default ReplyTicket