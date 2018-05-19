using Habius.Classes.Administrativo;
using Habius.Common;
using Habius.Persistencia.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Administrativo_Advogado_CadastrarGastos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["KeepModalOpen"] != null)
        {
            modalAddDespesas.Show();
        }
        if (!Page.IsPostBack)
        {
            CarregaCategoria();
            CarregaDespesaEscritorio(Convert.ToInt32(Session["Advogado"]));
        }
    }
    public void CarregaCategoria()
    {
        TipoDespesasDB bd = new TipoDespesasDB();
        DataSet ds = bd.SelectAllDDLCategoria();
        Function.CarregaDDL(ds, ddlCategoria, "TID_CODIGO", "TID_TIPODESPESA");
    }
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        Despesas despesas = new Despesas();
        DespesasDB despesasDB = new DespesasDB();
        TipoDespesa tipoDespesa = new TipoDespesa();
        TipoDespesasDB tipoDespesaDB = new TipoDespesasDB();
        Advogado adv = new Advogado();

        try
        {
            if (Convert.ToDateTime(txtData.Text) > DateTime.Today)
            {
                divmsgModalAddDespesaEscritorio.Attributes["class"] = "alert alert-danger";
                lblmsgModalAddDespesaEscritorio.Text = "A data dever ser menor que a de hoje";
            }
            else if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                divmsgModalAddDespesaEscritorio.Attributes["class"] = "alert alert-danger";
                lblmsgModalAddDespesaEscritorio.Text = "Insira uma Valor";
                txtValor.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtData.Text))
            {
                divmsgModalAddDespesaEscritorio.Attributes["class"] = "alert alert-danger";
                lblmsgModalAddDespesaEscritorio.Text = "Insira uma data";
                txtData.Focus();
            }
            else if (ddlCategoria.SelectedItem.Text == "Selecione")
            {
                divmsgModalAddDespesaEscritorio.Attributes["class"] = "alert alert-danger";
                lblmsgModalAddDespesaEscritorio.Text = "Selecione uma categoria";
                ddlCategoria.Focus();
            }
            else if (ddlCategoria.SelectedItem.Text == "Outros" && string.IsNullOrWhiteSpace(txtDescricaoDespesa.Text))
            {
                divmsgModalAddDespesaEscritorio.Attributes["class"] = "alert alert-danger";
                lblmsgModalAddDespesaEscritorio.Text = "Escreva sobre o que é a despesa";
                txtDescricaoDespesa.Focus();
            }
            else
            {

                adv.Codigo = Convert.ToInt32(Session["Advogado"]);
                despesas.PesCodigo = adv;

                despesas.Valor = Convert.ToDecimal(txtValor.Text);
                despesas.Data = Convert.ToDateTime(txtData.Text);
                despesas.Obs = txtObs.Text;
                tipoDespesa.Codigo = Convert.ToInt32(ddlCategoria.SelectedItem.Value);
                despesas.TipoDespesa = tipoDespesa;

                if (ddlCategoria.SelectedItem.Text == "Outros")
                {
                    tipoDespesaDB.Insert(txtDescricaoDespesa.Text);
                    tipoDespesa.Codigo = tipoDespesaDB.GetLastId(txtDescricaoDespesa.Text);
                }

                if (despesasDB.Insert(despesas))
                {
                    divmsgModalAddDespesaEscritorio.Attributes["class"] = "alert alert-success";
                    lblmsgModalAddDespesaEscritorio.Text = "Despesa cadastrada";
                    CarregaDespesaEscritorio(Convert.ToInt32(Session["Advogado"]));
                    LimpaModal();
                }
            }
        }
        catch (FormatException)
        {
            divmsgModalAddDespesaEscritorio.Attributes["class"] = "alert alert-danger";
            lblmsgModalAddDespesaEscritorio.Text = "Data invalida";
        }
    }
    protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategoria.SelectedItem.Text == "Outros")
        {
            //lblDescricaoDespesa.Enabled = true;
            txtDescricaoDespesa.Enabled = true;
            txtDescricaoDespesa.Focus();
        }
        else
        {
            //lblDescricaoDespesa.Enabled = false;
            txtDescricaoDespesa.Enabled = false;
            txtDescricaoDespesa.Text = string.Empty;
            ddlCategoria.Focus();
        }
    }
    protected void btnCalcelModalDespesas_Click(object sender, EventArgs e)
    {
        LimpaModal();
        Session["KeepModalOpen"] = null;
        modalAddDespesas.Hide();
    }
    protected void btnAddDespesa_Click(object sender, EventArgs e)
    {
        txtData.Text = DateTime.Today.ToString("dd/MM/yyyy");
        divmsgModalAddDespesaEscritorio.Attributes["class"] = "";
        lblmsgModalAddDespesaEscritorio.Text = string.Empty;
        //lblDescricaoDespesa.Enabled = false;
        txtDescricaoDespesa.Enabled = false;
        CarregaCategoria();
        Session["KeepModalOpen"] = 1;
        modalAddDespesas.Show();
    }
    private void CarregaDespesaEscritorio(int adv)
    {
        DespesasDB desDB = new DespesasDB();
        DataSet ds = desDB.SelectAllDespesasEscritorio(adv, DateTime.Today.Month);
        Function.CarregaGrid(ds, gdvDespesasEscritorio, lblqtdDespesa);
        lblValorTotalDespesa.Text = desDB.GetTotalDespesaEscritorio(adv, DateTime.Today.Month).ToString("C2"); ;
    }
    private void LimpaModal()
    {
        txtDescricaoDespesa.Text = string.Empty;
        txtDescricaoDespesa.Enabled = false;
        txtData.Text = DateTime.Today.ToString("dd/MM/yyyy");
        txtValor.Text = string.Empty;
        txtObs.Text = string.Empty;
        Function.LimpaDDL(ddlCategoria);
        modalAddDespesas.Show();
    }
    protected void gdvDespesasEscritorio_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvDespesasEscritorio.PageIndex = e.NewPageIndex;
        CarregaDespesaEscritorio(Convert.ToInt32(Session["Advogado"]));
    }
}