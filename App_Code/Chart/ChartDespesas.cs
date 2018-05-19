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
    public class ChartDespesas
    {
        public void BindChartTipoGastosByDate(int adv, Literal lt, DateTime initialdate, DateTime finaldate)
        {
            StringBuilder str = new StringBuilder();
            DespesasDB desDB = new DespesasDB();
            Despesas des = new Despesas();
            DataTable dt = new DataTable();
            TipoDespesasDB tidDB = new TipoDespesasDB();
            TipoDespesa tid = new TipoDespesa();
            int totalTID = tidDB.SelectTotalTipoDespesaEscritorio();
            dt.Columns.Add("total", typeof(int));
            dt.Columns.Add("tipo", typeof(string));

            for (int j = 1; j <= totalTID; j++)
            {
                tid = tidDB.SelectTipoDespesaEscritorio(j);
                if (tid.Categoria == 1)
                {
                    double total = desDB.GetTotalDespesaEscritorioByDate(adv, tid.Codigo, initialdate, finaldate);
                    dt.Rows.Add(total, tid.Descricao);
                }
            }

            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChartTipoGastosByMonth);
                       function drawChartTipoGastosByMonth() {
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
            str.Append(" var chart = new google.visualization.PieChart(document.getElementById('piechart_TipoGastosByMonth'));");
            str.Append(" chart.draw(data, options);");
            str.Append("}");

            str.Append("</script>");

            lt.Text = str.ToString().Replace('*', '"');
        }
        public void BindChartTipoGastosByDateHome(int adv, Literal lt, DateTime initialdate, DateTime finaldate)
        {
            StringBuilder str = new StringBuilder();
            DespesasDB desDB = new DespesasDB();
            Despesas des = new Despesas();
            DataTable dt = new DataTable();
            TipoDespesasDB tidDB = new TipoDespesasDB();
            TipoDespesa tid = new TipoDespesa();
            int totalTID = tidDB.SelectTotalTipoDespesaEscritorio();
            dt.Columns.Add("total", typeof(int));
            dt.Columns.Add("tipo", typeof(string));

            for (int j = 1; j <= totalTID; j++)
            {
                tid = tidDB.SelectTipoDespesaEscritorio(j);
                double total = desDB.GetTotalDespesaEscritorioByDate(adv, tid.Codigo, initialdate, finaldate);
                if (tid.Categoria == 1)
                {
                    dt.Rows.Add(total, tid.Descricao);
                }
            }

            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChartTipoGastosByMonth);
                       function drawChartTipoGastosByMonth() {
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
            str.Append("            chartArea: { width: '100%', height: '100%'}, ");
            str.Append("         }; ");
            str.Append(" var chart = new google.visualization.PieChart(document.getElementById('piechart_TipoGastosByMonth'));");
            str.Append(" chart.draw(data, options);");
            str.Append("}");

            str.Append("</script>");

            lt.Text = str.ToString().Replace('*', '"');
        }
        public void BindChartGastos(int adv, Literal lt)
        {

            StringBuilder str = new StringBuilder();
            DespesasDB desDB = new DespesasDB();
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("Total", typeof(double));
            dt.Columns.Add("Mes", typeof(string));
            for (int i = 1; i <= 12; i++)
            {
                double total = desDB.GetTotalDespesaEscritorio(adv, i);
                string mes = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                dt.Rows.Add(i, total, mes);
            }

            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChartGastosAno);
                       function drawChartGastosAno() {
        var data = google.visualization.arrayToDataTable([
             ['2015', 'Despesas'],
        ");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("['" + dt.Rows[i]["Mes"].ToString() + "'," + dt.Rows[i]["Total"].ToString() + "],");
            }
            str.Append(" ]); ");
            str.Append("  var options = {");
            str.Append(" backgroundColor: 'none', ");
            str.Append("            chartArea: { width: '100%', height: '100%'}, ");
            str.Append("            chart: {");
            str.Append("                title: 'Relatorio Gastos',");
            str.Append("            }");
            str.Append("        };");
            str.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('chart_GastosAno'));");
            str.Append(" chart.draw(data, options);");
            str.Append("}");
            str.Append("</script>");
            lt.Text = str.ToString().Replace('*', '"');
        }
    }
}