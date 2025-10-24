import {
  AreaChart, Area, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer,
  BarChart, Bar
} from 'recharts';
import { useNavigate, useLocation } from 'react-router-dom';
import { useEffect, useState } from 'react';
import api from "../services/api.js"
import { formatDate } from '../components/FormatDate.jsx';

const Report = () => {
  const location = useLocation()
  const user = location.state?.user
  const navigate = useNavigate()
  const [viewTickets, SetViewTickets] = useState("")
  const [showCreateReport, setShowCreateReport] = useState(true)
  const [showReport, setShowReport] = useState(false)

  async function getTicket() {
    try {
      const response = await api.get("/All-tickets")
      if (response.data.success) {
        SetViewTickets(response.data.Tickets)
      } else {
        setErrorMessage("Erro ao carregar chamados.")
      }
    } catch (error) {
      console.error("Erro ao buscar chamados:", error)
    }
  }
  useEffect(() => {
    getTicket()
  }, [])

  const createReport = () => {
    setShowCreateReport(false)
    setShowReport(true)
  }

  const data = [
    { "name": "Ago", "uv": 20, "pv": 30 },
    { "name": "Set", "uv": 13, "pv": 48 },
    { "name": "Out", "uv": 17, "pv": 30 },
    { "name": "Nov", "uv": 35, "pv": 40 }
  ]

  const data2 = [
    { name: "Baixa", color: '#50eb89ff', uv: 4 },
    { name: "Média", color: '#5789e0ff', uv: 3 },
    { name: "Alta", color: '#ec6258ff', uv: 2 }
  ]

  const CustomTooltip = ({ active, payload, label }) => {
    if (active && payload && payload.length) {
      return (
        <div style={{
          backgroundColor: 'rgba(255, 255, 255, 0.9)',
          border: '1px solid #ccc',
          padding: '10px',
          borderRadius: '5px'
        }}>
          <p className="label">{`${label} : ${payload[0].value}`}</p>
        </div>
      );
    }
    return null;
  };

  const handleHome = () => {
    navigate("/admin-home", { state: { user } });
  }

  const handleBack = () => {
    setShowReport(false)
    setShowCreateReport(true)
  }

  return (
    <main>
      <h1>Relatórios</h1>
      
        <section className="report">
          <div>
            <p className="report-title">Período:</p>
            <form className="report-date-group">
              <input type="date" id='start-date' className="report-date" />
              <label htmlFor="start-date">De:</label>
              <div className="form-group">
                <input type="date" id='end-date' className="report-date" />
                <label htmlFor="end-date">Até:</label>
              </div>
            </form>
          </div>

          <div>
            <p className="report-title">Tipo de relatório:</p>
            <ul>
              <li className="report-item">Prioridade<input type="radio" className='report-radio' /></li>
              <li className="report-item">Status <input type="radio" className='report-radio' /></li>
            </ul>
          </div>

          <button className="button-report" onClick={createReport}>Gerar relatório</button>
        </section>

        {showCreateReport && (<>
        <section className='report-chart'>

          <div className='report-box-chart'>
            <p className='report-title-chart'>Chamados Resolvidos x Criados (Mensal)</p>
            <ResponsiveContainer>
              <AreaChart
                data={data}
                margin={{ top: 10, right: 30, left: 0, bottom: 40 }}
              >
                <defs>
                  <linearGradient id="colorUv" x1="0" y1="0" x2="0" y2="1">
                    <stop offset="5%" stopColor="#00b0ff" stopOpacity={0.8} />
                    <stop offset="95%" stopColor="#00b0ff" stopOpacity={0} />
                  </linearGradient>
                  <linearGradient id="colorPv" x1="0" y1="0" x2="0" y2="1">
                    <stop offset="5%" stopColor="#00e676" stopOpacity={0.8} />
                    <stop offset="95%" stopColor="#00e676" stopOpacity={0} />
                  </linearGradient>
                </defs>
                <XAxis dataKey="name" />
                <YAxis width={60} />
                <CartesianGrid strokeDasharray="3 3" />
                <Tooltip />
                <Area type="monotone" dataKey="uv" stroke="#00b0ff" fillOpacity={1} fill="url(#colorUv)" name="Resolvidos" />
                <Area type="monotone" dataKey="pv" stroke="#00e676" fillOpacity={1} fill="url(#colorPv)" name="Criados" />
              </AreaChart>
            </ResponsiveContainer>
          </div>

          <div className='report-box-chart'>
            <p className='report-title-chart'>Chamados Resolvidos (Prioridade)</p>
            <ResponsiveContainer>
              <BarChart data={data2} margin={{ top: 10, right: 30, left: 0, bottom: 40 }}>
                <XAxis dataKey="name" />
                <YAxis width={60} />
                <Tooltip content={<CustomTooltip />} />
                <Bar dataKey="uv" radius={[10, 10, 0, 0]} fill="currentColor" />
              </BarChart>
            </ResponsiveContainer>
          </div>

        </section>

        <button onClick={handleHome} className='button-back' >
          Voltar
        </button>
      </>)}

      {showReport && (
        <button onClick={handleBack} className='button-back' >
          Voltar
        </button>
      )}

    </main>
  )
}

export default Report