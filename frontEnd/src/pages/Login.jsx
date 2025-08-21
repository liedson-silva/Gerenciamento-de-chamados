import { useState } from 'react';
import '../Style.css'
import logo from '../assets/logo.png'
import { FaRegUser } from "react-icons/fa";
import { TbLockPassword } from "react-icons/tb";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const [name, setName] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [forgetPassword, setForgetPassword] = useState("")
  const navigate = useNavigate()

  const users = [
    {
      name: "Tirulipa",
      password: "123"
    },
    {
      name: "Neymar Junior",
      password: "123"
    },
    {
      name: "1",
      password: "1"
    },
  ]

  const handleLogin = () => {
    const userFound = users.find(
      (user) => user.name == name && user.password == password)

    if (userFound) {
      navigate("/home", { state: { user: userFound } })
    } else {
      setError("Usuário ou senha inválidos!");
      setName("")
      setPassword("")
      setTimeout(() => {
        setError("");
      }, 2000);
    }
  }

  const textForgetPassword = () => {
    setForgetPassword("Entre em contato com o administrador para recuperar a senha")
    setTimeout(() => {
      setForgetPassword("");
    }, 2000);
  }

  return (
    <div className='login'>
      <div>
        <img src={logo} alt="logo Fatal-System" className='logo-login' />
      </div>
      <div className="box-login">
        <h1 className='text-login'>Login</h1>
        <div className='form'>
          
          <div className='inputs'>
            <FaRegUser className='icons' />
            <input className='input-login' type="text" placeholder='Usuário'
              value={name} onChange={(e) => setName(e.target.value)} />
          </div>

          <div className='inputs'>
            <TbLockPassword className='icons' />
            <input className='input-login' type="password" placeholder='Senha'
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
    </div>
  )
}

export default Login