import { useNavigate, useLocation } from "react-router-dom";
import userIcon from "../assets/folder.svg";
import low from "../assets/low.svg";
import medium from "../assets/medium.svg";
import high from "../assets/high.svg";
import StageCharts from '../components/StageCharts';
import StageCharts2 from '../components/StageCharts2';

const AdminHome = () => {
  const location = useLocation();
  const user = location.state?.user;
  const navigate = useNavigate();

  if (!user) {
    return <p>Usuário não autenticado.</p>;
  }

  const handleManageUsers = () => {
    navigate("/manage-users", { state: { user } });
  };

  const handleManageTickets = () => {
    navigate("/manage-tickets", { state: { user } });
  };

  const handleLowTicket = () => {
    navigate("/priority-ticket", { state: { user, priority: "Baixa" } });
  };

  const handleMediumTicket = () => {
    navigate("/priority-ticket", { state: { user, priority: "Média" } });
  };

  const handleHighTicket = () => {
    navigate("/priority-ticket", { state: { user, priority: "Alta" } });
  };

  return (
    <main>
      <header className="home-header">
        <h1 className="home-welcome">Bem-vindo, {user?.Nome}</h1>
        <p className="home-role">Administrador</p>
      </header>

      <section className="box-buttons">
        <div className="chamados">
          <button className="button-chamados" onClick={handleManageUsers}>
            <img src={userIcon} alt="Gerenciar Usuários" />
            Gerenciar Usuários
          </button>
          <button className="button-chamados" onClick={handleManageTickets}>
            <img src={userIcon} alt="Gerenciar Chamados" />
            Gerenciar Chamados
          </button>
        </div>


        <div className="chamados">
          <button className="button-chamados" onClick={handleLowTicket}>
            <img src={low} alt="Pendentes" />
            Chamados Baixo
          </button>

          <button className="button-chamados" onClick={handleMediumTicket}>
            <img src={medium} alt="Em andamento" />
            Chamados Médio
          </button>

          <button className="button-chamados" onClick={handleHighTicket}>
            <img src={high} alt="Solucionados" />
            Chamados Alto
          </button>
        </div>
      </section>

      <section className="dashboard-charts">
        <div className="chart-wrapper">
          {StageCharts(user)}
        </div>

        <div>
          {StageCharts2(user)}
        </div>
      </section>
    </main>
  );
};

export default AdminHome;