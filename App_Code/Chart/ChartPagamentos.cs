using Habius.Classes.Administrativo;
using Habius.Persistencia.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Habius.Chart
{
    public class ChartPagamentos
    {
        public void BindChartTipoPagamentosByDate(int adv, Literal lt, DateTime initialdate, DateTime finaldate)
        {
            StringBuilder str = new StringBuilder();
            Pagamento pag = new Pagamento();
            PagamentoDB pagDB = new PagamentoDB();
            DataTable dt = new DataTable();
            Servico sev = new Servico();
            ServicoDB sevDB = new ServicoDB();

            int totalServico = sevDB.SelectTotalServico();
            dt.Columns.Add("total", typeof(int));
            dt.Columns.Add("tipo", typeof(string));

            for (int j = 1; j <= totalServico; j++)
            {
                sev = sevDB.Select(j);
                if (sev.Descricao != "Outros")
                {
                    double total = pagDB.GetTotalPagamentoByDate(adv, sev.Codigo, initialdate, finaldate);
                    dt.Rows.Add(total, sev.Descricao);
                }
            }

            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChartServicoPagamentoByMonth);
                       function drawChartServicoPagamentoByMonth() {
        var data = google.visualization.arrayToDataTable([
             ['TipoDespesa', 'quantidade'],
            ");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("['" + dt.Rows[i]["tipo"].ToString() + "'," + dt.Rows[i]["total"].ToString() + "],");
            }
            str.Append(" ]); ");
            str.Append(" var options = { ");
            str.Append(" is3D: true, ");
            str.Append(" backgroundColor: 'none', ");
            str.Append("            chartArea: { left: 100, width: '100%', height: '100%' } ");
            str.Append("         }; ");
            str.Append(" var chart = new google.visualization.PieChart(document.getElementById('piechart_ServicoPagamentoByMonth'));");
            str.Append(" chart.draw(data, options);");
            str.Append("}");

            str.Append("</script>");

            lt.Text = str.ToString().Replace('*', '"');
        }
        public void BindChartTipoPagamentosByDateHome(int adv, Literal lt, DateTime initialdate, DateTime finaldate)
        {
            StringBuilder str = new StringBuilder();
            Pagamento pag = new Pagamento();
            PagamentoDB pagDB = new PagamentoDB();
            DataTable dt = new DataTable();
            Servico sev = new Servico();
            ServicoDB sevDB = new ServicoDB();

            int totalServico = sevDB.SelectTotalServico();
            dt.Columns.Add("total", typeof(int));
            dt.Columns.Add("tipo", typeof(string));

            for (int j = 1; j <= totalServico; j++)
            {
                sev = sevDB.Select(j);
                if (sev.Descricao != "Outros")
                {
                    double total = pagDB.GetTotalPagamentoByDate(adv, sev.Codigo, initialdate, finaldate);
                    dt.Rows.Add(total, sev.Descricao);
                }
            }

            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChartServicoPagamentoByMonth);
                       function drawChartServicoPagamentoByMonth() {
        var data = google.visualization.arrayToDataTable([
             ['TipoDespesa', 'quantidade'],
            ");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("['" + dt.Rows[i]["tipo"].ToString() + "'," + dt.Rows[i]["total"].ToString() + "],");
            }
            str.Append(" ]); ");
            str.Append(" var options = { ");
            str.Append(" is3D: true, ");
            str.Append(" backgroundColor: '#e8f1f4', ");
            str.Append("            chartArea: { left: 40, top: 20, width: '100%', height: '100%' } ");
            str.Append("         }; ");
            str.Append(" var chart = new google.visualization.PieChart(document.getElementById('piechart_ServicoPagamentoByMonth'));");
            str.Append(" chart.draw(data, options);");
            str.Append("}");

            str.Append("</script>");

            lt.Text = str.ToString().Replace('*', '"');
        }

        public void BindChartPagamentos(int adv, Literal lt)
        {
            DataTable dt = new DataTable();
            StringBuilder str = new StringBuilder();
            PagamentoDB pagDB = new PagamentoDB();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("Total", typeof(int));
            dt.Columns.Add("Mes", typeof(string));
            for (int i = 1; i <= 12; i++)
            {
                double total = pagDB.TotalPagamentoMes(adv, i);
                string mes = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                dt.Rows.Add(i, total, mes);
            }

            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChartPagamentosAno);
                       function drawChartPagamentosAno() {
        var data = google.visualization.arrayToDataTable([
             ['2015', 'Pagamentos'],
        ");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("['" + dt.Rows[i]["Mes"].ToString() + "'," + dt.Rows[i]["Total"].ToString() + "],");
            }
            str.Append(" ]); ");
            str.Append("  var options = {");
            str.Append(" backgroundColor: '#fff', ");
            str.Append("            chart: {");
            str.Append("                title: 'Relatorio Pagamentos',");
            str.Append("            }");
            str.Append("        };");
            str.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('chart_PagamentosAno'));");
            str.Append(" chart.draw(data, options);");
            str.Append("}");
            str.Append("</script>");
            lt.Text = str.ToString().Replace('*', '"');
        }
    }
}