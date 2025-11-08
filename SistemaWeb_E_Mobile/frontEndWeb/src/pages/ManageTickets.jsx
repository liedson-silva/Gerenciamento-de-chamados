import { useEffect, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import api from "../services/api";
import "../Style.css";
import { formatDate } from "../components/FormatDate";

const ManageTickets = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const user = location.state?.user;

  if (!user) {
    return <p>Usuário não autenticado.</p>;
  }

  const [tickets, setTickets] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [selectedTicketId, setSelectedTicketId] = useState(null);
  const [formData, setFormData] = useState({
    Titulo: "",
    PrioridadeChamado: "",
    Descricao: "",
    DataChamado: "",
    StatusChamado: "",
    Categoria: "",
    FK_IdUsuario: user.IdUsuario || "", // já setar com usuário logado
    PessoasAfetadas: "",
    ImpedeTrabalho: "",
    OcorreuAnteriormente: "",
  });

  const [successMessage, setSuccessMessage] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  useEffect(() => {
    fetchTickets();
  }, []);

  const fetchTickets = async () => {
    try {
      const response = await api.get("/all-tickets");
      if (response.data.success) {
        setTickets(response.data.Tickets);
      } else {
        setErrorMessage("Erro ao carregar chamados.");
      }
    } catch (error) {
      console.error("Erro ao buscar chamados:", error);
      setErrorMessage("Erro ao buscar chamados.");
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleCreateTicket = () => {
    navigate("/create-ticket", { state: { user } });
  };

  const handleEditTicket = (ticket) => {
    setFormData({
      Titulo: ticket.Titulo || "",
      PrioridadeChamado: ticket.PrioridadeChamado || "",
      Descricao: ticket.Descricao || "",
      DataChamado: ticket.DataChamado ? ticket.DataChamado.slice(0, 10) : "",
      StatusChamado: ticket.StatusChamado || "",
      Categoria: ticket.Categoria || "",
      FK_IdUsuario: ticket.FK_IdUsuario || user.IdUsuario || "",
      PessoasAfetadas: ticket.PessoasAfetadas || "",
      ImpedeTrabalho: ticket.ImpedeTrabalho || "",
      OcorreuAnteriormente: ticket.OcorreuAnteriormente || "",
    });
    setSelectedTicketId(ticket.IdChamado);
    setIsEditing(true);
    setShowForm(true);
  };

  const handleFormSubmit = async (e) => {
    e.preventDefault();

    try {
      if (isEditing) {
        const response = await api.put(`/update-ticket/${selectedTicketId}`, formData);
        if (response.data.success) {
          setSuccessMessage("Chamado atualizado com sucesso!");
        } else {
          setErrorMessage("Erro ao atualizar chamado.");
        }
      } else {
        const response = await api.post("/create-ticket", formData);
        if (response.data.success) {
          setSuccessMessage("Chamado criado com sucesso!");
        } else {
          setErrorMessage("Erro ao criar chamado.");
        }
      }
      setShowForm(false);
      fetchTickets();
    } catch (error) {
      console.error("Erro:", error);
      setErrorMessage("Erro ao processar a solicitação.");
    }

    setTimeout(() => {
      setSuccessMessage("");
      setErrorMessage("");
    }, 3000);
  };

  const handleBack = () => {
    navigate("/admin-home", { state: { user } });
  };

  return (
    <main className="scroll-list">
      <h1>Gerenciar Chamados</h1>

      {errorMessage && <p className="notice">{errorMessage}</p>}
      {successMessage && <p className="success">{successMessage}</p>}

      <button onClick={handleCreateTicket} className="button-create-ticket">
        Criar Novo Chamado
      </button>

      <section className="user-list" style={{ position: "relative", paddingBottom: "70px" }}>
        <h2>Chamados Cadastrados</h2>
        <div className="scroll-lists">
          <table className="user-table">
            <thead>
              <tr>
                <th>ID</th>
                <th>Título</th>
                <th>Prioridade</th>
                <th>Descrição</th>
                <th>Data</th>
                <th>Status</th>
                <th>Categoria</th>
                <th>Usuário</th>
                <th>Ações</th>
              </tr>
            </thead>
            <tbody>
              {tickets.map((ticket) => (
                <tr key={ticket.IdChamado}>
                  <td>{ticket.IdChamado}</td>
                  <td>{ticket.Titulo}</td>
                  <td>{ticket.PrioridadeChamado}</td>
                  <td>{ticket.Descricao}</td>
                  <td>{formatDate(ticket.DataChamado)}</td>
                  <td>{ticket.StatusChamado}</td>
                  <td>{ticket.Categoria}</td>
                  <td>{ticket.FK_IdUsuario}</td>
                  <td>
                    <button
                      className="button-edit-ticket"
                      onClick={() => handleEditTicket(ticket)}
                    >
                      Editar
                    </button>
                  </td>
                </tr>
              ))}
              {tickets.length === 0 && (
                <tr>
                  <td colSpan="9" style={{ textAlign: "center" }}>
                    Nenhum chamado encontrado.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>

        <button onClick={handleBack} className="button-voltar-canto">
          Voltar
        </button>
      </section>

      {showForm && (
        <section className="user-form">
          <h2>{isEditing ? "Editar Chamado" : "Criar Novo Chamado"}</h2>
          <form onSubmit={handleFormSubmit} className="form-create-user">

            <div className="form-group">
              <input
                id="titulo"
                name="Titulo"
                className="input-create-user"
                value={formData.Titulo}
                onChange={handleInputChange}
              />
              <label htmlFor="titulo">Título</label>
            </div>

            <div className="form-group">
              <select
                id="prioridade"
                name="PrioridadeChamado"
                className="input-create-user"
                value={formData.PrioridadeChamado}
                onChange={handleInputChange}
              >
                <option value="">Selecione a Prioridade</option>
                <option value="Baixa">Baixa</option>
                <option value="Média">Média</option>
                <option value="Alta">Alta</option>
              </select>
              <label htmlFor="prioridade">Prioridade:</label>
            </div>

            <div className="form-group">
              <textarea
                id="descricao"
                name="Descricao"
                className="input-create-user"
                value={formData.Descricao}
                onChange={handleInputChange}
                required
                rows={3}
              />
              <label htmlFor="descricao">Descrição</label>
            </div>

            <div className="form-group">
              <input
                id="dataChamado"
                name="DataChamado"
                className="input-create-user"
                type="date"
                value={formData.DataChamado}
                onChange={handleInputChange}
                required
              />
              <label htmlFor="dataChamado">Data do Chamado</label>
            </div>

            <div className="form-group">
              <select
                id="statusChamado"
                name="StatusChamado"
                className="input-create-user"
                value={formData.StatusChamado}
                onChange={handleInputChange}
                required
              >
                <option value="">--Selecione o Status--</option>
                <option value="Pendente">Pendente</option>
                <option value="Em Andamento">Em Andamento</option>
                <option value="Resolvido">Resolvido</option>
                <option value="Fechado">Fechado</option>
              </select>
              <label htmlFor="statusChamado">Status</label>
            </div>

            <div className="form-group">
              <select
                id="categoria"
                name="Categoria"
                className="input-create-user"
                value={formData.Categoria}
                onChange={handleInputChange}
                required
              >
                <option value="">--Selecione a Categoria--</option>
                <option value="Hardware">Hardware</option>
                <option value="Software">Software</option>
                <option value="Segurança">Segurança</option>
                <option value="Serviços">Serviços</option>
                <option value="Rede">Rede</option>
                <option value="Infraestrutura">Infraestrutura</option>
              </select>
              <label htmlFor="categoria">Categoria</label>
            </div>

            <div className="form-group">
              <input
                name="FK_IdUsuario"
                className="input-create-user"
                type="number"
                id="id"
                value={formData.FK_IdUsuario}
                onChange={handleInputChange}
                required
              />
              <label htmlFor="id">ID do usuário</label>
            </div>

            <div className="form-group">
              <select
                id="pessoasAfetadas"
                name="PessoasAfetadas"
                className="input-create-user"
                value={formData.PessoasAfetadas}
                onChange={handleInputChange}
              >
                <option value="">--Selecione--</option>
                <option value="Somente eu">Somente eu</option>
                <option value="Meu setor">Meu setor</option>
                <option value="Empresa Inteira">Empresa Inteira</option>
              </select>
              <label htmlFor="pessoasAfetadas">Pessoas Afetadas</label>
            </div>

            <div className="form-group">
            <select
              id="impedeTrabalho"
              name="ImpedeTrabalho"
              className="input-create-user"
              value={formData.ImpedeTrabalho}
              onChange={handleInputChange}
            >
              <option value="">--Selecione--</option>
              <option value="Sim">Sim</option>
              <option value="Não">Não</option>
              <option value="Parcialmente">Parcialmente</option>
            </select>
            <label htmlFor="impedeTrabalho">Impede Trabalho</label>
            </div>

            <div className="form-group">
            <select
              id="ocorreuAnteriormente"
              name="OcorreuAnteriormente"
              className="input-create-user"
              value={formData.OcorreuAnteriormente}
              onChange={handleInputChange}
            >
              <option value="">--Selecione--</option>
              <option value="Sim">Sim</option>
              <option value="Não">Não</option>
              <option value="Não sei">Não sei</option>
            </select>
            <label htmlFor="ocorreuAnteriormente">Ocorreu Anteriormente</label>
            </div>

            <button className="button-confirm-user" type="submit">
              {isEditing ? "Atualizar" : "Criar"}
            </button>
          </form>
        </section>
      )}
    </main>
  );
};

export default ManageTickets;