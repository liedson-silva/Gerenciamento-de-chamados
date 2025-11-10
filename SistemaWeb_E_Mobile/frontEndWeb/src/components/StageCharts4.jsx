import React from 'react';
import {
  BarChart, Bar, XAxis, YAxis, Tooltip, ResponsiveContainer
} from 'recharts';

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

const TicketsByPriorityChart = ({ viewTickets }) => {

  const data2 = [
    { name: "Baixa", color: '#50eb89ff', uv: viewTickets.filter(ticket => ticket.PrioridadeChamado === "Baixa" && ticket.StatusChamado === "Resolvido").length },
    { name: "Média", color: '#5789e0ff', uv: viewTickets.filter(ticket => ticket.PrioridadeChamado === "Média" && ticket.StatusChamado === "Resolvido").length },
    { name: "Alta", color: '#ec6258ff', uv: viewTickets.filter(ticket => ticket.PrioridadeChamado === "Alta" && ticket.StatusChamado === "Resolvido").length }
  ];

  return (
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
  );
};

export default TicketsByPriorityChart;