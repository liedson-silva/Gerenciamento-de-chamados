import { useEffect, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import api from "../services/api";
import "../Style.css";

const ManageUsers = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const user = location.state?.user;

  if (!user) {
    return <p>Usuário não autenticado.</p>;
  }

  const [users, setUsers] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [selectedUserId, setSelectedUserId] = useState(null);
  const [formData, setFormData] = useState({
    name: "",
    cpf: "",
    rg: "",
    functionUser: "",
    sex: "",
    sector: "",
    date: "",
    email: "",
    password: "",
    login: "",
  });

  const [successMessage, setSuccessMessage] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const response = await api.get("/users");
      if (response.data.success) {
        setUsers(response.data.users);
      } else {
        setErrorMessage("Erro ao carregar usuários.");
      }
    } catch (error) {
      console.error("Erro ao buscar usuários:", error);
      setErrorMessage("Erro ao buscar usuários.");
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleShowCreateForm = () => {
    setFormData({
      name: "",
      cpf: "",
      rg: "",
      functionUser: "",
      sex: "",
      sector: "",
      date: "",
      email: "",
      password: "",
      login: "",
    });
    setIsEditing(false);
    setSelectedUserId(null);
    setShowForm(true);
  };

  const handleEditUser = (user) => {
    setFormData({
      name: user.Nome,
      cpf: user.CPF,
      rg: user.RG,
      functionUser: user.FuncaoUsuario,
      sex: user.Sexo,
      sector: user.Setor,
      date: user.DataDeNascimento ? user.DataDeNascimento.slice(0, 10) : "",
      email: user.Email,
      password: "", // vazio por segurança, senha só muda se preencher
      login: user.Login,
    });
    setSelectedUserId(user.IdUsuario);
    setIsEditing(true);
    setShowForm(true);
  };

  const handleFormSubmit = async (e) => {
    e.preventDefault();

    try {
      // Cria uma cópia para ajustar dados antes de enviar
      const dataToSend = { ...formData };

      // Se estiver editando e a senha estiver vazia, remove para não atualizar
      if (isEditing && !dataToSend.password) {
        delete dataToSend.password;
      }

      if (isEditing) {
        const response = await api.put(`/update-user/${selectedUserId}`, dataToSend);
        if (response.data.success) {
          setSuccessMessage("Usuário atualizado com sucesso!");
        } else {
          setErrorMessage("Erro ao atualizar usuário.");
        }
      } else {
        const response = await api.post("/create-user", dataToSend);
        if (response.data.success) {
          setSuccessMessage("Usuário criado com sucesso!");
        } else {
          setErrorMessage("Erro ao criar usuário.");
        }
      }
      setShowForm(false);
      fetchUsers();
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
      <h1>Gerenciar Usuários</h1>

      {errorMessage && <p className="notice">{errorMessage}</p>}
      {successMessage && <p className="success">{successMessage}</p>}

      <button onClick={handleShowCreateForm} className="button-create-ticket">
        Criar Novo Usuário
      </button>

      <section className="user-list" style={{ position: "relative", paddingBottom: "70px" }}>
        <h2>Usuários Cadastrados</h2>
        <div className="scroll-lists">
          <table className="user-table">
            <thead>
              <tr>
                <th>ID</th>
                <th>Nome</th>
                <th>Login</th>
                <th>Função</th>
                <th>Email</th>
                <th>Setor</th>
                <th>Ações</th>
              </tr>
            </thead>
            <tbody>
              {users.map((user) => (
                <tr key={user.IdUsuario}>
                  <td>{user.IdUsuario}</td>
                  <td>{user.Nome}</td>
                  <td>{user.Login}</td>
                  <td>{user.FuncaoUsuario}</td>
                  <td>{user.Email}</td>
                  <td>{user.Setor}</td>
                  <td>
                    <button className="button-edit-ticket" onClick={() => handleEditUser(user)}>
                      Editar
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
        <button onClick={handleBack} className="button-voltar-canto">
          Voltar
        </button>
      </section>

      {showForm && (
        <section className="user-form">
          <h2>{isEditing ? "Editar Usuário" : "Criar Novo Usuário"}</h2>
          <form onSubmit={handleFormSubmit} className="form-create-user">
            <div className="form-group">
              <input
                name="name"
                className="input-create-user"
                id="name"
                value={formData.name}
                onChange={handleInputChange}
                required
              />
              <label htmlFor="name">Nome</label>
            </div>

            <div className="form-group">
              <input
                name="cpf"
                id="cpf"
                className="input-create-user"
                value={formData.cpf}
                onChange={handleInputChange}
                required
              />
              <label htmlFor="cpf">CPF: xxx.xxx.xxx-xx</label>
            </div>

            <div className="form-group">
              <input
                name="rg"
                id="rg"
                type="rg"
                className="input-create-user"
                value={formData.rg}
                onChange={handleInputChange}
                required
              />
              <label htmlFor="rg">RG: xx.xxx.xxx-x</label>
            </div>

            <div className="form-group">
              <select
                name="functionUser"
                id="functionUser"
                className="input-create-user"
                value={formData.functionUser}
                onChange={handleInputChange}
                required
              >
                <option value="Funcionario">Funcionário</option>
                <option value="Admin">Admin</option>
                <option value="Tecnico">Técnico</option>
              </select>
              <label htmlFor="functionUser">Função</label>
            </div>

            <div className="form-group">
              <input
                name="email"
                id="email"
                className="input-create-user"
                type="email"
                value={formData.email}
                onChange={handleInputChange}
                required
              />
              <label htmlFor="email">Email</label>
            </div>

            <div className="form-group"><select
              name="sex"
              id="sex"
              className="input-create-user"
              value={formData.sex}
              onChange={handleInputChange}
              required
            >
              <option value="Masculino">Masculino</option>
              <option value="Feminino">Feminino</option>
              <option value="Outros">Outros</option>
            </select>
              <label htmlFor="sex">Sexo</label>
            </div>

            <div className="form-group">
              <input
                name="date"
                id="date"
                className="input-create-user"
                type="date"
                value={formData.date}
                onChange={handleInputChange}
              />
              <label htmlFor="date">Data do nascimento</label>
            </div>

            <div className="form-group">
              <select
                name="sector"
                id="sector"
                className="input-create-user"
                value={formData.sector}
                onChange={handleInputChange}
                required
              >
                <option value="RH">RH</option>
                <option value="Financeiro">Financeiro</option>
              </select>
              <label htmlFor="sector">setor</label>
            </div>

            <div className="form-group">
              <input
                name="login"
                id="login"
                className="input-create-user"
                value={formData.login}
                onChange={handleInputChange}
                required
              />
              <label htmlFor="login">Login</label>
            </div>
            <div className="form-group">
              <input
                name="password"
                className="input-create-user"
                type="password"
                id="senha"
                placeholder={isEditing && "Senha (deixe vazio para não alterar)"}
                value={formData.password}
                onChange={handleInputChange}
                required={!isEditing} // obrigatório só na criação
              />
              <label htmlFor="senha">Senha</label>
            </div>

            <button className="button-confirm-user" type="submit">
              {isEditing ? "Salvar Alterações" : "Criar Usuário"}
            </button>
          </form>
        </section>
      )}
    </main>
  );
};

export default ManageUsers;