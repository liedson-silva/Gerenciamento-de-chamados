import { useLocation, useNavigate } from 'react-router-dom';
import low from "../assets/low.svg";
import medium from "../assets/medium.svg";
import high from "../assets/high.svg";
import StageCharts from '../components/StageCharts';
import StageCharts2 from '../components/StageCharts2';
 
const TecHome = () => {
  const location = useLocation();
  const user = location.state?.user;
  const navigate = useNavigate();

  const handleReplyTicket = () => {
    navigate("/reply-ticket", { state: { user } });
  }

  const handleCreateTicket = () => {
    navigate("/create-ticket", { state: { user } });
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

  if (!user) {
    return <p>Usuário não autenticado.</p>;
  }

  return (
    <main>
      <header className="home-header">
        <h1 className="home-welcome">Bem-vindo, {user?.Nome}</h1>
        <p className="home-role">Técnico</p>
      </header>

      <section className="box-buttons">
        <div className="box-tec-button">
          <button className="button-answer-ticket" onClick={handleReplyTicket}>
            Responder chamado
          </button>
          <button className="button-criar-chamado" onClick={handleCreateTicket}>
            Criar chamado
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
            Chamados Altos
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

export default TecHome;