import { useLocation, useNavigate } from "react-router-dom";
import { useEffect } from "react";

const ManageUsers = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const user = location.state?.user;

  useEffect(() => {
    console.log("Usuário recebido em ManageUsers:", user);
  }, [user]);

  const isAdmin = user?.FuncaoUsuario?.trim().toLowerCase() === "admin";

  if (!user) {
    return <p>Usuário não autenticado.</p>;
  }

  if (!isAdmin) {
    return (
      <main>
        <h1>Acesso Negado</h1>
        <p style={{ marginLeft: "40px" }}>
          Apenas administradores podem acessar esta página.
        </p>
        <div className="box-button-chamado">
          <button className="button-criar-chamado" onClick={() => navigate(-1)}>
            Voltar
          </button>
        </div>
      </main>
    );
  }

  return (
    <main>
      <h1>Gerenciar Usuários</h1>
      {/* conteúdo a ser inserido ainda */}
      <p>Olá, {user.Nome}! Gerenciar os usuários.</p>
    </main>
  );
};

export default ManageUsers;