import {
  AreaChart, Area, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer,
  BarChart, Bar
} from 'recharts';
import { useNavigate, useLocation } from 'react-router-dom';

const Report = () => {
  const location = useLocation()
  const user = location.state?.user
  const navigate = useNavigate()

  const data = [
    { "name": "Jun", "uv": 40, "pv": 40 },
    { "name": "Jul", "uv": 30, "pv": 38 },
    { "name": "Ago", "uv": 20, "pv": 30 },
    { "name": "Set", "uv": 20, "pv": 48 },
    { "name": "Out", "uv": 10, "pv": 30 },
    { "name": "Nov", "uv": 20, "pv": 40 },
    { "name": "Dez", "uv": 30, "pv": 40 }
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

  const handleBack = () => {
    navigate("/admin-home", { state: { user } });
  }

  return (
    <main>
      <h1>Relatórios</h1>

      <section className="report">
        <div>
          <p className="report-title">Período:</p>
          <input type="date" className="report-date" />
        </div>

        <div>
          <p className="report-title">Tipo de relatório:</p>
          <ul>
            <li className="report-item">Prioridade</li>
            <li className="report-item">Status</li>
          </ul>
        </div>

        <button className="button-report">Gerar relatório</button>
      </section>

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

      <button onClick={handleBack} className='button-back' >
        Voltar
      </button>

    </main>
  )
}

export default Report