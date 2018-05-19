using Habius.Classes.Administrativo;
using Habius.Common;
using Habius.Persistencia.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Administrativo_Advogado_CadastraAgenda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Agenda";
        CarregaGrid();
        if (Session["Agenda"] != null)
        {
            Session["Agenda2"] = Session["Agenda"];
            CarregaAgenda(Convert.ToInt32(Session["Agenda"]));
        }
        else
        {
            if (!Page.IsPostBack)
            {
                CarregaAgenda();
                txtData.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        Agenda age = new Agenda();
        AgendaDB ageDB = new AgendaDB();
        Advogado adv = new Advogado();
        adv.Codigo = Convert.ToInt32(Session["Advogado"]);
        age.PessoaAdvogado = adv;

        if (string.IsNullOrWhiteSpace(txtTitulo.Text))
        {
            lblMensagem.Text = "Insira um titulo";
            divMensagem.Attributes["class"] = "alert alert-danger";
            txtTitulo.Focus();
        }
        else if (string.IsNullOrWhiteSpace(txtDescricao.Text))
        {
            lblMensagem.Text = "Insira uma descrição para o evento";
            divMensagem.Attributes["class"] = "alert alert-danger";
            txtDescricao.Focus();
        }
        else if (string.IsNullOrWhiteSpace(txtData.Text))
        {
            lblMensagem.Text = "Insira uma Data";
            divMensagem.Attributes["class"] = "alert alert-danger";
            txtData.Focus();
        }
        else if (Convert.ToDateTime(txtData.Text) < DateTime.Today)
        {
            lblMensagem.Text = "Favor selecione uma data maior que a de hoje";
            divMensagem.Attributes["class"] = "alert alert-danger";
            txtData.Focus();
        }
        else if (string.IsNullOrWhiteSpace(txtHora.Text))
        {
            lblMensagem.Text = "Favor selecione uma hora para o evento";
            divMensagem.Attributes["class"] = "alert alert-danger";
            txtHora.Focus();
        }
        else
        {
            try
            {
                TimeSpan hora = Convert.ToDateTime(txtHora.Text).TimeOfDay;
                DateTime data = Convert.ToDateTime(txtData.Text);
                age.DataFinalizacao = data + hora;
                age.Descricao = txtDescricao.Text;
                age.Titulo = txtTitulo.Text;

                if (ageDB.Insert(age))
                {
                    lblMensagem.Text = "Evento Inserido com sucesso!";
                    divMensagem.Attributes["class"] = "alert alert-success";
                    txtTitulo.Text = string.Empty;
                    txtHora.Text = string.Empty;
                    txtDescricao.Text = string.Empty;
                    txtData.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    CarregaGrid();
                }
                else
                {
                    lblMensagem.Text = "Erro ao cadastrar";
                    divMensagem.Attributes["class"] = "alert alert-danger";
                }
            }
            catch (FormatException)
            {
                lblMensagem.Text = "Data ou Hora invalida";
                divMensagem.Attributes["class"] = "alert alert-danger";
                txtData.Focus();
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Agenda age = new Agenda();
        AgendaDB ageDB = new AgendaDB();
        age = ageDB.Select(Convert.ToInt32(Session["Agenda2"]));

        if (string.IsNullOrWhiteSpace(txtTitulo.Text))
        {
            lblMensagem.Text = "Insira um titulo";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtDescricao.Text))
        {
            lblMensagem.Text = "Insira uma descrição para o evento";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtData.Text))
        {
            lblMensagem.Text = "Insira uma Data";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtHora.Text))
        {
            lblMensagem.Text = "Favor selecione uma hora para o evento";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else
        {
            TimeSpan hora = Convert.ToDateTime(txtHora.Text).TimeOfDay;
            DateTime data = Convert.ToDateTime(txtData.Text);
            DateTime datahora = data + hora;

            age.DataFinalizacao = datahora;
            age.Descricao = txtDescricao.Text;
            age.Titulo = txtTitulo.Text;

            if (ageDB.Update(age))
            {
                lblMensagem.Text = "Evento atualizado!";
                divMensagem.Attributes["class"] = "alert alert-success";
                txtData.Text = DateTime.Today.ToShortDateString();
                txtHora.Text = string.Empty;
                txtTitulo.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                btnCadastrar.Visible = true;
                btnUpdate.Visible = false;
                btnCancelar.Visible = false;
                Session["Agenda"] = null;
                Session["Agenda2"] = null;
                CarregaGrid();
            }
            else
            {
                //msg de erro
            }
        }
    }
    public void CarregaGrid()
    {
        AgendaDB ageDB = new AgendaDB();
        DataSet ds = ageDB.SelectAll(Convert.ToInt32(Session["Advogado"]));
        Function.CarregaGrid(ds, gdvAgenda, lblqtd);
    }
    private void CarregaAgenda(int id)
    {
        Agenda age = new Agenda();
        AgendaDB ageDB = new AgendaDB();
        age = ageDB.Select(id);
        DateTime datahora = age.DataFinalizacao;
       
        txtData.Text = datahora.Date.ToString("dd/MM/yyyy");
        txtHora.Text = datahora.ToString("t");
        txtTitulo.Text = age.Titulo;
        txtDescricao.Text = age.Descricao;
        btnCadastrar.Visible = false;
        btnUpdate.Visible = true;
        btnCancelar.Visible = true;
        Session["Agenda"] = null;
    }
    protected void gdvAgenda_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Agenda age = new Agenda();
        AgendaDB ageDB = new AgendaDB();
        switch (e.CommandName)
        {
            case "Finalizar":
                age.Finalizado = 1;
                age.Codigo = Convert.ToInt32(e.CommandArgument);
                ageDB.Finaliza(age);
                CarregaAgenda();
                break;
            case "Editar":
                Session["Agenda"] = e.CommandArgument;
                Response.Redirect("../../../Pages/Administrativo/Advogado/CadastraAgenda.aspx");
                break;
        }
    }
    protected void gdvAgenda_Sorting(object sender, GridViewSortEventArgs e)
    {
        CarregaAgenda();
        DataView data = gdvAgenda.DataSource as DataView;

        if (e.SortDirection == SortDirection.Ascending)
        {
            data.Sort = e.SortExpression + " DESC ";

            gdvAgenda.DataSource = data;
            gdvAgenda.DataBind();
        }
        else
        {
            data.Sort = e.SortExpression + " ASC ";

            gdvAgenda.DataSource = data;
            gdvAgenda.DataBind();
        }
    }
    protected void gdvAgenda_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvAgenda.PageIndex = e.NewPageIndex;
        CarregaAgenda();
    }
    public void CarregaAgenda()
    {
        AgendaDB ageDB = new AgendaDB();
        DataSet ds = ageDB.SelectAll(Convert.ToInt32(Session["Advogado"]));
        Function.CarregaGrid(ds, gdvAgenda, lblqtd);
    }
    protected void gdvAgenda_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime data = Convert.ToDateTime(e.Row.Cells[2].Text);

            if (data < DateTime.Today)
            {
                string hexValue = "#ffcbcb"; // You do need the hash
                Color colour = System.Drawing.ColorTranslator.FromHtml(hexValue);
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(hexValue);
            }
            else if (data > DateTime.Today)
            {
                string hexValue = "#ffd8c1"; // You do need the hash
                Color colour = System.Drawing.ColorTranslator.FromHtml(hexValue);
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(hexValue);
            }
            else if (data == DateTime.Today)
            {
                string hexValue = "#eeffed"; // You do need the hash
                Color colour = System.Drawing.ColorTranslator.FromHtml(hexValue);
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(hexValue);
            }
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        txtData.Text = DateTime.Today.ToShortDateString();
        txtHora.Text = string.Empty;
        txtTitulo.Text = string.Empty;
        txtDescricao.Text = string.Empty;
        btnCadastrar.Visible = true;
        btnUpdate.Visible = false;
        btnCancelar.Visible = false;
        Session["Agenda"] = null;
        Session["Agenda2"] = null;
    }
}