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

public partial class Pages_Administrativo_Advogado_CadastraCustoProcesso : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ManterModalAddCustoAberto"] != null)
        {
            modalAddCusto.Show();
        }
        if (Session["Custo"] != null)
        {
            modalEditarCusto.Show();
        }
        if (!Page.IsPostBack)
        {
            ProcessoDB proDB = new ProcessoDB();
            Processo pro = proDB.Select(Convert.ToInt32(Session["ProcessoCusto"]));
            lblProcesso.Text = pro.NumeroProcesso;
            txtDataCusto.Text = DateTime.Today.ToString("dd/MM/yyyy");
            CarregaGridElblTotal(Convert.ToInt32(Session["ProcessoCusto"]));
            CarregaTipoDespesas(ddlTipoCusto);
        }
    }
    protected void gridCustos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridCustos.PageIndex = e.NewPageIndex;
        CarregaGridElblTotal(Convert.ToInt32(Session["ProcessoCusto"]));
    }
    protected void btnAddCustoProcesso_Click(object sender, EventArgs e)
    {
        lblMsgAddCusto.Text = string.Empty;
        divMsgAddCusto.Attributes["class"] = "";
        Session["ManterModalAddCustoAberto"] = 1;
        modalAddCusto.Show();
    }
    protected void btnSalvarCusto_Click(object sender, EventArgs e)
    {
        InsertCusto(Convert.ToInt32(Session["ProcessoCusto"]));
    }
    protected void ddlTipoCusto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoCusto.SelectedItem.Text == "Outros")
        {
            txtDescricaoCusto.Enabled = true;
            //lblDescricaoCusto.Enabled = true;
            txtDescricaoCusto.Focus();
        }
        else
        {
            txtDescricaoCusto.Enabled = false;
            //lblDescricaoCusto.Enabled = false;
            ddlTipoCusto.Focus();
            txtDescricaoCusto.Text = string.Empty;
        }
    }
    protected void btnCalcelModalCusto_Click(object sender, EventArgs e)
    {
        Session["ManterModalAddCustoAberto"] = null;
        modalAddCusto.Hide();
    }
    protected void btnCancelarEditar_Click(object sender, EventArgs e)
    {
        Session["Custo"] = null;
        modalEditarCusto.Hide();
    }
    protected void btnSalvarEditarCusto_Click(object sender, EventArgs e)
    {
        EditarCusto(Convert.ToInt32(Session["Custo"]));
    }
    protected void ddlEditarTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEditarTipo.SelectedItem.Text == "Outros")
        {
            txtEditarDescricao.Enabled = true;
            txtEditarDescricao.Focus();
        }
        else
        {
            txtEditarDescricao.Enabled = false;
            ddlEditarTipo.Focus();
            txtEditarDescricao.Text = string.Empty;

        }
    }
    protected void gridCustos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "EditarCusto":
                lblMsgEditarCusto.Text = string.Empty;
                divMsgEditarCusto.Attributes["class"] = "";
                Session["Custo"] = e.CommandArgument;
                CarregaEditarCusto(Convert.ToInt32(Session["Custo"]));
                modalEditarCusto.Show();
                break;
        }
    }
    private void InsertCusto(int processo)
    {
        Despesas despesas = new Despesas();
        DespesasDB despesasDB = new DespesasDB();
        TipoDespesa tipoDespesa = new TipoDespesa();
        TipoDespesasDB tipoDespesaDB = new TipoDespesasDB();
        Processo pro = new Processo();
        Advogado adv = new Advogado();
        try
        {
            if (Convert.ToDateTime(txtDataCusto.Text) > DateTime.Today)
            {
                lblMsgAddCusto.Text = "A data deve ser menos que a data de hoje!";
                divMsgAddCusto.Attributes["class"] = "alert alert-danger";
            }
            else if (ddlTipoCusto.SelectedItem.Text == "Selecione")
            {
                lblMsgAddCusto.Text = "Escolha um tipo de custo!";
                divMsgAddCusto.Attributes["class"] = "alert alert-danger";
                ddlTipoCusto.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtDataCusto.Text))
            {
                lblMsgAddCusto.Text = "Preencha a data!";
                divMsgAddCusto.Attributes["class"] = "alert alert-danger";
                txtDataCusto.Focus();
            }
            else if (ddlTipoCusto.SelectedItem.Text == "Outros" && string.IsNullOrWhiteSpace(txtDescricaoCusto.Text))
            {
                lblMsgAddCusto.Text = "Escreva uma descrição!";
                divMsgAddCusto.Attributes["class"] = "alert alert-danger";
                txtDescricaoCusto.Focus();
            }
            else
            {
                adv.Codigo = Convert.ToInt32(Session["Advogado"]);
                despesas.PesCodigo = adv;
                despesas.Valor = Convert.ToDecimal(txtValor.Text);
                despesas.Data = Convert.ToDateTime(txtDataCusto.Text);
                despesas.Obs = txtObsCusto.Text;
                tipoDespesa.Codigo = Convert.ToInt32(ddlTipoCusto.SelectedItem.Value);
                despesas.TipoDespesa = tipoDespesa;
                pro.Codigo = processo;
                despesas.Processo = pro;

                if (ddlTipoCusto.SelectedItem.Text == "Outros")
                {
                    tipoDespesaDB.InsertCustoProcesso(txtDescricaoCusto.Text);
                    tipoDespesa.Codigo = tipoDespesaDB.GetLastId(txtDescricaoCusto.Text);
                }
                if (despesasDB.Insert(despesas))
                {
                    txtDescricaoCusto.Text = string.Empty;
                    txtDescricaoCusto.Enabled = false;
                    //lblDescricaoCusto.Enabled = false;
                    txtDataCusto.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtValor.Text = string.Empty;
                    txtObsCusto.Text = string.Empty;
                    Function.LimpaDDL(ddlTipoCusto);
                    divMsgAddCusto.Attributes["class"] = "alert alert-success";
                    lblMsgAddCusto.Text = "Despesa cadastrada";
                    CarregaGridElblTotal(processo);
                }
            }
        }
        catch (FormatException)
        {
            lblMsgAddCusto.Text = "Data Invalida";
            divMsgAddCusto.Attributes["class"] = "alert alert-danger";
        }
    }
    public void CarregaEditarCusto(int id)
    {
        Despesas despesas = new Despesas();
        DespesasDB despesasDB = new DespesasDB();
        TipoDespesa tipoDespesa = new TipoDespesa();
        TipoDespesasDB tipoDespesaDB = new TipoDespesasDB();

        despesas = despesasDB.Select(id);
        txtEditarValor.Text = Convert.ToString(despesas.Valor);
        txtEditarData.Text = despesas.Data.ToString("dd/MM/yyyy");
        txtEditarObs.Text = despesas.Obs;

        CarregaTipoDespesas(ddlEditarTipo);
        Function.CarregaItemDDLByCodigo(ddlEditarTipo, despesas.TipoDespesa.Codigo);
    }

    public void EditarCusto(int id)
    {
        Despesas despesas = new Despesas();
        DespesasDB despesasDB = new DespesasDB();
        TipoDespesa tipoDespesa = new TipoDespesa();
        TipoDespesasDB tipoDespesaDB = new TipoDespesasDB();

        try
        {
            if (Convert.ToDateTime(txtEditarData.Text) > DateTime.Today)
            {
                lblMsgEditarCusto.Text = "A data deve ser menos que a data de hoje!";
                divMsgEditarCusto.Attributes["class"] = "alert alert-danger";
            }
            else if (ddlEditarTipo.SelectedItem.Text == "Selecione")
            {
                lblMsgEditarCusto.Text = "Escolha um tipo de custo!";
                divMsgEditarCusto.Attributes["class"] = "alert alert-danger";
                ddlEditarTipo.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtEditarData.Text))
            {
                lblMsgEditarCusto.Text = "Preencha a data!";
                divMsgEditarCusto.Attributes["class"] = "alert alert-danger";
                txtEditarData.Focus();
            }
            else if (ddlEditarTipo.SelectedItem.Text == "Outros" && string.IsNullOrWhiteSpace(txtEditarDescricao.Text))
            {
                lblMsgEditarCusto.Text = "Escreva uma descrição!";
                divMsgEditarCusto.Attributes["class"] = "alert alert-danger";
                txtEditarDescricao.Focus();
            }
            else
            {

                despesas = despesasDB.Select(id);
                despesas.Valor = Convert.ToDecimal(txtEditarValor.Text);
                despesas.Data = Convert.ToDateTime(txtEditarData.Text);
                despesas.Obs = txtEditarObs.Text;
                tipoDespesa.Codigo = Convert.ToInt32(ddlEditarTipo.SelectedItem.Value);
                despesas.TipoDespesa = tipoDespesa;
                if (ddlEditarTipo.SelectedItem.Text == "Outros")
                {
                    tipoDespesaDB.InsertCustoProcesso(txtEditarDescricao.Text);
                    tipoDespesa.Codigo = tipoDespesaDB.GetLastId(txtEditarDescricao.Text);
                }
                despesasDB.Update(despesas);
                CarregaGridElblTotal(Convert.ToInt32(Session["ProcessoCusto"]));
                Function.CarregaItemDDLByCodigo(ddlEditarTipo, tipoDespesa.Codigo);
                lblMsgEditarCusto.Text = "Custo editado com sucesso";
                divMsgEditarCusto.Attributes["class"] = "alert alert-success";
            }
        }
        catch (FormatException)
        {
            lblMsgEditarCusto.Text = "Data invalida";
            divMsgEditarCusto.Attributes["class"] = "alert alert-danger";
        }
    }
    private void CarregaTipoDespesas(DropDownList ddl)
    {
        TipoDespesasDB tipDB = new TipoDespesasDB();
        DataSet ds = tipDB.SelectAllDDLCategoriaProcesso();
        Function.CarregaDDL(ds, ddl, "TID_CODIGO", "TID_TIPODESPESA");
    }
    private void CarregaGridElblTotal(int processo)
    {
        DespesasDB desDB = new DespesasDB();
        DataSet ds = desDB.SelectAllCustoProcesso(processo);
        Function.CarregaGrid(ds, gridCustos, lblqtddespesaprocesso);

        lblValorTotalCusto.Text = desDB.TotalCustoProcesso(processo).ToString("C2");
    }
}