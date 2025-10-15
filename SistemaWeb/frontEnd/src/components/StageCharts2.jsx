import { PieChart, Pie, Cell, ResponsiveContainer, Tooltip } from 'recharts'
import api from "../services/api.js"
import { useEffect, useState } from 'react'

const CustomTooltip = ({ active, payload }) => {
  if (active && payload && payload.length) {
    const data = payload[0]
    return (
      <div>
        <p>Prioridade {data.payload.name}</p>
        <p>{data.payload.count} chamados pendentes</p>
      </div>
    )
  }
  return null
}

// Legenda personalizada
const LegendItem = ({ color, name, count }) => (
  <div className="legend-item">
    <div
      className="legend-color"
      style={{ backgroundColor: color }}
    />
    <span>{name}</span>
    <span>({count})</span>
  </div>
)

const StageCharts2 = (user) => {
  const [ViewTickets, SetViewTickets] = useState([])

  async function getTickets() {
    if (user.FuncaoUsuario === "Admin" || user.FuncaoUsuario === "Tecnico") {
      const response = await api.get("/All-tickets")
      SetViewTickets(response.data.Tickets)
    } else {
      const response = await api.get(`/tickets/${user.IdUsuario}`)
      SetViewTickets(response.data.Tickets)
    }
  }
  useEffect(() => {
    getTickets()
  }, [])

  const ticketData = [
    { 
      name: 'Baixa',
      value: ViewTickets.filter(ticket => ticket.PrioridadeChamado === "Baixa").length,
      color: '#50eb89ff',
      count: ViewTickets.filter(ticket => ticket.PrioridadeChamado === "Baixa").length
    },
    {
      name: 'Média',
      value: ViewTickets.filter(ticket => ticket.PrioridadeChamado === "Média").length,
      color: '#5789e0ff',
      count: ViewTickets.filter(ticket => ticket.PrioridadeChamado === "Média").length
    },
    {
      name: 'Alta',
      value: ViewTickets.filter(ticket => ticket.PrioridadeChamado === "Alta").length,
      color: '#ec6258ff',
      count: ViewTickets.filter(ticket => ticket.PrioridadeChamado === "Alta").length
    }
  ]

  return (
    <div>
      <div className="chart-wrapper">
        <h3>Chamados por prioridade</h3>
        <ResponsiveContainer width="100%" height={100}>
          <PieChart>
            <Pie
              data={ticketData}
              cx="50%"
              cy="50%"
              innerRadius={15}
              outerRadius={50}
              paddingAngle={2}
              dataKey="value"
              strokeWidth={0}
            >
              {ticketData.map((entry, index) => (
                <Cell key={`cell-${index}`} fill={entry.color} />
              ))}
            </Pie>
            <Tooltip content={<CustomTooltip />} />
          </PieChart>
        </ResponsiveContainer>

        <div className="chart-legend">
          {ticketData.map((item, index) => (
            <LegendItem
              key={index}
              color={item.color}
              name={item.name}
              count={item.count}
            />
          ))}
        </div>

      </div>
    </div>
  )
}

export default StageCharts2