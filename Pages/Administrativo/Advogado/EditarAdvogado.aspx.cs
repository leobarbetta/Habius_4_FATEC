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

public partial class Pages_Administrativo_Advogado_EditarAdvogado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Page.Title = "Meus Dados";
            //CarregaEstado();
            //CarregaEstadoCivil();
            //CarregaAdvogado();
            //txtNome.Focus();
        }
    }
    private void CarregaEstado()
    {
        EstadoDB estDB = new EstadoDB();
        DataSet dsEstado = estDB.SelectAll();
        Function.CarregaDDL(dsEstado, ddlEstado, "EST_CODIGO", "EST_ESTADO");
    }
    private void CarregaEstadoCivil()
    {
        EstadoCivilDB estCivilDB = new EstadoCivilDB();
        DataSet ds = estCivilDB.SelecAllEstadoCivil();
        Function.CarregaDDL(ds, ddlEstadoCivil, "ECI_CODIGO", "ECI_DESCRICAO");
    }
    private void CarregaCidade(int id)
    {
        CidadeDB cidDB = new CidadeDB();
        DataSet dsCidade = cidDB.SelectAll(id);
        Function.CarregaDDL(dsCidade, ddlCidade, "CID_CODIGO", "CID_CIDADE");
    }
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        AdvogadoDB advDB = new AdvogadoDB();
        Advogado advogado = new Advogado();
        ContatoDB conDB = new ContatoDB();
        Contato contato = new Contato();
        EnderecoDB endDB = new EnderecoDB();
        Endereco endereco = new Endereco();
        int id = Convert.ToInt32(Session["Advogado"]);
        string email = contato.Email;
        string cpf = advogado.Cpf;
        string rg = advogado.Rg;
        string login = advogado.UserName;

        advogado = advDB.Select(id);
        contato = conDB.SelectContato(advogado.ContatoPessoa.Codigo);
        endereco = endDB.Select(advogado.Endereco.Codigo);

        if (string.IsNullOrWhiteSpace(txtNome.Text))
        {
            txtNome.Focus();
            lblMensagem.Text = "Insira um Nome";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtCelular.Text) && string.IsNullOrWhiteSpace(txtTelefone.Text))
        {
            txtCelular.Focus();
            lblMensagem.Text = "Insira ao menos um telefone";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (ValidaDoc.ValidaCPF(txtCPF.Text) == false)
        {
            txtCPF.Focus();
            lblMensagem.Text = "Insira um CPF Valido";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtRg.Text) || txtRg.Text.Length < 12)
        {
            txtRg.Focus();
            lblMensagem.Text = "Insira um RG Valido";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtOab.Text))
        {
            txtOab.Focus();
            lblMensagem.Text = "Insira um numero da OAB";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (Convert.ToDateTime(txtDataNascimento.Text) >= DateTime.Today)
        {
            txtDataNascimento.Focus();
            lblMensagem.Text = "Insira uma data de nascimento valida";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (ddlSexo.SelectedItem.Text == "Selecione")
        {
            ddlSexo.Focus();
            lblMensagem.Text = "Selecione o Sexo";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (ddlEstado.SelectedItem.Text == "Selecione")
        {
            ddlEstado.Focus();
            lblMensagem.Text = "Selecione um estado";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (ddlCidade.SelectedItem.Text == "Selecione")
        {
            ddlCidade.Focus();
            lblMensagem.Text = "Selecione uma cidade";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtEmail.Text))
        {
            txtEmail.Focus();
            lblMensagem.Text = "Insira um Email";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtLogin.Text))
        {
            txtLogin.Focus();
            lblMensagem.Text = "Insira um Nome de usuario";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (advDB.ValidaLogin(txtLogin.Text) != null && login != txtLogin.Text)
        {
            txtLogin.Focus();
            lblMensagem.Text = "Insira um Email";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (advDB.ValidaRG(txtRg.Text) != null && rg != txtRg.Text)
        {
            txtRg.Focus();
            lblMensagem.Text = "Insira um Email";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (advDB.ValidaCPF(txtCPF.Text) != null && cpf != txtCPF.Text)
        {
            txtCPF.Focus();
            lblMensagem.Text = "Insira um Email";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (conDB.ValidaEmail(txtEmail.Text) != null && email != txtEmail.Text)
        {
            txtEmail.Focus();
            lblMensagem.Text = "Insira um Email";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else
        {

            advogado.Cpf = txtCPF.Text;
            advogado.Rg = txtRg.Text;
            advogado.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text);
            advogado.OAB = txtOab.Text;
            advogado.UserName = txtLogin.Text;
            advogado.Sexo = ddlSexo.SelectedItem.Value;

            EstadoCivil estCivil = new EstadoCivil();
            estCivil.Codigo = Convert.ToInt32(ddlEstadoCivil.SelectedItem.Value);
            advogado.EstadoCivil = estCivil;



            contato.Nome = txtNome.Text;
            contato.Telefone = txtTelefone.Text;
            contato.Celular = txtCelular.Text;
            contato.Email = txtEmail.Text;



            endereco.Logradouro = txtEndereco.Text;
            endereco.Bairro = txtBairro.Text;
            endereco.Complemento = txtComplemento.Text;
            endereco.Numero = txtNumero.Text;
            endereco.Cep = txtCEP.Text;

            CidadeDB cidDB = new CidadeDB();
            Cidade cidade = new Cidade();
            cidade.Codigo = Convert.ToInt32(ddlCidade.SelectedValue);
            endereco.Cidade = cidade;

            endDB.Update(endereco);
            conDB.Update(contato);

            if (advDB.Update(advogado))
            {
                lblMensagem.Text = "Dados atualizados";
                divMensagem.Attributes["class"] = "alert alert-success";
            }
            else
            {
                lblMensagem.Text = "Advogado não foi atualizado";
                divMensagem.Attributes["class"] = "alert alert-danger";
            }
        }
    }
    public void CarregaAdvogado()
    {
        EnderecoDB endDB = new EnderecoDB();
        Endereco endereco = new Endereco();
        AdvogadoDB advDB = new AdvogadoDB();
        Advogado advogado = new Advogado();
        ContatoDB conDB = new ContatoDB();
        Contato contato = new Contato();
        CidadeDB cidDB = new CidadeDB();
        Cidade cid = new Cidade();
        EstadoDB estDB = new EstadoDB();
        Estado est = new Estado();
        EstadoCivil estCivil = new EstadoCivil();
        EstadoCivilDB estCivilDB = new EstadoCivilDB();

        int id = Convert.ToInt32(Session["Advogado"]);

        advogado = advDB.Select(id);

        txtCPF.Text = advogado.Cpf;
        txtRg.Text = advogado.Rg;
        txtDataNascimento.Text = advogado.DataNascimento.ToString("dd/MM/yyyy");
        txtOab.Text = advogado.OAB;
        txtLogin.Text = advogado.UserName;
        Function.CarregaItemDDLByTexto(ddlSexo, advogado.Sexo);
        Function.CarregaItemDDLByCodigo(ddlEstadoCivil, advogado.EstadoCivil.Codigo);

        estCivil = estCivilDB.Select(advogado.EstadoCivil.Codigo);

        contato = conDB.SelectContato(advogado.ContatoPessoa.Codigo);
        txtNome.Text = contato.Nome;
        txtTelefone.Text = contato.Telefone;
        txtCelular.Text = contato.Celular;
        txtEmail.Text = contato.Email;

        endereco = endDB.Select(advogado.Endereco.Codigo);
        txtEndereco.Text = endereco.Logradouro;
        txtBairro.Text = endereco.Bairro;
        txtComplemento.Text = endereco.Complemento;
        txtNumero.Text = endereco.Numero;
        txtCEP.Text = Convert.ToString(endereco.Cep);

        cid = cidDB.SelectCidadePessoa(endereco.Cidade.Codigo);
        est = estDB.SelectEstado(cid.Estado.Codigo);

        Function.CarregaItemDDLByCodigo(ddlEstado, est.Codigo);
        CarregaCidade(est.Codigo);
        Function.CarregaItemDDLByCodigo(ddlCidade, cid.Codigo);
    }
    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregaCidade(Convert.ToInt32(ddlEstado.SelectedItem.Value));
    }
}
