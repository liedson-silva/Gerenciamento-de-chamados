import { useNavigate, useLocation } from 'react-router-dom'
import { useState } from 'react'

const CreateTicket = () => {
    const [title, setTitle] = useState("")
    const [category, setCategory] = useState("")
    const [description, setDescription] = useState("")
    const location = useLocation()
    const user = location.state?.user
    const navigate = useNavigate()

    const handleImpact = () => {
        navigate("/create-ticket/Impact", { state: { user, title, category, description } })
    }

    const handleHome = () => {
        if (user.FuncaoUsuario === "Admin") {
            navigate("/admin-home", { state: { user } });
        } else if (user.FuncaoUsuario === "Tecnico") {
            navigate("/tec-home", { state: { user } });
        } else {
            navigate("/home", { state: { user } });
        }
    };

    return (
        <main>

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

            <div className='add-file'>
                <p className='form-ticket'>Adicionar arquivos</p>
                <div className='form-file'>
                    <div>
                        <p className='form-add-file'>Nome do arquivo</p>
                        <input className='input-add-file' type="text" name="text" />
                    </div>
                    <div>
                        <p className='form-add-file'>Anexo</p>
                        <input className='input-add-file' type="file" name='file' />
                    </div>
                </div>
            </div>

            <div className='box-buttons-ticket'>
                <button className='button-back-ticket' onClick={handleHome}>Voltar</button>

                <button className='button-continuar-ticket' onClick={handleImpact}>Continuar</button>
            </div>

        </main>
    )
}

export default CreateTicket