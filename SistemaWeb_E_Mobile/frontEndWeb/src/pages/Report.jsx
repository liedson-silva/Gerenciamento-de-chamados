import { useNavigate, useLocation } from 'react-router-dom';
import { useEffect, useState } from 'react';
import api from "../services/api.js";
import { formatDate } from '../components/FormatDate.jsx';
import StageChart3 from '../components/StageCharts3.jsx';
import StageChart4 from '../components/StageCharts4.jsx';
import ExcelJS from 'exceljs';
import { saveAs } from 'file-saver';

const Report = () => {
  const location = useLocation();
  const user = location.state?.user;
  const navigate = useNavigate();
  const [viewTickets, SetViewTickets] = useState([]);
  const [viewReportTickets, SetReportTickets] = useState([]);
  const [startDate, setStartDate] = useState('');
  const [endDate, setEndDate] = useState('');
  const [error, setError] = useState("");
  const [showCreateReport, setShowCreateReport] = useState(true);
  const [showReport, setShowReport] = useState(false);

  async function getTicket() {
    try {
      const response = await api.get("/All-tickets");
      if (response.data.success) {
        SetViewTickets(response.data.Tickets);
      } else {
        console.log("Erro ao carregar chamados.");
      }
    } catch (error) {
      console.error("Erro ao buscar chamados:", error);
    }
  }

  useEffect(() => {
    getTicket();
  }, []);

  async function getReportTicket(startDate, endDate) {
    try {
      const response = await api.get(`/report-ticket?startDate=${startDate}&endDate=${endDate}`);
      if (response.data.success && response.data.Tickets) {
        const fetchedTickets = response.data.Tickets;
        SetReportTickets(fetchedTickets);
        return fetchedTickets;
      } else {
        SetReportTickets([]);
        return [];
      }
    } catch (error) {
      console.error("Erro ao buscar relatórios dos chamados:", error);
      SetReportTickets([]);
      return [];
    }
  }

  const createReport = async () => {
    if (!startDate || !endDate) {
      setError("Por favor, selecione as datas de início e fim para gerar o relatório.");
      setTimeout(() => setError(""), 4000);
      return;
    }
    setShowCreateReport(false);
    setShowReport(true);

    const tickets = await getReportTicket(startDate, endDate);

    if (tickets.length === 0) {
      setShowReport(false);
      setShowCreateReport(true);
      setError("Nenhum chamado encontrado no período especificado.");
      setTimeout(() => setError(""), 4000);
    }
  };

  const handleHome = () => {
    navigate("/admin-home", { state: { user } });
  };

  const handleBack = () => {
    setShowReport(false);
    setShowCreateReport(true);
    SetReportTickets([]);
  };

  const totalTickets = viewReportTickets.length;
  const pendenteTickets = viewReportTickets.filter(t => t.StatusChamado === "Pendente").length;
  const emAndamentoTickets = viewReportTickets.filter(t => t.StatusChamado === "Em andamento").length;
  const resolvidoTickets = viewReportTickets.filter(t => t.StatusChamado === "Resolvido").length;
  const baixaTickets = viewReportTickets.filter(t => t.PrioridadeChamado === "Baixa").length;
  const mediaTickets = viewReportTickets.filter(t => t.PrioridadeChamado === "Média").length;
  const altaTickets = viewReportTickets.filter(t => t.PrioridadeChamado === "Alta").length;

  const handleDownloadExcel = async () => {
    if (viewReportTickets.length === 0) {
      setError("Não há dados para baixar.");
      setTimeout(() => setError(""), 4000);
      return;
    }

    const workbook = new ExcelJS.Workbook();
    workbook.creator = "Seu Sistema";
    workbook.lastModifiedBy = user?.nome || "Usuário";
    workbook.created = new Date();
    workbook.modified = new Date();

    const worksheet = workbook.addWorksheet("Relatório de Chamados");

    worksheet.columns = [
      { header: "ID", key: "IdChamado", width: 10 },
      { header: "Título", key: "Titulo", width: 30 },
      { header: "Descrição", key: "Descricao", width: 45 },
      { header: "Prioridade", key: "PrioridadeChamado", width: 15 },
      { header: "Data", key: "DataChamado", width: 15 },
      { header: "Status", key: "StatusChamado", width: 18 },
      { header: "ID Usuário", key: "FK_IdUsuario", width: 12 },
    ];

    worksheet.getRow(1).eachCell((cell) => {
      cell.font = { bold: true, color: { argb: "FFFFFFFF" } };
      cell.fill = {
        type: "pattern",
        pattern: "solid",
        fgColor: { argb: "FF007B5E" },
      };
      cell.alignment = { vertical: 'middle', horizontal: 'center' };
    });

    const dataForExcel = viewReportTickets.map(ticket => ({
      ...ticket,
      DataChamado: formatDate(ticket.DataChamado)
    }));

    worksheet.addRows(dataForExcel);

    const cores = {
      prioridade: {
        Alta: "FFFFC7CE",
        Média: "FFC6E0FF",
        Baixa: "FFD8EAD3",
      },
      status: {
        Pendente: "FFFFEB9C",
        "Em andamento": "FFFFD9C4",
        Resolvido: "FFD8EAD3"
      },
      fontes: {
        Alta: "FF9C0006",
        Média: "FF006100",
        Baixa: "FF006100",
        Pendente: "FF9C6500",
        "Em andamento": "FF9C4A00",
        Resolvido: "FF006100"
      }
    };

    worksheet.eachRow({ includeEmpty: false }, (row, rowNumber) => {
      if (rowNumber > 1) {
        const prioridadeCell = row.getCell("PrioridadeChamado");
        const statusCell = row.getCell("StatusChamado");

        if (cores.prioridade[prioridadeCell.value]) {
          prioridadeCell.fill = {
            type: "pattern", pattern: "solid",
            fgColor: { argb: cores.prioridade[prioridadeCell.value] }
          };
          prioridadeCell.font = { color: { argb: cores.fontes[prioridadeCell.value] } };
        }

        if (cores.status[statusCell.value]) {
          statusCell.fill = {
            type: "pattern", pattern: "solid",
            fgColor: { argb: cores.status[statusCell.value] }
          };
          statusCell.font = { color: { argb: cores.fontes[statusCell.value] } };
        }
      }
    });

    const buffer = await workbook.xlsx.writeBuffer();
    const blob = new Blob([buffer], {
      type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
    });
    const fileName = `Relatorio_Chamados_${startDate}_a_${endDate}.xlsx`;
    saveAs(blob, fileName);
  };

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

        {showReport && (
          <div className='box-button-report'>
            <button
              className="button-report"
              onClick={handleDownloadExcel}
            >
              Baixar
            </button>
          </div>
        )}
      </section>

      {showCreateReport && (
        <>
          <section className='report-chart'>
            <div>
              <StageChart3 viewTickets={viewTickets} />
            </div>
            <div>
              <StageChart4 viewTickets={viewTickets} />
            </div>
          </section>
          <button onClick={handleHome} className='button-back' >
            Voltar
          </button>
        </>
      )}

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
            <p>Baixa: <span className='report-title'>{baixaTickets} </span></p>
            <p>Média: <span className='report-title'>{mediaTickets} </span></p>
            <p>Alta: <span className='report-title'>{altaTickets} </span></p>
          </section>

          <div className='box-button-back-report'>
            <button onClick={handleBack} className='button-back-report' >
              Voltar
            </button>
          </div>
        </section>
      )}
    </main>
  );
};

export default Report;