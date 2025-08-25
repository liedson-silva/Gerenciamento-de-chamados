import { useLocation } from 'react-router-dom'

const UserConfig = () => {
    const location = useLocation()
    const user = location.state?.user
    return (
        <div>
            <h1>Minhas Configurações</h1>
            <div>
                <p className='user-config'>Usuário: <div className='user-data'>{user?.name}</div></p>
                <p className='user-config'>Senha: <span className='user-data'>{"*".repeat(user?.password?.length || 0)}</span></p>
            </div>
        </div>
    )
}

export default UserConfig