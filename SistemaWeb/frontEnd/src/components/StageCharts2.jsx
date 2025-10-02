import { PieChart, Pie, Cell, ResponsiveContainer, Tooltip } from 'recharts'

// Dados dos chamados por prioridade
const ticketData = [
  {
    name: 'Baixa',
    value: 65,
    color: '#50eb89ff',
    count: 65
  },
  {
    name: 'Média',
    value: 28,
    color: '#9557e0ff',
    count: 28
  },
  {
    name: 'Alta',
    value: 15,
    color: '#ec6258ff',
    count: 15
  }
]

// Tooltip personalizada
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

const StageCharts2 = () => {
  const totalTickets = ticketData.reduce((sum, item) => sum + item.count, 0)

  return (
    <div>
      <div className="chart-wrapper">
        {/* 🚨 Altura corrigida aqui */}
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