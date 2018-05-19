using Habius.Common;
using Habius.Persistencia.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Habius.Persistencia;
using Habius.Classes.Administrativo;
using System.Text;
using Habius.Chart;

public partial class Pages_Administrativo_Advogado_RelatorioProcessos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DateTime FirstDayOfYear = new DateTime(DateTime.Now.Year, 01, 01);
            txtInitialDate.Text = FirstDayOfYear.ToString("dd/MM/yyyy");
            txtFinalDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            ChartProcessos chartProcessos = new ChartProcessos();
            chartProcessos.BindChartTipoMovimentacao(Convert.ToInt32(Session["Advogado"]), ltMovimentacao, Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
            chartProcessos.BindChartClasse(Convert.ToInt32(Session["Advogado"]), ltClasses, Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
            chartProcessos.BindChartPosicaoCliente(Convert.ToInt32(Session["Advogado"]), ltPosicao, Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
            CarregaProcesso(Convert.ToInt32(Session["Advogado"]), Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
            divMensagem.Attributes["class"] = "";
            lblMensagem.Text = string.Empty;
        }
    }
    protected void btnBusca_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtInitialDate.Text == "__/__/____" && txtFinalDate.Text == "__/__/____")
            {
                DateTime FirstDayOfYear = new DateTime(DateTime.Now.Year, 01, 01);
                txtInitialDate.Text = FirstDayOfYear.ToString("dd/MM/yyyy");
                txtFinalDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                ChartProcessos chartProcessos = new ChartProcessos();
                chartProcessos.BindChartTipoMovimentacao(Convert.ToInt32(Session["Advogado"]), ltMovimentacao, Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
                chartProcessos.BindChartClasse(Convert.ToInt32(Session["Advogado"]), ltClasses, Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
                chartProcessos.BindChartPosicaoCliente(Convert.ToInt32(Session["Advogado"]), ltPosicao, Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
                CarregaProcesso(Convert.ToInt32(Session["Advogado"]), Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
                divMensagem.Attributes["class"] = "";
                lblMensagem.Text = string.Empty;
            }
            else if (txtInitialDate.Text == "__/__/____")
            {
                divMensagem.Attributes["class"] = "alert alert-danger";
                lblMensagem.Text = "Entre com a data inicial";
                txtInitialDate.Focus();
            }
            else if (txtFinalDate.Text == "__/__/____")
            {
                divMensagem.Attributes["class"] = "alert alert-danger";
                lblMensagem.Text = "Entre com a data final";
                txtFinalDate.Focus();
            }
            else if (Convert.ToDateTime(txtInitialDate.Text) >= Convert.ToDateTime(txtFinalDate.Text))
            {
                divMensagem.Attributes["class"] = "alert alert-danger";
                lblMensagem.Text = "A data incial deve ser menor que a data final";
            }
            else
            {
                ChartProcessos chartProcessos = new ChartProcessos();
                CarregaProcesso(Convert.ToInt32(Session["Advogado"]), Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
                chartProcessos.BindChartTipoMovimentacao(Convert.ToInt32(Session["Advogado"]), ltMovimentacao, Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
                chartProcessos.BindChartClasse(Convert.ToInt32(Session["Advogado"]), ltClasses, Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
                chartProcessos.BindChartPosicaoCliente(Convert.ToInt32(Session["Advogado"]), ltPosicao, Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
                divMensagem.Attributes["class"] = "";
                lblMensagem.Text = string.Empty;
            }
        }
        catch (FormatException)
        {
            divMensagem.Attributes["class"] = "alert alert-danger";
            lblMensagem.Text = "DataInvalida";
        }
    }
    private void CarregaProcesso(int adv, DateTime initialDate, DateTime finalDate)
    {
        ProcessoDB proDB = new ProcessoDB();
        DataSet ds = proDB.SelectAllProcessosByDate(adv, initialDate, finalDate);
        Function.CarregaGrid(ds, gdvProcesso, lblQtd);
    }
    protected void gdvProcesso_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvProcesso.PageIndex = e.NewPageIndex;
        CarregaProcesso(Convert.ToInt32(Session["Advogado"]), Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
    }
}