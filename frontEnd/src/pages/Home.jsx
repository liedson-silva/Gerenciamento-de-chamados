import '../Style.css'
import folder from "../assets/folder.svg"
import hourglass from "../assets/hourglass.svg"
import correct from "../assets/correct.svg"
import { useLocation } from "react-router-dom"

const Home = () => {
  const location = useLocation()
  const user = location.state?.user

  return (
    <section>
      <h1>Bem-vindo, {user?.name}!</h1>
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
    </section>
  )
}

export default Home