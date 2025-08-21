import './Login.css'
import logo from '../assets/logo.png'
import { FaRegUser } from "react-icons/fa";
import { TbLockPassword } from "react-icons/tb";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const navigate = useNavigate()

  const handleLogin = () => {
    navigate("/home")
  }

  return (
    <div className='login'>
          <div>
            <img src={logo} alt="logo Fatal-System" className='logo' />
          </div>
          <div className="box-login">
            <h1>Login</h1>
            <div className='form'>
              <div className='inputs'>
                <FaRegUser className='icons' />
                <input type="email" placeholder='Email' />
              </div>
              <div className='inputs'>
                <TbLockPassword className='icons' />
                <input type="password" placeholder='Senha' />
              </div>
              <button className='forget-password'>Esqueceu a senha?</button>
              <div className='button-enter'>
                <button className='enter' onClick={handleLogin}>Entrar</button>
              </div>
            </div>
          </div>
        </div>
  )
}

export default Login