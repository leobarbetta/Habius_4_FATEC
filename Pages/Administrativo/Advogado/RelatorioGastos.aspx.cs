using Habius.Chart;
using Habius.Classes.Administrativo;
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

public partial class Pages_Administrativo_Advogado_RelatorioGastos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ChartDespesas chartDespesas = new ChartDespesas();
            DateTime dtinitial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtfinal = DateTime.Today;
            txtInitialDate.Text = dtinitial.ToString("dd/MM/yyyy");
            txtFinalDate.Text = dtfinal.ToString("dd/MM/yyyy");
            chartDespesas.BindChartTipoGastosByDate(Convert.ToInt32(Session["Advogado"]), ltGastos, dtinitial, dtfinal);
            chartDespesas.BindChartGastos(Convert.ToInt32(Session["Advogado"]), lt);
            int mesAtual = DateTime.Today.Month;
            CarregaDespesaEscritorio(Convert.ToInt32(Session["Advogado"]), dtinitial, dtfinal);
            CarregaTotalLabel(Convert.ToInt32(Session["Advogado"]), dtinitial, dtfinal);
        }
    }
    protected void btnBusca_Click(object sender, EventArgs e)
    {
        ChartDespesas chartDespesas = new ChartDespesas();
        try
        {
            if (txtInitialDate.Text == "__/__/____" && txtFinalDate.Text == "__/__/____")
            {
                DateTime dtinitial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime dtfinal = DateTime.Today;
                txtInitialDate.Text = dtinitial.ToString("dd/MM/yyyy");
                txtFinalDate.Text = dtfinal.ToString("dd/MM/yyyy");
                chartDespesas.BindChartTipoGastosByDate(Convert.ToInt32(Session["Advogado"]), ltGastos, Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
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
                Label lbl = new Label();
                lbl.Text = lt.Text;
                divMensagem.Attributes["class"] = "";
                lblMensagem.Text = string.Empty;
                chartDespesas.BindChartTipoGastosByDate(Convert.ToInt32(Session["Advogado"]), ltGastos, Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
            }
            CarregaDespesaEscritorio(Convert.ToInt32(Session["Advogado"]), Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
            CarregaTotalLabel(Convert.ToInt32(Session["Advogado"]), Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
        }
        catch (FormatException)
        {
            divMensagem.Attributes["class"] = "alert alert-danger";
            lblMensagem.Text = "Data invalida";
        }

    }
    private void CarregaDespesaEscritorio(int adv, DateTime initialDate, DateTime finalDate)
    {
        DespesasDB desDB = new DespesasDB();
        DataSet ds = desDB.SelectAllDespesasEscritorioByDate(adv, initialDate, finalDate);
        Function.CarregaGrid(ds, gdvDespesasEscritorio, lblqtdDespesa);
    }
    private void CarregaTotalLabel(int adv, DateTime initialDate, DateTime finalDate)
    {
        DespesasDB desDB = new DespesasDB();
        lblTotalDespesa.Text = desDB.GetTotalDespesaEscritorioByDate(adv, initialDate, finalDate).ToString("C2");
    }

    protected void gdvDespesasEscritorio_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvDespesasEscritorio.PageIndex = e.NewPageIndex;
        int mesAtual = DateTime.Today.Month;
        CarregaDespesaEscritorio(Convert.ToInt32(Session["Advogado"]), Convert.ToDateTime(txtInitialDate.Text), Convert.ToDateTime(txtFinalDate.Text));
    }

}