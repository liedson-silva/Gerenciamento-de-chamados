import React from 'react';
import { Bar, BarChart, Tooltip, XAxis, YAxis } from 'recharts'

const StageCharts = () => {
  const data = [
    { name: 'Pendentes', uv: 350 },
    { name: 'Atendimento', uv: 690 },
    { name: 'Resolvidos', uv: 950 },
  ]

  return (
    <div className="chart-wrapper">
      <h3 className="chart-title">Solicitações em etapas</h3>
      <BarChart width={400} height={250} data={data}>
        <XAxis dataKey="name" />
        <YAxis hide /> {/* esconde os números do eixo Y para ficar mais limpo */}
        <Tooltip content={() => null} cursor={false} />
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