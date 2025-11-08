import { useNavigate, useLocation } from "react-router-dom"
import { useState } from "react"
import api from "../services/api"

const ImpactTicket = () => {
    const [affectedPeople, setAffectedPeople] = useState("")
    const [stopWork, setStopWork] = useState("")
    const [happenedBefore, setHappenedBefore] = useState("")
    const [error, setError] = useState("");
    const location = useLocation()
    const { user, title, category, description } = location.state || {}
    const navigate = useNavigate()

    const handleCreateTicket = () => {
        navigate("/create-ticket", { state: { user } })
    }

    const handleSuccessTicket = async () => {
        try {
            const { data } = await api.post("/create-ticket", { title, description, category, userId: user?.IdUsuario, affectedPeople, stopWork, happenedBefore })

            if (data.success) {
                navigate("/create-ticket/impact/success", { state: { user, ticket: data.ticket } })
            }
        } catch (err) {
            setError("Campos obrigatórios faltando!");
            setTimeout(() => setError(""), 2000);
        }
    }

    return (
        <main>

            <div className='create-ticket'>
                <p className='form-ticket'>Quais pessoas são afetadas ? <span className='asterisk'>*</span></p>
                <select
                    className="select-dropdown"
                    value={affectedPeople}
                    onChange={(e) => setAffectedPeople(e.target.value)}
                >
                    <option value="">Selecione</option>
                    <option value="Somente eu">Somente eu</option>
                    <option value="Meu setor">Meu setor</option>
                    <option value="Empresa inteira">Empresa inteira</option>
                </select>
            </div>

            <div className='create-ticket'>
                <p className='form-ticket'>Esse problema está impedindo meu trabalho ? <span className='asterisk'>*</span></p>
                <select
                    className="select-dropdown"
                    value={stopWork}
                    onChange={(e) => setStopWork(e.target.value)}
                >
                    <option value="">Selecione</option>
                    <option value="Sim">Sim</option>
                    <option value="Não">Não</option>
                    <option value="Parcialmente">Parcialmente</option>
                </select>
            </div>

            <div className='create-ticket'>
                <p className='form-ticket'>Já ocorreu anteriormente ? <span className='asterisk'>*</span></p>
                <select
                    className="select-dropdown"
                    value={happenedBefore}
                    onChange={(e) => setHappenedBefore(e.target.value)}
                >
                    <option value="">Selecione</option>
                    <option value="Sim">Sim</option>
                    <option value="Não">Não</option>
                    <option value="Não sei">Não sei</option>
                </select>
            </div>

            <div className='create-ticket'>
                <p className="info-impact">*Esta seção está sendo preenchida com apoio da inteligência artificial*</p>
            </div>

            <div className="box-impact-buttons">
                <button className='button-voltar-ticket' onClick={handleCreateTicket}>Voltar</button>

                <button className='button-enviar-ticket' onClick={handleSuccessTicket}>Enviar</button>
            </div>
            {error && <p className="notice">{error}</p>}
        </main>
    )
}

export default ImpactTicket