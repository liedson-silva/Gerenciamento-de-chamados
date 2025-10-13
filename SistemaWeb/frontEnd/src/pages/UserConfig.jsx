import { useLocation } from 'react-router-dom'
import { formatDate } from '../components/FormatDate'

const UserConfig = () => {
    const location = useLocation()
    const user = location.state?.user
    return (
        <section>
            <h1>Minhas Configurações</h1>
            <div className='user-config-data'>
                <div>
                    <p className='user-config'>Nome:</p>
                    <p className='user-config'>Email:</p>
                    <p className='user-config'>Função:</p>
                    <p className='user-config'>Setor:</p>
                    <p className='user-config'>Login:</p>
                    <p className='user-config'>Sexo:</p>
                    <p className='user-config'>Data:</p>
                </div>
                <div>
                    <div className='user-data'>{user?.Nome}</div>
                    <div className='user-data'>{user?.Email}</div>
                    <div className='user-data'>{user?.FuncaoUsuario}</div>
                    <div className='user-data'>{user?.Setor}</div>
                    <div className='user-data'>{user?.Login}</div>
                    <div className='user-data'>{user?.Sexo}</div>
                    <div className='user-data'>{formatDate(user?.DataDeNascimento)}</div>
                </div>
            </div>
        </section>
    )
}

export default UserConfig