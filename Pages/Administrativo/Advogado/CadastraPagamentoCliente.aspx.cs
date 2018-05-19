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

public partial class Pages_Administrativo_Advogado_CadastraPagamentoCliente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ModalPagamento"] != null)
        {
            modalAddPagamento.Show();
        }
        else
        {
            txtValor.Text = string.Empty;
            lblMsgAddPagamento.Text = string.Empty;
            divMsgAddPagamento.Attributes["class"] = "";
            txtDataPagamento.Text = DateTime.Today.ToString("dd/MM/yyyy");
            Function.LimpaDDL(ddlProcesso);
            Function.LimpaDDL(ddlServico);
            txtDescricaoServico.Enabled = false;
            ddlProcesso.Enabled = false;
        }
        if (!Page.IsPostBack)
        {
            CarregaPagamentos(Convert.ToInt32(Session["PagamentoCliente"]));
            CarregaProcessosDoCliente(Convert.ToInt32(Session["PagamentoCliente"]));
            PessoaDB pesDB = new PessoaDB();
            Pessoa pes = pesDB.SelectGenerico(Convert.ToInt32(Session["PagamentoCliente"]));
            if (pes.Nivel == 3)
            {
                ClienteFisicoDB cliFSDB = new ClienteFisicoDB();
                ClienteFisico cliFS = cliFSDB.Select(pes.Codigo);
                ContatoDB conDB = new ContatoDB();
                Contato con = conDB.SelectContato(cliFS.ContatoPessoa.Codigo);
                lblNome.Text = con.Nome;
            }
            else if (pes.Nivel == 4)
            {
                ClienteJuridicoDB cliJuDB = new ClienteJuridicoDB();
                ClienteJuridico cliJu = cliJuDB.Select(pes.Codigo);
                ContatoDB conDB = new ContatoDB();
                Contato con = conDB.SelectContato(pes.Codigo);
                lblNome.Text = con.Nome;
            }
            PagamentoDB pagDB = new PagamentoDB();
            lblValorTotalPagamento.Text = pagDB.TotalPagamentoCliente(pes.Codigo).ToString("C2");
            CarregaServico();
        }
    }
    protected void gridPagamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridPagamento.PageIndex = e.NewPageIndex;
        CarregaPagamentos(Convert.ToInt32(Session["PagamentoCliente"]));
    }
    protected void btnAbrirModal_Click(object sender, EventArgs e)
    {
        Session["ModalPagamento"] = 1;
        modalAddPagamento.Show();
    }
    protected void btnAddPagamento_Click(object sender, EventArgs e)
    {
        InsertPagamento();
        CarregaPagamentos(Convert.ToInt32(Session["PagamentoCliente"]));
    }
    protected void ddlServico_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServico.SelectedItem.Text == "Outros")
        {
            txtDescricaoServico.Enabled = true;
            ddlProcesso.Enabled = false;
            Function.LimpaDDL(ddlProcesso);
            txtDescricaoServico.Focus();
        }
        else if (ddlServico.SelectedItem.Text == "Processo")
        {
            txtDescricaoServico.Enabled = false;
            ddlProcesso.Enabled = true;
            txtDescricaoServico.Text = string.Empty;
            ddlProcesso.Focus();
        }
        else
        {
            txtDescricaoServico.Enabled = false;
            ddlProcesso.Enabled = false;
            txtDescricaoServico.Text = string.Empty;
            Function.LimpaDDL(ddlProcesso);
            ddlServico.Focus();
        }
    }
    protected void btnCalcelModalAddPagamento_Click(object sender, EventArgs e)
    {
        Session["ModalPagamento"] = null;
        modalAddPagamento.Hide();
    }
    private void CarregaProcessosDoCliente(int id)
    {
        ProcessoDB proDB = new ProcessoDB();
        DataSet ds = proDB.SelectAllByCliente(id);
        Function.CarregaDDL(ds, ddlProcesso, "CODIGO_PROCESSO", "NUMEROPROCESSO");
    }
    private void CarregaPagamentos(int cliente)
    {
        PagamentoDB pagDB = new PagamentoDB();
        DataSet ds = pagDB.SelectAllPagamentosByCliente(cliente);
        Function.CarregaGrid(ds, gridPagamento, lblqtdPagamentoprocesso);
    }
    private void CarregaServico()
    {
        ServicoDB sevDB = new ServicoDB();
        DataSet ds = sevDB.SelectAll();
        Function.CarregaDDL(ds, ddlServico, "SEV_CODIGO", "SEV_SERVICO");
    }
    private bool InsertPagamento()
    {
        try
        {
            if (Convert.ToDateTime(txtDataPagamento.Text) > DateTime.Today)
            {
                lblMsgAddPagamento.Text = "A data não pode ser maior que hoje";
                divMsgAddPagamento.Attributes["class"] = "alert alert-danger";
            }
            else if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                lblMsgAddPagamento.Text = "Insira um valor";
                divMsgAddPagamento.Attributes["class"] = "alert alert-danger";
            }
            else if (string.IsNullOrWhiteSpace(txtDataPagamento.Text))
            {
                lblMsgAddPagamento.Text = "Insira uma data";
                divMsgAddPagamento.Attributes["class"] = "alert alert-danger";
            }
            else if (ddlServico.SelectedItem.Text == "Selecione")
            {
                lblMsgAddPagamento.Text = "Selecione um Serviço para o pagamento";
                divMsgAddPagamento.Attributes["class"] = "alert alert-danger";
            }
            else if (ddlServico.SelectedItem.Text == "Outros" && string.IsNullOrWhiteSpace(txtDescricaoServico.Text))
            {
                lblMsgAddPagamento.Text = "Insira uma descrição para o pagamento";
                divMsgAddPagamento.Attributes["class"] = "alert alert-danger";
            }
            else if (ddlServico.SelectedItem.Text == "Processo" && ddlProcesso.SelectedItem.Text == "Selecione")
            {
                lblMsgAddPagamento.Text = "informe de qual processo é o pagamento";
                divMsgAddPagamento.Attributes["class"] = "alert alert-danger";
            }
            else
            {
                Pagamento pag = new Pagamento();
                PagamentoDB pagDB = new PagamentoDB();
                Servico sev = new Servico();
                ServicoDB sevDB = new ServicoDB();
                Processo pro = new Processo();
                Pessoa pes = new Pessoa();
                Advogado adv = new Advogado();

                pag.Valor = Convert.ToDecimal(txtValor.Text);
                pag.DataPagamento = Convert.ToDateTime(txtDataPagamento.Text);
                sev.Codigo = Convert.ToInt32(ddlServico.SelectedItem.Value);
                pag.Servico = sev;

                adv.Codigo = Convert.ToInt32(Session["Advogado"]);
                pag.Advogado = adv;

                pes.Codigo = Convert.ToInt32(Session["PagamentoCliente"]);
                pag.Pes_cliente = pes;

                if (ddlProcesso.SelectedItem.Text != "Selecione")
                {
                    pro.Codigo = Convert.ToInt32(ddlProcesso.SelectedItem.Value);
                    pag.Processo = pro;
                }
                if (ddlServico.SelectedItem.Text == "Outros")
                {
                    sev.Descricao = txtDescricaoServico.Text;
                    sevDB.Insert(sev);
                    sev = sevDB.GetLastId(txtDescricaoServico.Text);
                    pag.Servico = sev;
                }

                if (!pagDB.Insert(pag))
                {
                    //mensagem de falha
                }
                else
                {
                    txtValor.Text = string.Empty;
                    lblMsgAddPagamento.Text = "Pagamento inserido com secesso";
                    divMsgAddPagamento.Attributes["class"] = "alert alert-success";
                    txtDataPagamento.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    Function.LimpaDDL(ddlProcesso);
                    Function.LimpaDDL(ddlServico);
                    txtDescricaoServico.Enabled = false;
                    ddlProcesso.Enabled = false;
                }
            }
        }
        catch (FormatException)
        {
            lblMsgAddPagamento.Text = "Data Invalida";
            divMsgAddPagamento.Attributes["class"] = "alert alert-danger";
        }
        return true;
    }
}