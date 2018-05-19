using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Habius.Persistencia;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Web.UI.DataVisualization.Charting;
using System.Net;

public partial class Pages_Administrativo_Advogado_TestesLeo : System.Web.UI.Page
{
    //private bool CheckForInternetConnection()
    //{
    //    //try
    //    //{
    //    //    using (var client = new WebClient())
    //    //    {
    //    //        using (var stream = client.OpenRead("http://www.google.com"))
    //    //        {
    //    //            return true;
    //    //        }
    //    //    }
    //    //}
    //    //catch
    //    //{
    //    //    return false;
    //    //}
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!CheckForInternetConnection())
        //{
        //    lblTeste.Text = "Não há conexão com a internet";
        //}
        
        //if (Page.IsPostBack == false)
        //{
        //    BindChart(Convert.ToInt32(Session["Advogado"]));
        //}
    }

    private void BindChart(int adv)
    {
        DataTable dt = new DataTable();

        StringBuilder str = new StringBuilder();

        dt = GetData(adv);

        str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
        var data = google.visualization.arrayToDataTable([
             ['2015', 'Despesas'],
");
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            str.Append("['" + dt.Rows[i]["Mes"].ToString() + "'," + dt.Rows[i]["Total"].ToString() + "],");
        }
        str.Append(" ]); ");
        str.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));");
        str.Append(" chart.draw(data, {width: 700, height: 500, title: 'Despesas',");
        str.Append("hAxis: {title: 'Meses', titleTextStyle: {color: 'green'}}");
        str.Append("}); }");
        str.Append("</script>");
        lt.Text = str.ToString().Replace('*', '"');
    }
    public DataTable GetData(int adv)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id", typeof(int));
        dt.Columns.Add("Total", typeof(int));
        dt.Columns.Add("Mes", typeof(string));
        for (int i = 1; i <= 12; i++)
        {
            double total = GetTotal(adv, i);
            string mes = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
            dt.Rows.Add(i, total, mes);
        }
        return dt;
    }
    public double GetTotal(int adv, int mes)
    {
        double total = 0;

        IDbCommand objCommand;
        IDbConnection objConexao;
        IDataReader objDataReader;

        string sql = "SELECT sum(D.DES_VALOR) AS TOTAL FROM DES_DESPESA D LEFT JOIN TID_TIPODESPESA T ON (D.TID_CODIGO = T.TID_CODIGO) WHERE T.TID_CATEGORIA =1 AND D.PES_CODIGO_ADV = ?adv AND month(D.DES_DATA) = ?mes ORDER BY D.DES_DATA";

        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
        objCommand.Parameters.Add(Mapped.Parameter("?mes", mes));

        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            if (objDataReader["TOTAL"] != DBNull.Value)
                total = Convert.ToInt32(objDataReader["TOTAL"]);
        }

        objConexao.Close();
        objDataReader.Close();

        objConexao.Dispose();
        objCommand.Dispose();
        objDataReader.Dispose();

        return total;
    }
}