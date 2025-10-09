import logo from "../assets/logo.png";
import { BsList } from "react-icons/bs";
import { LuMessageCircleQuestion } from "react-icons/lu";
import { FaHouse } from "react-icons/fa6";
import { FaRegUserCircle } from "react-icons/fa";
import { Outlet, useLocation, useNavigate } from "react-router-dom";

const Home = () => {
  const location = useLocation();
  const user = location.state?.user;
  const navigate = useNavigate();

  // 👇 Redireciona para a home correta (admin ou normal)
  const handleHome = () => {
    if (user?.FuncaoUsuario === "admin") {
      navigate("/admin-home", { state: { user } });
    } else {
      navigate("/home", { state: { user } });
    }
  };

  const handleLogin = () => {
    navigate("/");
  };

  const handleUserConfig = () => {
    navigate("/user-configuration", { state: { user } });
  };

  const handlePendingTicket = () => {
    navigate("/tickets", { state: { user } });
  };

  const handleFAQ = () => {
    navigate("/faq", { state: { user } });
  };

  return (
    <section className="home">
      <div className="nav-bar">
        <button className="box-logo" onClick={handleHome}>
          <img src={logo} className="logo-home" alt="logo" />
        </button>
        <ul>
          <li className="utils" onClick={handlePendingTicket}>
            <BsList className="icons-home" /> Chamados
          </li>
          <li className="utils" onClick={handleFAQ}>
            <LuMessageCircleQuestion className="icons-home" /> FAQ
          </li>
        </ul>
      </div>

      <div className="header">
        <div className="box-inicio">
          <button className="button-inicio" onClick={handleHome}>
            <FaHouse className="icons-home" /> Início
          </button>

          <div className="home-mobile" onClick={handleHome}>
            <img src={logo} className="logo-mobile" alt="logo" />
          </div>

          <div className="dropdown-mobile" tabIndex={0}>
            <button className="menu-mobile"><BsList /></button>
            <div className="dropdown-menu">
              <ul>
                <li onClick={handleUserConfig} className="menu">Minha Conta</li>
                <li onClick={handlePendingTicket} className="menu">Chamados</li>
                <li onClick={handleFAQ} className="menu">FAQ</li>
                <li onClick={handleLogin} className="menu">Sair</li>
              </ul>
            </div>
          </div>

          <div className="user">
            <p className="name">{user?.Nome}</p>
            <div className="dropdown" tabIndex={0}>
              <button className="button-user"><FaRegUserCircle /></button>
              <div className="dropdown-menu">
                <ul>
                  <li onClick={handleUserConfig} className="menu">Minha Conta</li>
                  <li onClick={handleLogin} className="menu">Sair</li>
                </ul>
              </div>
            </div>
          </div>
        </div>

        <div className="content">
          <Outlet />
        </div>
      </div>
    </section>
  );
};

export default Home;