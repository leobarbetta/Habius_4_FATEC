using Habius.Chart;
using Habius.Classes.Administrativo;
using Habius.Common;
using Habius.Persistencia.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Administrativo_Advogado_HomeAdvogado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ChartProcessos chartProcessos = new ChartProcessos();
            ChartPagamentos chartPagamentos = new ChartPagamentos();
            ChartDespesas chartDespesas = new ChartDespesas();
            ChartFinanceiro chartFincanceiro = new ChartFinanceiro();
            DateTime dtfinal = new DateTime();
            //bool teste = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

            //if (teste == true)
            //{
            string mes = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(DateTime.Today.Month);
            mes = char.ToUpper(mes[0]) + mes.Substring(1);
            //lblDespesa.Text = "Despesas " + mes;
            //lblPagamento.Text = "Pagamentos " + mes;

            DateTime dtinicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            if (DateTime.Now.Month == 12)
            {
                dtfinal = new DateTime(DateTime.Now.Year, 12, 31);
            }
            else
            {
                dtfinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
            }
            chartProcessos.BindChartTipoMovimentacaoAbertas(Convert.ToInt32(Session["Advogado"]), ltProcesso);
            chartPagamentos.BindChartTipoPagamentosByDateHome(Convert.ToInt32(Session["Advogado"]), ltPagamentos, dtinicial, dtfinal);
            chartDespesas.BindChartTipoGastosByDateHome(Convert.ToInt32(Session["Advogado"]), ltGastos, dtinicial, dtfinal);
            //}
            //else
            //{
            //    lblTeste.Text = "Sem conexão!";
            //}
            Master.Page.Title = "Home Page";
            CarregaProcesso();
        }
    }
    protected void gdvProcesso_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime data = new DateTime();
            try
            {
                //var teste = e.Row.Cells[3].Text;
                data = Convert.ToDateTime(e.Row.Cells[3].Text);
            }
            catch (FormatException)
            {

            }
            finally
            {
                if (data < DateTime.Today)
                {
                    string hexValue = "#B22222"; // You do need the hash
                    Color colour = System.Drawing.ColorTranslator.FromHtml(hexValue);
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(hexValue);
                }
                else if (data == DateTime.Today)
                {
                    e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
                }
            }
        }
    }
    private void CarregaProcesso()
    {
        ProcessoDB proDB = new ProcessoDB();
        DataSet ds = proDB.SelectAllOrderByDate(Convert.ToInt32(Session["Advogado"]));
        Function.CarregaGrid(ds, gdvProcesso, lbl);
    }
    protected void gdvProcesso_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvProcesso.PageIndex = e.NewPageIndex;
        CarregaProcesso();
    }
}