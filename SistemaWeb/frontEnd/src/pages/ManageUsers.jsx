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
    console.log("Objeto User recebido:", user)
    setFormData({
      name: user.Nome,
      cpf: user.CPF,
      rg: user.RG,
      functionUser: user.FuncaoUsuario,
      sex: user.Sexo,
      sector: user.Setor,
      date: user.DataDeNascimento,
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
    <main className="scroll-list">
      <h1>Gerenciar Usuários</h1>

      {errorMessage && <p className="notice">{errorMessage}</p>}
      {successMessage && <p className="success">{successMessage}</p>}

      <button onClick={handleShowCreateForm} className="button-create-ticket">Criar Novo Usuário</button>

      <section className="user-list">
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
                    <button className="button-edit-ticket" onClick={() => handleEditUser(user)}>Editar</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </section>

      {showForm && (
        <section className="user-form">
          <h2>{isEditing ? "Editar Usuário" : "Criar Novo Usuário"}</h2>
          <form onSubmit={handleFormSubmit} className="form-create-user">
            <input name="name" className="input-create-user" placeholder="Nome" value={formData.name} onChange={handleInputChange} required />
            <input name="cpf" className="input-create-user" placeholder="CPF: xxx.xxx.xxx-xx" value={formData.cpf} onChange={handleInputChange} required />
            <input name="rg" type="rg" className="input-create-user" placeholder="RG: xx.xxx.xxx-x" value={formData.rg} onChange={handleInputChange} required />
            <select name="functionUser" className="input-create-user" value={formData.functionUser}
              onChange={handleInputChange} required>
              <option value="">Função</option>
              <option value="Admin">Admin</option>
              <option value="Tecnico">Técnico</option>
              <option value="Funcionario">Funcionário</option>
            </select >
            <input name="email" className="input-create-user" type="email" placeholder="Email" value={formData.email} onChange={handleInputChange} required />
            <select name="sex" className="input-create-user" value={formData.sex}
              onChange={handleInputChange} required>
              <option value="">Sexo</option>
              <option value="Masculino">Masculino</option>
              <option value="Feminino">Feminino</option>
              <option value="Outros">Outros</option>
            </select >
            <input name="date" className="input-create-user" type="date" value={formData.date} onChange={handleInputChange} />
            <select name="sector" className="input-create-user" value={formData.sector}
              onChange={handleInputChange} required>
              <option value="">Setor</option>
              <option value="RH">RH</option>
              <option value="Financeiro">Financeiro</option>
            </select >
            <input name="login" className="input-create-user" placeholder="Login" value={formData.login} onChange={handleInputChange} required />
            {!isEditing && (<input name="password" className="input-create-user" type="password" placeholder="Senha" value={formData.password} onChange={handleInputChange} required />)}
            <button className="button-confirm-user" type="submit">{isEditing ? "Salvar Alterações" : "Criar Usuário"}</button>
          </form>
        </section>
      )}
    </main>
  );
};

export default ManageUsers;