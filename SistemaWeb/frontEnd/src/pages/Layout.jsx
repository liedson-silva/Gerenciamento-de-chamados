import logo from "../assets/logo.png";
import { BsList } from "react-icons/bs";
import { LuMessageCircleQuestion } from "react-icons/lu";
import { FaHouse } from "react-icons/fa6";
import { IoExitOutline } from "react-icons/io5";
import { FaRegUserCircle } from "react-icons/fa";
import { Outlet, useLocation, useNavigate } from "react-router-dom";

const Home = () => {
  const location = useLocation();
  const user = location.state?.user;
  const navigate = useNavigate();

  const handleHome = () => {
    if (user.FuncaoUsuario === "Admin") {
      navigate("/admin-home", { state: { user } });
    } else if (user.FuncaoUsuario === "Tecnico") {
      navigate("/tec-home", { state: { user } });
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

  const handleAllTicket = () => {
    navigate("/tickets", { state: { user } });
  };

  const handleFAQ = () => {
    navigate("/faq", { state: { user } });
  };

  return (
    <main className="app-layout">
      <nav>
        <button className="nav-logo-link" onClick={handleHome}>
          <img src={logo} className="nav-logo-img" alt="logo" />
        </button>
        <ul>
          <li className="nav-item" onClick={handleUserConfig}>
            <FaRegUserCircle className="nav-item-icon" /> Minha Conta
          </li>
          <li className="nav-item" onClick={handleAllTicket}>
            <BsList className="nav-item-icon" /> Chamados
          </li>
          <li className="nav-item" onClick={handleFAQ}>
            <LuMessageCircleQuestion className="nav-item-icon" /> FAQ
          </li>
          <li className="nav-item" onClick={handleLogin}>
            <IoExitOutline className="nav-item-icon" /> Sair
          </li>
        </ul>
      </nav>

      <div className="main-header">
        <div className="header-content-wrapper">
          <button className="header-home-btn" onClick={handleHome}>
            <FaHouse className="nav-item-icon" /> Início
          </button>

          <figure className="header-mobile-home" onClick={handleHome}>
            <img src={logo} className="header-mobile-logo" alt="logo" />
          </figure>

          <div className="dropdown-mobile" tabIndex={0}>
            <button className="menu-mobile"><BsList /></button>
            <ul className="dropdown-menu">
              <li onClick={handleUserConfig} className="menu">Minha Conta</li>
              <li onClick={handleAllTicket} className="menu">Chamados</li>
              <li onClick={handleFAQ} className="menu">FAQ</li>
              <li onClick={handleLogin} className="menu">Sair</li>
            </ul>
          </div>

          <header className="user">
            <p className="name">{user?.Nome}</p>
            <div className="dropdown" tabIndex={0}>
              <button className="button-user"><FaRegUserCircle /></button>
              <ul className="dropdown-menu">
                <li onClick={handleUserConfig} className="menu">Minha Conta</li>
                <li onClick={handleLogin} className="menu">Sair</li>
              </ul>
            </div>
          </header>
        </div>

        <main className="content">
          <Outlet />
        </main>
      </div>
    </main>
  );
};

export default Home;