import { Bar, BarChart, XAxis } from 'recharts'
import api from "../services/api.js"
import { useState, useEffect } from 'react'

const StageCharts = (user) => {
  const [ViewTickets, SetViewTickets] = useState([])

  async function getTickets() {
    const response = await api.get(`/tickets/${user.IdUsuario}`)
    SetViewTickets(response.data.Tickets)
  }
  useEffect(() => {
    getTickets()
  }, [])

  const data = [
    { name: 'Pendente', uv: ViewTickets.filter(ticket => ticket.StatusChamado === "Pendente").length },
    { name: 'Em andamento', uv: ViewTickets.filter(ticket => ticket.StatusChamado === "Em andamento").length },
    { name: 'Resolvido', uv: ViewTickets.filter(ticket => ticket.StatusChamado === "Resolvido").length }
  ]

  return (
    <div>
      <h3 >Chamados por etapa</h3>
      <BarChart width={300} height={140} data={data}>
        <XAxis dataKey="name" />
        <Bar
          dataKey="uv"
          radius={[10, 10, 0, 0]} // borda arredondada no topo
          fill="currentColor"
        />
      </BarChart>
    </div>
  )
}

export default StageCharts