import React from 'react';
import {
  AreaChart, Area, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer
} from 'recharts';

const TicketsByMonthChart = ({ viewTickets }) => {

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

  return (
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
  );
};

export default TicketsByMonthChart;