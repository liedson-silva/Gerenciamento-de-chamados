import { useState, useEffect } from 'react';
import '../Style.css'
import logo from '../assets/logo.png'
import { FaRegUser } from "react-icons/fa";
import { TbLockPassword } from "react-icons/tb";
import { useNavigate } from "react-router-dom";
import api from "../services/api.js"

const Login = () => {
  const [name, setName] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [forgetPassword, setForgetPassword] = useState("")
  const navigate = useNavigate()

  const handleLogin = async () => {
    try {
      const response = await api.post("/login", { name, password })

      if (response.data.success) {
        navigate("/home", { state: { user: response.data.user } })
      }
    } catch (err) {
      setError("Usuário ou senha inválidos!");
      setName("");
      setPassword("");
      setTimeout(() => setError(""), 2000);
    }
  }

  const textForgetPassword = () => {
    setForgetPassword("Entre em contato com o administrador para recuperar a senha")
    setTimeout(() => setForgetPassword(""), 2000);
  }

  return (
    <section className='login'>
      <figure>
        <img src={logo} alt="logo Fatal-System" className='logo-login' />
      </figure>
      <div className="box-login">
        <h1 className='text-login'>Login</h1>
        <div className='form'>

          <div className='inputs'>
            <FaRegUser className='icons' />
            <input className='input-login' name='name' type="text" placeholder='Usuário'
              value={name} onChange={(e) => setName(e.target.value)} />
          </div>

          <div className='inputs'>
            <TbLockPassword className='icons' />
            <input className='input-login' name='password' type="password" placeholder='Senha'
              value={password} onChange={(e) => setPassword(e.target.value)}
              onKeyDown={e => e.key === "Enter" && handleLogin()} />
          </div>

          {error && <p className="notice">{error}</p>}
          {forgetPassword && <p className='notice'>{forgetPassword}</p>}

          <button className='forget-password' onClick={textForgetPassword}>
            Esqueceu a senha?</button>

          <div className='button-enter'>
            <button className='enter' type='submit' onClick={handleLogin}>Entrar</button>
          </div>

        </div>
      </div>
    </section>
  )
}

export default Login