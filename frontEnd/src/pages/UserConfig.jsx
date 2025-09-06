import { useLocation } from 'react-router-dom'

const UserConfig = () => {
    const location = useLocation()
    const user = location.state?.user
    return (
        <section>
            <h1>Minhas Configurações</h1>
            <div className='user-config-data'>
                <div>
                    <p className='user-config'>Usuário:</p>
                    <p className='user-config'>Senha:</p>
                </div>
                <div>
                    <div className='user-data'>{user?.name}</div>
                    <div className='user-data'>{"*".repeat(user?.password?.length || 0)}</div>
                </div>
            </div>
        </section>
    )
}

export default UserConfig