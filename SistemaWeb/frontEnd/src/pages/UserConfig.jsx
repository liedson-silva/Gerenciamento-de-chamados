import { useLocation } from 'react-router-dom'

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
                </div>
                <div>
                    <div className='user-data'>{user?.Nome}</div>
                    <div className='user-data'>{user?.Email}</div>
                </div>
            </div>
        </section>
    )
}

export default UserConfig