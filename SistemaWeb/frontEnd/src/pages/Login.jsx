import { useState } from 'react';
import '../Style.css'
import logo from '../assets/logo.png'
import { FaRegUser } from "react-icons/fa";
import { TbLockPassword } from "react-icons/tb";
import { useNavigate } from "react-router-dom";
import api from "../services/api.js"

const Login = () => {
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [forgetPassword, setForgetPassword] = useState("")
  const navigate = useNavigate()

  const handleLogin = async () => {
    try {
      const response = await api.post("/login", { login, password })

      if (response.data.success) {
        const user = response.data.user;

        if (user.FuncaoUsuario === "Admin") {
          navigate("/admin-home", { state: { user } });
        } else if (user.FuncaoUsuario === "Tecnico") {
          navigate("/tec-home", { state: { user } });
        } else {
          navigate("/home", { state: { user } });
        }
      }
    } catch (err) {
      setError("Usuário ou senha inválidos!");
      setLogin("");
      setPassword("");
      setTimeout(() => setError(""), 2000);
    }
  }

  const textForgetPassword = () => {
    setForgetPassword("Entre em contato com o administrador para recuperar a senha")
    setTimeout(() => setForgetPassword(""), 2000);
  }

  return (
    <main className='login'>
      <figure>
        <img src={logo} alt="logo Fatal-System" className='logo-login' />
      </figure>
      <section className="box-login">
        <h1 className='text-login'>Login</h1>
        <div className='form'>

          <div className='inputs'>
            <FaRegUser className='icons' />
            <input
              className='input-login'
              name='login'
              type="text"
              placeholder='Usuário'
              value={login}
              onChange={(e) => setLogin(e.target.value)}
            />
          </div>

          <div className='inputs'>
            <TbLockPassword className='icons' />
            <input
              className='input-login'
              name='password'
              type="password"
              placeholder='Senha'
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              onKeyDown={e => e.key === "Enter" && handleLogin()}
            />
          </div>

          {error && <p className="notice">{error}</p>}
          {forgetPassword && <p className='notice'>{forgetPassword}</p>}

          <button className='forget-password' onClick={textForgetPassword}>
            Esqueceu a senha?
          </button>

          <div className='button-enter'>
            <button className='enter' type='submit' onClick={handleLogin}>Entrar</button>
          </div>

        </div>
      </section>
    </main>
  )
}

export default Login