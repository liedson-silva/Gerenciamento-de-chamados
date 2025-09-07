import { useNavigate, useLocation } from 'react-router-dom'

const CreateTicket = () => {
    const location = useLocation()
    const user = location.state?.user
    const navigate = useNavigate()

    const handleImpact = () => {
        navigate("/create-ticket/Impact", { state: { user } } )
    }
    return (
        <section>

            <div className='create-ticket'>
                <p className='form-ticket'>Título <span className='asterisk'>*</span></p>
                <input className='input-create-ticket' type="text" name='text' />
            </div>

            <div className='create-ticket'>
                <p className='form-ticket'>Categoria <span className='asterisk'>*</span></p>
                <select className="select-dropdown">
                    <option value="">Selecione</option>
                    <option value="Hardware">Hardware</option>
                    <option value="Segurança">Segurança</option>
                    <option value="Serviços">Serviços</option>
                    <option value="Rede">Rede</option>
                    <option value="Infraestrutura">Infraestrutura</option>
                </select>
            </div>

            <div className='create-ticket'>
                <p className='form-ticket'>Descrição do problema <span className='asterisk'>*</span></p>
                <textarea className='textarea-create-ticket' name="text"/>
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

            <div className='box-continuar-ticket'>
                <button className='button-continuar-ticket' onClick={handleImpact}>Continuar</button>
            </div>

        </section>
    )
}

export default CreateTicket