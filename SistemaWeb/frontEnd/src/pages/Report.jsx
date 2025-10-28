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
  const [viewTickets, SetViewTickets] = useState([])
  const [viewReportTickets, SetReportTickets] = useState([])
  const [startDate, setStartDate] = useState('');
  const [endDate, setEndDate] = useState('');
  const [error, setError] = useState("");
  const [showCreateReport, setShowCreateReport] = useState(true)
  const [showReport, setShowReport] = useState(false)

  async function getTicket() {
    try {
      const response = await api.get("/All-tickets")
      if (response.data.success) {
        SetViewTickets(response.data.Tickets)
      } else {
        console.log("Erro ao carregar chamados.")
      }
    } catch (error) {
      console.error("Erro ao buscar chamados:", error)
    }
  }
  useEffect(() => {
    getTicket()
  }, [])

  async function getReportTicket(startDate, endDate) {
    try {
      const response = await api.get(`/report-ticket?startDate=${startDate}&endDate=${endDate}`)
      if (response.data.success && response.data.Tickets) {
        const fetchedTickets = response.data.Tickets;
        SetReportTickets(fetchedTickets);
        return fetchedTickets;
      } else {
        console.log("Erro ao carregar chamados ou sucesso falso da API.");
        SetReportTickets([]);
        return []
      }
    } catch (error) {
      console.error("Erro ao buscar relatórios dos chamados:", error);
      SetReportTickets([]);
      return []
    }
  }

  const createReport = async () => {

    if (!startDate || !endDate) {
      setError("Por favor, selecione as datas de início e fim para gerar o relatório.")
      setTimeout(() => setError(""), 4000);
      return
    }
    setShowCreateReport(false)
    setShowReport(true)

    const tickets = await getReportTicket(startDate, endDate)

    if (tickets.length === 0) {
      setShowReport(false);
      setShowCreateReport(true);
      setError("Nenhum chamado encontrado no período especificado.");
      setTimeout(() => setError(""), 4000);
    }
  }

  const monthNames = ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"];

  const data = Array.from({ length: 4 }, (_, i) => {
    const today = new Date();
    const targetDate = new Date(today.getFullYear(), today.getMonth() - i, 1);

    const monthIndex = targetDate.getMonth();
    const year = targetDate.getFullYear();
    const monthName = monthNames[monthIndex];

    const ticketsInMonth = viewTickets.filter(ticket => {
      const d = new Date(ticket.DataChamado);
      return d.getUTCFullYear() === year && d.getUTCMonth() === monthIndex;
    });

    return {
      "name": monthName,
      "uv": ticketsInMonth.filter(t => t.StatusChamado === "Resolvido").length,
      "pv": ticketsInMonth.length
    };
  }).reverse();

  const data2 = [
    { name: "Baixa", color: '#50eb89ff', uv: viewTickets.filter(ticket => ticket.PrioridadeChamado === "baixa").length },
    { name: "Média", color: '#5789e0ff', uv: viewTickets.filter(ticket => ticket.PrioridadeChamado === "Média").length },
    { name: "Alta", color: '#ec6258ff', uv: viewTickets.filter(ticket => ticket.PrioridadeChamado === "Alta").length }
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

  const totalTickets = viewReportTickets.length;
  const pendenteTickets = viewReportTickets.filter(t => t.StatusChamado === "Pendente").length;
  const emAndamentoTickets = viewReportTickets.filter(t => t.StatusChamado === "Em andamento").length;
  const resolvidoTickets = viewReportTickets.filter(t => t.StatusChamado === "Resolvido").length;

  return (
    <main>
      <h1>Relatórios</h1>

      <section className="report">
        <div>
          <p className="report-title">Período:</p>
          <form className="report-date-group">
            <input type="date" id='start-date' className="report-date" value={startDate} onChange={(e) => setStartDate(e.target.value)} />
            <label htmlFor="start-date">De:</label>
            <div className="form-group">
              <input type="date" id='end-date' className="report-date" value={endDate} onChange={(e) => setEndDate(e.target.value)} />
              <label htmlFor="end-date">Até:</label>
            </div>
          </form>
        </div>

        {error && <p className="notice">{error}</p>}

        <div className='box-button-report'>
          <button className="button-report" onClick={createReport}>Gerar relatório</button>
        </div>
      </section>

      {showCreateReport && (<>
        <section className='report-chart'>

          <div className='report-box-chart1'>
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
        <section className='report-list'>
          <div className="scroll-lists">
            <table className="ticket-table">
              <thead>
                <tr>
                  <th>ID</th>
                  <th>Título</th>
                  <th>Descrição</th>
                  <th>Prioridade</th>
                  <th>Data</th>
                  <th>Status</th>
                  <th>Usuário</th>
                </tr>
              </thead>
              <tbody>
                {viewReportTickets.map((ticket) => (
                  <tr key={ticket.IdChamado}>
                    <td>{ticket.IdChamado}</td>
                    <td>{ticket.Titulo}</td>
                    <td>{ticket.Descricao}</td>
                    <td>{ticket.PrioridadeChamado === "Média" ? (
                      <> <span className="circle-blue">ㅤ</span> {ticket.PrioridadeChamado}</>
                    ) : ticket.PrioridadeChamado === "Alta" ? (
                      <> <span className="circle-red">ㅤ</span> {ticket.PrioridadeChamado}</>
                    ) : (
                      <> <span className="circle-green">ㅤ</span> {ticket.PrioridadeChamado}</>
                    )}</td>
                    <td>{formatDate(ticket.DataChamado)}</td>
                    <td>{ticket.StatusChamado === "Pendente" ? (
                      <> <span className="circle-yellow">ㅤ</span> {ticket.StatusChamado}</>
                    ) : ticket.StatusChamado === "Em andamento" ? (
                      <> <span className="circle-orange">ㅤ</span> {ticket.StatusChamado}</>
                    ) : (
                      <> <span className="circle-green">ㅤ</span> {ticket.StatusChamado}</>
                    )}</td>
                    <td>{ticket.FK_IdUsuario}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          <section className='report-ticket'>
            <p>Total: <span className='report-title'>{totalTickets}</span></p>
            <p>Pendente: <span className='report-title'>{pendenteTickets} </span></p>
            <p>Em andamento: <span className='report-title'>{emAndamentoTickets} </span></p>
            <p>Resolvido: <span className='report-title'>{resolvidoTickets} </span></p>
          </section>

          <div className='box-button-back-report'>
          <button onClick={handleBack} className='button-back-report' >
            Voltar
          </button>
          </div>
        </section>
      )}

    </main>
  )
}

export default Report