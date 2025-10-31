using Gerenciamento_De_Chamados.Repositories; 
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks; 
using System.Windows.Forms;
using Gerenciamento_De_Chamados.Helpers;

namespace Gerenciamento_De_Chamados
{
    public partial class FormRelatorioDetalhado : Form
    {
        private DateTime _dataInicio;
        private DateTime _dataFim;
        private DataTable relatorioDataTable;

       
        private readonly IRelatorioRepository _relatorioRepository;

        public FormRelatorioDetalhado(DateTime dataInicio, DateTime dataFim)
        {
            InitializeComponent();

            _dataInicio = dataInicio;
            _dataFim = dataFim;

            
            _relatorioRepository = new RelatorioRepository();

            this.Load += FormRelatorioDetalhado_Load;
        }

        
        private async void FormRelatorioDetalhado_Load(object sender, EventArgs e)
        {
            
            await CarregarRelatorioAsync();
        }

     
        private async Task CarregarRelatorioAsync()
        {
            try
            {
                relatorioDataTable = await _relatorioRepository.GerarRelatorioDetalhadoAsync(_dataInicio, _dataFim);

               
                dgvRelatorio.DataSource = relatorioDataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar relatório detalhado: \n" + ex.Message);
            }
        }

        #region Métodos de Exportação e Navegação (Sem Alterações)

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            var relatorio = new Relatorio();
            relatorio.Show();
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
           
            if (relatorioDataTable == null || relatorioDataTable.Rows.Count == 0)
            {
                MessageBox.Show("Não há dados para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Arquivo CSV (*.csv)|*.csv";
            saveDialog.Title = "Salvar Relatório como CSV";
            saveDialog.FileName = $"Relatorio_Chamados_{DateTime.Now:yyyyMMdd}.csv";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveDialog.FileName, false, Encoding.UTF8))
                    {
                        List<string> headers = new List<string>();
                        foreach (DataColumn column in relatorioDataTable.Columns)
                        {
                            headers.Add($"\"{column.ColumnName}\"");
                        }
                        sw.WriteLine(string.Join(";", headers));

                        foreach (DataRow row in relatorioDataTable.Rows)
                        {
                            List<string> fields = new List<string>();
                            foreach (object field in row.ItemArray)
                            {
                                fields.Add($"\"{field?.ToString() ?? ""}\"");
                            }
                            sw.WriteLine(string.Join(";", fields));
                        }
                    }
                    MessageBox.Show("Relatório exportado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar o arquivo: \n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            // Este método também não muda
            if (relatorioDataTable == null || relatorioDataTable.Rows.Count == 0)
            {
                MessageBox.Show("Não há dados para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Arquivo PDF (*.pdf)|*.pdf";
            saveDialog.Title = "Salvar Relatório como PDF";
            saveDialog.FileName = $"Relatorio_Chamados_{DateTime.Now:yyyyMMdd}.pdf";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Document doc = new Document(PageSize.A4.Rotate());
                    PdfWriter.GetInstance(doc, new FileStream(saveDialog.FileName, FileMode.Create));
                    doc.Open();

                    Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
                    Paragraph title = new Paragraph("Relatório Detalhado de Chamados", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    title.SpacingAfter = 20f;
                    doc.Add(title);

                    PdfPTable pdfTable = new PdfPTable(relatorioDataTable.Columns.Count);
                    pdfTable.WidthPercentage = 100;

                    Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);
                    Font cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.BLACK);

                    foreach (DataColumn column in relatorioDataTable.Columns)
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, headerFont));
                        headerCell.BackgroundColor = new BaseColor(68, 84, 136);
                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        headerCell.Padding = 5;
                        pdfTable.AddCell(headerCell);
                    }

                    foreach (DataRow row in relatorioDataTable.Rows)
                    {
                        foreach (object field in row.ItemArray)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(field?.ToString() ?? "", cellFont));
                            cell.Padding = 5;
                            pdfTable.AddCell(cell);
                        }
                    }

                    doc.Add(pdfTable);
                    doc.Close();

                    MessageBox.Show("Relatório PDF exportado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar o arquivo PDF. \nVerifique se o arquivo não está aberto.\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lbl_Inicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void PctBox_Inicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void lbSair_Click(object sender, EventArgs e)
        {
            FormHelper.Sair(this);
        }

        #endregion
    }
}