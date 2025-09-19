import React from 'react'
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
      <div className="custom-tooltip">
        <p className="tooltip-label">Prioridade {data.payload.name}</p>
        <p className="tooltip-desc">{data.payload.count} chamados pendentes</p>
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
    <span className="legend-name">{name}</span>
    <span className="legend-count">({count})</span>
  </div>
)

const StageCharts2 = () => {
  const totalTickets = ticketData.reduce((sum, item) => sum + item.count, 0)

  return (
    <div className="chart-container">
      <header className="chart-header">
        <h3>Chamados por Prioridade</h3>
        <p>Total: {totalTickets} chamados pendentes</p>
      </header>

      <div className="chart-wrapper">
        {/* 🚨 Altura corrigida aqui */}
        <ResponsiveContainer width="100%" height={300}>
          <PieChart>
            <Pie
              data={ticketData}
              cx="50%"
              cy="50%"
              innerRadius={60}
              outerRadius={120}
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
      </div>

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
  )
}

export default StageCharts2