import { useNavigate, useLocation } from "react-router-dom";
import userIcon from "../assets/folder.svg";
import ticketIcon from "../assets/folder.svg";
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

  const handleAllTickets = () => {
    navigate("/tickets", { state: { user } });
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

          <button className="button-chamados" onClick={handleAllTickets}>
            <img src={ticketIcon} alt="Gerenciar Chamados" />
            Gerenciar Chamados
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