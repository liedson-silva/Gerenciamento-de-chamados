import { useNavigate, useLocation } from 'react-router-dom'
import { useState } from 'react'
import api from '../services/api'

const CreateTicket = () => {
    const [title, setTitle] = useState("")
    const [category, setCategory] = useState("")
    const [description, setDescription] = useState("")
    const [affectedPeople, setAffectedPeople] = useState("")
    const [stopWork, setStopWork] = useState("")
    const [happenedBefore, setHappenedBefore] = useState("")
    const [showImpactSection, setShowImpactSection] = useState(false)
    const [showCreateSection, setShowCreateSection] = useState(true)
    const [error, setError] = useState("")
    const location = useLocation()
    const user = location.state?.user
    const navigate = useNavigate()

    const handleImpact = () => {
        if (title.trim() === "" || category === "" || description.trim() === "") {
            setError("Por favor, preencha todos os campos obrigatórios (*).")
            setTimeout(() => {
                setError("")
            }, 3000)
            return
        }
        setShowImpactSection(true)
        setShowCreateSection(false)
    }

    const handleCreateTicket = () => {
        setShowImpactSection(false)
        setShowCreateSection(true)
    }

    const handleHome = () => {
        if (user.FuncaoUsuario === "Admin") {
            navigate("/admin-home", { state: { user } });
        } else if (user.FuncaoUsuario === "Tecnico") {
            navigate("/tec-home", { state: { user } });
        } else {
            navigate("/home", { state: { user } });
        }
    }

    const handleSuccessTicket = async () => {
        try {
            const { data } = await api.post("/create-ticket", { title, description, category, userId: user?.IdUsuario, affectedPeople, stopWork, happenedBefore })

            if (data.success) {
                navigate("/create-ticket/success", { state: { user, ticket: data.ticket } })
            }
        } catch (err) {
            setError("Erro ao criar chamado");
            setTimeout(() => setError(""), 2000);
        }
    }

    return (
        <main>
            {showCreateSection && (
                <section>

                    <div className='create-ticket'>
                        <p className='form-ticket'>Título <span className='asterisk'>*</span></p>
                        <input
                            className='input-create-ticket'
                            type="text"
                            name='text'
                            value={title}
                            onChange={(e) => setTitle(e.target.value)}
                        />
                    </div>

                    <div className='create-ticket'>
                        <p className='form-ticket'>Categoria <span className='asterisk'>*</span></p>
                        <select
                            className="select-dropdown"
                            value={category}
                            onChange={(e) => setCategory(e.target.value)}
                        >
                            <option value="">Selecione</option>
                            <option value="Hardware">Hardware</option>
                            <option value="Software">Software</option>
                            <option value="Segurança">Segurança</option>
                            <option value="Serviços">Serviços</option>
                            <option value="Rede">Rede</option>
                            <option value="Infraestrutura">Infraestrutura</option>
                            <option value="Outros">Outros</option>
                        </select>
                    </div>

                    <div className='create-ticket'>
                        <p className='form-ticket'>Descrição do problema <span className='asterisk'>*</span></p>
                        <textarea
                            className='textarea-create-ticket'
                            name="text"
                            value={description}
                            onChange={(e) => setDescription(e.target.value)}
                        />
                    </div>

                    {error && <p className="notice">{error}</p>}

                    <div className='box-buttons-ticket'>
                        <button className='button-back-ticket' onClick={handleHome}>Voltar</button>

                        <button className='button-continuar-ticket' onClick={handleImpact}>Continuar</button>
                    </div>

                </section>
            )}

            {showImpactSection && (
                <section>

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
                </section>
            )}
        </main>)
}

export default CreateTicket