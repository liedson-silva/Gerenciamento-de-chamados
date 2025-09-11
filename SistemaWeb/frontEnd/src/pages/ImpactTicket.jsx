import { useNavigate, useLocation } from "react-router-dom"

const ImpactTicket = () => {
    const location = useLocation()
    const user = location.state?.user
    const navigate = useNavigate()

    const handleSuccessTicket = () => {
        navigate("/create-ticket/impact/success", { state: { user }} )
    }

  return (
    <section>

            <div className='create-ticket'>
                <p className='form-ticket'>Quais pessoas são afetadas ? <span className='asterisk'>*</span></p>
                <select className="select-dropdown">
                    <option value="">Selecione</option>
                    <option value="Somente eu">Somente eu</option>
                    <option value="Meu setor">Meu setor</option>
                    <option value="Empresa inteira">Empresa inteira</option>
                </select>
            </div>

            <div className='create-ticket'>
                <p className='form-ticket'>Esse problema está impedindo meu trabalho ? <span className='asterisk'>*</span></p>
                <select className="select-dropdown">
                    <option value="">Selecione</option>
                    <option value="Sim">Sim</option>
                    <option value="Não">Não</option>
                    <option value="Parcialmente">Parcialmente</option>
                </select>
            </div>

            <div className='create-ticket'>
                <p className='form-ticket'>Já ocorreu anteriormente ? <span className='asterisk'>*</span></p>
                <select className="select-dropdown">
                    <option value="">Selecione</option>
                    <option value="Sim">Sim</option>
                    <option value="Não">Não</option>
                    <option value="Parcialmente">Não sei</option>
                </select>
            </div>

            <div className='create-ticket'>
                <p className="info-impact">*Esta seção está sendo preenchida com apoio da inteligência artificial*</p>
            </div>

            <div className='box-enviar-ticket'>
                <button className='button-enviar-ticket' onClick={handleSuccessTicket}>Enviar</button>
            </div>

        </section>
  )
}

export default ImpactTicket