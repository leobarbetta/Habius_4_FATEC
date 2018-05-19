using Habius.Chart;
using Habius.Common;
using Habius.Persistencia.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Administrativo_Advogado_RelatorioPagamentos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            DateTime dtinitial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtfinal = DateTime.Today;
            txtInitialDate.Text = dtinitial.ToString("dd/MM/yyyy");
            txtFinalDate.Text = dtfinal.ToString("dd/MM/yyyy");
            ChartPagamentos chartPagamentos = new ChartPagamentos();
            chartPagamentos.BindChartTipoPagamentosByDate(Convert.ToInt32(Session["Advogado"]), ltPagamentos, dtinitial, dtfinal);
            chartPagamentos.BindChartPagamentos(Convert.ToInt32(Session["Advogado"]), lt);
            int mes = DateTime.Today.Month;
            CarregaPagamentos(Convert.ToInt32(Session["Advogado"]), dtinitial, dtfinal);
            CarregaTotalLabel(Convert.ToInt32(Session["Advogado"]), dtinitial, dtfinal);
        }
    }
    private void CarregaPagamentos(int adv, DateTime initialdate, DateTime finaldate)
    {
        PagamentoDB pagDB = new PagamentoDB();
        DataSet ds = pagDB.SelectAllPagamentos(adv, initialdate, finaldate);
        Function.CarregaGrid(ds, gdvPagamento, lblqtdPagamento);
    }
    private void CarregaTotalLabel(int adv, DateTime initialdate, DateTime finaldate)
    {
        PagamentoDB pagDB = new PagamentoDB();
        lblTotalPagamento.Text = pagDB.TotalPagamentoByDate(adv, initialdate, finaldate).ToString("C2");
    }
    protected void gdvPagamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvPagamento.PageIndex = e.NewPageIndex;
        CarregaPagamentos(Convert.ToInt32(Session["Advogado"]), Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
    }
    protected void btnBusca_Click(object sender, EventArgs e)
    {
        ChartPagamentos chartPagamentos = new ChartPagamentos();
        try
        {
            if (txtInitialDate.Text == "__/__/____" && txtFinalDate.Text == "__/__/____")
            {
                DateTime dtinitial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime dtfinal = DateTime.Today;
                txtInitialDate.Text = dtinitial.ToString("dd/MM/yyyy");
                txtFinalDate.Text = dtfinal.ToString("dd/MM/yyyy");
                chartPagamentos.BindChartTipoPagamentosByDate(Convert.ToInt32(Session["Advogado"]), ltPagamentos, dtinitial, dtfinal);

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
                chartPagamentos.BindChartTipoPagamentosByDate(Convert.ToInt32(Session["Advogado"]), ltPagamentos, Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
                Label lbl = new Label();
                lbl.Text = lt.Text;
                CarregaPagamentos(Convert.ToInt32(Session["Advogado"]), Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
                CarregaTotalLabel(Convert.ToInt32(Session["Advogado"]), Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
                divMensagem.Attributes["class"] = "";
                lblMensagem.Text = string.Empty;
            }
        }
        catch (FormatException)
        {
            divMensagem.Attributes["class"] = "alert alert-danger";
            lblMensagem.Text = "Data invalida";
        }
    }
}