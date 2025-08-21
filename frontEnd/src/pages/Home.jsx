import logo from "../assets/logo.png"
import '../Style.css'
import { AiFillCustomerService } from "react-icons/ai";
import { BsList } from "react-icons/bs";
import { LuMessageCircleQuestion } from "react-icons/lu";
import { FaHouse } from "react-icons/fa6";
import { FaRegUserCircle } from "react-icons/fa";
import folder from "../assets/folder.svg"
import hourglass from "../assets/hourglass.svg"
import correct from "../assets/correct.svg"
import { useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";

const Home = () => {
  const location = useLocation();
  const user = location.state?.user;

  const navigate = useNavigate()

  const handleLogin = () => {
    navigate("/")
  }

  return (
    <section className="home">
      <div className="nav-bar">
        <button className="box-logo">
          <img src={logo} className="logo-home" />
        </button>
        <ul>
          <li className="utils"><AiFillCustomerService className="icons-home" /> Servicos</li>
          <li className="utils"><BsList className="icons-home" /> Meus Chamados</li>
          <li className="utils"><LuMessageCircleQuestion className="icons-home" /> FAQ</li>
        </ul>
      </div>

      <div className="header">
        <div className="box-inicio">
          <button className="button-inicio"><FaHouse className="icons-home" /> Home</button>
          <div className="user">
            <p className="name">{user?.name}</p>
            <div className="dropdown" tabIndex={0}>
            <button className="button-user"><FaRegUserCircle /></button>
            <div className="dropdown-menu">
              <ul>
                <li className="menu">Minha Conta</li>
                <li onClick={handleLogin} className="menu">Sair</li>
              </ul>
            </div>
            </div>
          </div>
        </div>

        <div className="content">
          <h1 className="text-bv">Bem-vindo, {user?.name}!</h1>
          <div className="box-buttons">
            <div className="box-button-chamado">
              <button className="button-criar-chamado">Criar chamado</button>
            </div>

            <div className="chamados">
              <button className="button-chamados"><img src={folder} />
                Chamados Pendentes</button>
              <button className="button-chamados"><img src={hourglass} />
                Chamados em Andamentos</button>
              <button className="button-chamados"><img src={correct} />
                Chamados Solucionados</button>
            </div>
          </div>

        </div>

      </div>

    </section>
    
  )
}

export default Home