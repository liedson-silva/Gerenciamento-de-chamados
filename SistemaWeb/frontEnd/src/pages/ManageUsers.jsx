import { useEffect, useState } from "react";
import api from "../services/api";
import "../Style.css";

const ManageUsers = () => {
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
      cpf: "", 
      rg: "", 
      functionUser: user.FuncaoUsuario,
      sex: "", 
      sector: user.Setor,
      date: "", 
      email: user.Email,
      password: "", // vazio por segurança
      login: user.Login,
    });
    setSelectedUserId(user.IdUsuario);
    setIsEditing(true);
    setShowForm(true);
  };

  const handleFormSubmit = async (e) => {
    e.preventDefault();

    try {
      if (isEditing) {
        const response = await api.put(`/update-user/${selectedUserId}`, formData);
        if (response.data.success) {
          setSuccessMessage("Usuário atualizado com sucesso!");
        } else {
          setErrorMessage("Erro ao atualizar usuário.");
        }
      } else {
        const response = await api.post("/create-user", formData);
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

  return (
    <main className="manage-users">
      <h1>Gerenciar Usuários</h1>

      {errorMessage && <p className="error">{errorMessage}</p>}
      {successMessage && <p className="success">{successMessage}</p>}

      <button onClick={handleShowCreateForm}>Criar Novo Usuário</button>

      <section className="user-list">
        <h2>Usuários Cadastrados</h2>
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
                  <button onClick={() => handleEditUser(user)}>Editar</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </section>

      {showForm && (
        <section className="user-form">
          <h2>{isEditing ? "Editar Usuário" : "Criar Novo Usuário"}</h2>
          <form onSubmit={handleFormSubmit} className="form-create-user">
            <input name="name" placeholder="Nome" value={formData.name} onChange={handleInputChange} required />
            <input name="cpf" placeholder="CPF" value={formData.cpf} onChange={handleInputChange} />
            <input name="rg" placeholder="RG" value={formData.rg} onChange={handleInputChange} />
            <input name="functionUser" placeholder="Função" value={formData.functionUser} onChange={handleInputChange} required />
            <input name="sex" placeholder="Sexo" value={formData.sex} onChange={handleInputChange} />
            <input name="sector" placeholder="Setor" value={formData.sector} onChange={handleInputChange} required />
            <input name="date" type="date" value={formData.date} onChange={handleInputChange} />
            <input name="email" type="email" placeholder="Email" value={formData.email} onChange={handleInputChange} required />
            <input name="login" placeholder="Login" value={formData.login} onChange={handleInputChange} required />
            {!isEditing && (
              <input name="password" type="password" placeholder="Senha" value={formData.password} onChange={handleInputChange} required />
            )}
            <button type="submit">{isEditing ? "Salvar Alterações" : "Criar Usuário"}</button>
          </form>
        </section>
      )}
    </main>
  );
};

export default ManageUsers;