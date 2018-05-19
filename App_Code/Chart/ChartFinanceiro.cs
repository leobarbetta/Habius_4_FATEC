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
    public class ChartFinanceiro
    {
        public void BindChartFinanceiro(int adv, Literal lt)
        {
            DataTable dt = new DataTable();

            StringBuilder str = new StringBuilder();

            dt = GetDataFinanceiro(adv);

            str.Append(@"<script type=*text/javascript*> google.load(*visualization*, *1.1*, { packages: [*bar*] });
                       google.setOnLoadCallback(drawChartFinanceiro);
                       function drawChartFinanceiro() {
        var data = google.visualization.arrayToDataTable([
             ['2015', 'Pagamentos', 'Despesas'],
        ");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("['" + dt.Rows[i]["Mes"].ToString() + "'," + dt.Rows[i]["Pagamento"].ToString() + " ," + dt.Rows[i]["Despesa"].ToString() + "],");
            }
            str.Append(" ]); ");
            str.Append("  var options = {");
            str.Append(" backgroundColor: 'none', ");
            str.Append("            chartArea: { width: '100%', height: '100%'}, ");
            str.Append("                title: 'Relatorio Financeiro',");
     
            str.Append("        };");
            str.Append(" var chart = new google.charts.Bar(document.getElementById('chart_Financeiro'));");
            str.Append(" chart.draw(data, options);");
            str.Append("}");
            str.Append("</script>");

            lt.Text = str.ToString().Replace('*', '"');
        }
        private DataTable GetDataFinanceiro(int adv)
        {
            DespesasDB desDB = new DespesasDB();
            PagamentoDB pagDB = new PagamentoDB();
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("Despesa", typeof(int));
            dt.Columns.Add("Pagamento", typeof(int));
            dt.Columns.Add("Mes", typeof(string));
            for (int i = 1; i <= 12; i++)
            {
                double despesa = desDB.GetTotalDespesaEscritorio(adv, i);
                double pagamento = pagDB.TotalPagamentoMes(adv, i);
                string mes = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                dt.Rows.Add(i, despesa, pagamento, mes);
            }
            return dt;
        }
    }
}