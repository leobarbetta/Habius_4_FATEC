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

public partial class Pages_Administrativo_Advogado_RelatorioFinanceiro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ChartFinanceiro chartFincanceiro = new ChartFinanceiro();
        chartFincanceiro.BindChartFinanceiro(Convert.ToInt32(Session["Advogado"]), lt);
        int mesAtual = DateTime.Today.Month;
        CarregaLabels(Convert.ToInt32(Session["Advogado"]), mesAtual);
    }
    private void CarregaLabels(int adv, int mes)
    {
        PagamentoDB pagDB = new PagamentoDB();
        lblTotalPagamento.Text = pagDB.TotalPagamentoMes(adv, mes).ToString("C2");
        double pagamento = pagDB.TotalPagamentoMes(adv, mes);
        DespesasDB desDB = new DespesasDB();
        lblTotalDespesa.Text = desDB.GetTotalDespesaEscritorio(adv, mes).ToString("C2");
        double despesa = desDB.GetTotalDespesaEscritorio(adv, mes);

        double total = pagamento - despesa;
        lblFaturamento.Text = total.ToString("C2");
    }
}