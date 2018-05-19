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
    public class ChartProcessos
    {
        public void BindChartTipoMovimentacao(int adv, Literal lt, DateTime initialDate, DateTime finalDate)
        {
            DataTable dt = new DataTable();
            StringBuilder str = new StringBuilder();
            MovimentacaoDB movDB = new MovimentacaoDB();
            Movimentacao mov = new Movimentacao();
            ProcessoDB proDB = new ProcessoDB();
            int qtdmovimentacao = movDB.TotalMovimentacao();
            dt.Columns.Add("Total", typeof(int));
            dt.Columns.Add("Mov", typeof(string));
            for (int i = 1; i <= qtdmovimentacao; i++)
            {
                double total = proDB.SelectTotalGraficoByDate(adv, i, initialDate, finalDate);
                mov = movDB.Select(i);
                dt.Rows.Add(total, mov.Descricao);
            }

            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChartTipoMovimentacao);
                       function drawChartTipoMovimentacao() {
        var data = google.visualization.arrayToDataTable([
             ['Movimentação', 'quantidade'],
        ");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("['" + dt.Rows[i]["Mov"].ToString() + "'," + dt.Rows[i]["Total"].ToString() + "],");
            }
            str.Append(" ]); ");
            str.Append(" var options = { ");
            str.Append(" is3D: true, ");
            str.Append(" backgroundColor: 'none', ");
            str.Append("            chartArea: { left: 100, width: '100%', height: '100%'}, ");
            str.Append("         }; ");
            str.Append(" var chart = new google.visualization.PieChart(document.getElementById('piechart_TipoMovimentacao'));");
            str.Append(" chart.draw(data, options);");
            str.Append("}");

            str.Append("</script>");

            lt.Text = str.ToString().Replace('*', '"');
        }

        public void BindChartTipoMovimentacaoAbertas(int adv, Literal lt)
        {
            DataTable dt = new DataTable();
            StringBuilder str = new StringBuilder();
            MovimentacaoDB movDB = new MovimentacaoDB();
            Movimentacao mov = new Movimentacao();
            ProcessoDB proDB = new ProcessoDB();
            int qtdmovimentacao = movDB.TotalMovimentacao();
            dt.Columns.Add("Total", typeof(int));
            dt.Columns.Add("Mov", typeof(string));
            for (int i = 1; i <= qtdmovimentacao; i++)
            {
                double total = proDB.SelectTotalGrafico(adv, i);
                mov = movDB.Select(i);
                if (mov.Descricao != "Finalizado")
                    dt.Rows.Add(total, mov.Descricao);
            }

            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChartTipoMovimentacaoAbertas);
                       function drawChartTipoMovimentacaoAbertas() {
        var data = google.visualization.arrayToDataTable([
             ['Movimentação', 'quantidade'],
            ");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("['" + dt.Rows[i]["Mov"].ToString() + "'," + dt.Rows[i]["Total"].ToString() + "],");
            }
            str.Append(" ]); ");
            str.Append(" var options = { ");
            str.Append(" is3D: true, ");
            str.Append(" backgroundColor: 'none', ");
            str.Append("            chartArea: { left: 100, width: '100%', height: '100%' } ");
            str.Append("         }; ");
            str.Append(" var chart = new google.visualization.PieChart(document.getElementById('piechart_TipoMovimentacaoAbertas'));");
            str.Append(" chart.draw(data, options);");
            str.Append("}");

            str.Append("</script>");

            lt.Text = str.ToString().Replace('*', '"');
        }
        public void BindChartClasse(int adv, Literal lt, DateTime initialDate, DateTime finalDate)
        {
            DataTable dt = new DataTable();
            StringBuilder str = new StringBuilder();
            ClasseDB claDB = new ClasseDB();
            Classe cla = new Classe();
            ProcessoDB proDB = new ProcessoDB();
            int qtdmovimentacao = claDB.TotalClasse();
            dt.Columns.Add("Total", typeof(int));
            dt.Columns.Add("Classe", typeof(string));
            for (int i = 1; i <= qtdmovimentacao; i++)
            {
                double total = claDB.SelectTotalGrafico(adv, i, initialDate, finalDate);
                cla = claDB.Select(i);
                dt.Rows.Add(total, cla.Descricao);
            }

            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChartClasse);
                       function drawChartClasse() {
        var data = google.visualization.arrayToDataTable([
             ['Classe', 'quantidade'],
            ");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("['" + dt.Rows[i]["Classe"].ToString() + "'," + dt.Rows[i]["Total"].ToString() + "],");
            }
            str.Append(" ]); ");
            str.Append(" var options = { ");
            str.Append(" is3D: true, ");
            str.Append(" backgroundColor: 'none', ");
            str.Append("            chartArea: { left: 100, width: '100%', height: '100%' } ");
            str.Append("         }; ");
            str.Append(" var chart = new google.visualization.PieChart(document.getElementById('piechart_Classe'));");
            str.Append(" chart.draw(data, options);");
            str.Append("}");

            str.Append("</script>");

            lt.Text = str.ToString().Replace('*', '"');
        }

        public void BindChartPosicaoCliente(int adv, Literal lt, DateTime initialDate, DateTime finalDate)
        {
            DataTable dt = new DataTable();
            StringBuilder str = new StringBuilder();
            PosicaoCliente posCli = new PosicaoCliente();
            PosicaoClienteDB posCliDB = new PosicaoClienteDB();
            ProcessoDB proDB = new ProcessoDB();
            int qtdmovimentacao = posCliDB.TotalPosicao();
            dt.Columns.Add("Total", typeof(int));
            dt.Columns.Add("Posicao", typeof(string));
            for (int i = 1; i <= qtdmovimentacao; i++)
            {
                double total = posCliDB.SelectTotalGrafico(adv, i, initialDate, finalDate);
                posCli = posCliDB.Select(i);
                dt.Rows.Add(total, posCli.Descricao);
            }

            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChartPosicao);
                       function drawChartPosicao() {
        var data = google.visualization.arrayToDataTable([
             ['Posicao', 'quantidade'],
            ");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("['" + dt.Rows[i]["Posicao"].ToString() + "'," + dt.Rows[i]["Total"].ToString() + "],");
            }
            str.Append(" ]); ");
            str.Append(" var options = { ");
            str.Append(" is3D: true, ");
            str.Append(" backgroundColor: 'none', ");
            str.Append("            chartArea: { left: 100, width: '100%', height: '100%' } ");
            str.Append("         }; ");
            str.Append(" var chart = new google.visualization.PieChart(document.getElementById('piechart_Posicao'));");
            str.Append(" chart.draw(data, options);");
            str.Append("}");

            str.Append("</script>");

            lt.Text = str.ToString().Replace('*', '"');
        }
    }
}