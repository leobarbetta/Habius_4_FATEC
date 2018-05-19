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

public partial class Pages_Administrativo_Cliente_EditarMeusDados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Cliente"] != null)
        {
            txtCnpj.CssClass = "form-control input-lg";
            txtRg.CssClass = "form-control input-lg";
            txtCPF.CssClass = "form-control input-lg";
            txtDataNascimento.CssClass = "form-control input-lg";
            ddlSexo.CssClass = "form-control input-lg";
            ddlEstadoCivil.CssClass = "form-control input-lg";
            lblMensagem.Text = string.Empty;
            Pessoa pes = (Pessoa)Session["Cliente"];
            if (!Page.IsPostBack)
            {
                CarregaEstado();
                CarregaEstadoCivil();

                if (pes.Nivel == 3)
                {
                    txtCnpj.Text = string.Empty;
                    txtCnpj.Enabled = false;
                    txtRg.Enabled = true;
                    txtCPF.Enabled = true;
                    ddlSexo.Enabled = true;
                    ddlEstadoCivil.Enabled = true;
                    txtDataNascimento.Enabled = true;
                    CarregaClienteFisico(pes.Codigo);
                }
                else if (pes.Nivel == 4)
                {
                    for (int i = 0; i < ddlEstadoCivil.Items.Count; i++)
                    {
                        ddlEstadoCivil.Items[i].Selected = false;
                    }
                    for (int i = 0; i < ddlSexo.Items.Count; i++)
                    {
                        ddlSexo.Items[i].Selected = false;
                    }
                    txtCPF.Text = string.Empty;
                    txtRg.Text = string.Empty;
                    txtDataNascimento.Text = string.Empty;
                    txtDataNascimento.Enabled = false;
                    txtCPF.Enabled = false;
                    txtRg.Enabled = false;
                    ddlSexo.Enabled = false;
                    txtDataNascimento.Enabled = false;
                    ddlEstadoCivil.Enabled = false;
                    txtCnpj.Enabled = true;
                    CarregaClienteJuridico(pes.Codigo);
                }
                else
                {
                    Response.Redirect("/HomeCliente.aspx");
                }
            }
        }
    }
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        Pessoa pes = (Pessoa)Session["Cliente"];
        if (pes.Nivel == 3)
        {
            if (ValidaClienteFisico(lblMensagem))
            {
                UpdateClienteFisico();
            }
        }
        else if (pes.Nivel == 4)
        {
            if (ValidaClienteJuridico(lblMensagem))
            {
                UpdateClienteJuridico();
            }
        }
    }
    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregaCidade(Convert.ToInt32(ddlEstado.SelectedItem.Value));
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
        DataSet dsEstadoCivil = estCivilDB.SelecAllEstadoCivil();
        Function.CarregaDDL(dsEstadoCivil, ddlEstadoCivil, "ECI_CODIGO", "ECI_DESCRICAO");
    }
    private void CarregaCidade(int id)
    {
        CidadeDB cidDB = new CidadeDB();
        DataSet dsCidade = cidDB.SelectAll(id);
        Function.CarregaDDL(dsCidade, ddlCidade, "CID_CODIGO", "CID_CIDADE");
    }
    private void CarregaClienteFisico(int id)
    {
        EnderecoDB endDB = new EnderecoDB();
        Endereco endereco = new Endereco();
        ClienteFisicoDB clifisicoDB = new ClienteFisicoDB();
        ClienteFisico clifisico = new ClienteFisico();
        ContatoDB conDB = new ContatoDB();
        Contato contato = new Contato();
        CidadeDB cidDB = new CidadeDB();
        Cidade cid = new Cidade();
        EstadoDB estDB = new EstadoDB();
        Estado est = new Estado();

        clifisico = clifisicoDB.Select(id);

        txtRg.Text = clifisico.Rg;
        txtCPF.Text = clifisico.Cpf;
        txtDataNascimento.Text = clifisico.DataNascimento.ToString("dd/MM/yyyy");
        txtLogin.Text = clifisico.UserName;
        Function.CarregaItemDDLByCodigo(ddlEstadoCivil, clifisico.EstadoCivil.Codigo);
        Function.CarregaItemDDLByTexto(ddlSexo, clifisico.Sexo);

        contato = conDB.SelectContato(clifisico.ContatoPessoa.Codigo);
        txtNome.Text = contato.Nome;
        txtTelefone.Text = contato.Telefone;
        txtCelular.Text = contato.Celular;
        txtEmail.Text = contato.Email;

        endereco = endDB.Select(clifisico.Endereco.Codigo);
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
    private void CarregaClienteJuridico(int id)
    {
        EnderecoDB endDB = new EnderecoDB();
        Endereco endereco = new Endereco();
        ClienteJuridicoDB clijuridicoDB = new ClienteJuridicoDB();
        ClienteJuridico clijuridico = new ClienteJuridico();
        ContatoDB conDB = new ContatoDB();
        Contato contato = new Contato();
        CidadeDB cidDB = new CidadeDB();
        Cidade cid = new Cidade();
        EstadoDB estDB = new EstadoDB();
        Estado est = new Estado();

        clijuridico = clijuridicoDB.Select(id);

        txtCnpj.Text = clijuridico.Cnpj;
        txtLogin.Text = clijuridico.UserName;

        contato = conDB.SelectContato(clijuridico.ContatoPessoa.Codigo);
        txtNome.Text = contato.Nome;
        txtTelefone.Text = contato.Telefone;
        txtCelular.Text = contato.Celular;
        txtEmail.Text = contato.Email;


        endereco = endDB.Select(clijuridico.Endereco.Codigo);
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
    private bool UpdateClienteFisico()
    {
        EnderecoDB endDB = new EnderecoDB();
        Endereco endereco = new Endereco();
        ClienteFisicoDB clifisicoDB = new ClienteFisicoDB();
        ClienteFisico clifisico = new ClienteFisico();
        ContatoDB conDB = new ContatoDB();
        Contato contato = new Contato();
        CidadeDB cidDB = new CidadeDB();
        Cidade cid = new Cidade();
        EstadoDB estDB = new EstadoDB();
        Estado est = new Estado();
        Pessoa pes = (Pessoa)Session["Cliente"];
        clifisico = clifisicoDB.Select(pes.Codigo);
        clifisico.Cpf = txtCPF.Text;
        clifisico.Rg = txtRg.Text;
        clifisico.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text);
        clifisico.UserName = txtLogin.Text;
        clifisico.Sexo = ddlSexo.SelectedItem.Value;

        contato = conDB.SelectContato(clifisico.ContatoPessoa.Codigo);
        contato.Nome = txtNome.Text;
        contato.Telefone = txtTelefone.Text;
        contato.Celular = txtCelular.Text;
        contato.Email = txtEmail.Text;

        endereco = endDB.Select(clifisico.Endereco.Codigo);
        endereco.Logradouro = txtEndereco.Text;
        endereco.Bairro = txtBairro.Text;
        endereco.Complemento = txtComplemento.Text;
        endereco.Numero = txtNumero.Text;
        endereco.Cep = txtCEP.Text;

        endereco.Cidade.Codigo = Convert.ToInt32(ddlCidade.SelectedItem.Value);

        endDB.Update(endereco);
        conDB.Update(contato);

        if (clifisicoDB.Update(clifisico))
        {
            lblMensagem.Text = "Dados atualizados";
            divMensagem.Attributes["class"] = "alert alert-success";
            return true;
        }
        else
        {
            lblMensagem.Text = "Cliente não foi atualizado";
            return false;
        }
    }
    private bool UpdateClienteJuridico()
    {
        ClienteJuridicoDB clijuridicoDB = new ClienteJuridicoDB();
        ClienteJuridico clijuridico = new ClienteJuridico();
        Pessoa pes = (Pessoa)Session["Cliente"];
        clijuridico = clijuridicoDB.Select(pes.Codigo);
        clijuridico.Cnpj = txtCnpj.Text;
        clijuridico.UserName = txtLogin.Text;

        ContatoDB conDB = new ContatoDB();
        Contato contato = new Contato();
        contato = conDB.SelectContato(clijuridico.ContatoPessoa.Codigo);
        contato.Nome = txtNome.Text;
        contato.Telefone = txtTelefone.Text;
        contato.Celular = txtCelular.Text;
        contato.Email = txtEmail.Text;

        EnderecoDB endDB = new EnderecoDB();
        Endereco endereco = new Endereco();
        endereco = endDB.Select(clijuridico.Endereco.Codigo);
        endereco.Logradouro = txtEndereco.Text;
        endereco.Bairro = txtBairro.Text;
        endereco.Complemento = txtComplemento.Text;
        endereco.Numero = txtNumero.Text;
        endereco.Cep = txtCEP.Text;

        CidadeDB cidDB = new CidadeDB();
        Cidade cidade = new Cidade();
        endereco.Cidade.Codigo = Convert.ToInt32(ddlCidade.SelectedValue);

        endDB.Update(endereco);
        conDB.Update(contato);

        if (clijuridicoDB.Update(clijuridico))
        {
            lblMensagem.Text = "Dados atualizados";
            divMensagem.Attributes["class"] = "alert alert-success";
            return true;
        }
        else
        {
            lblMensagem.Text = "Cliente não foi atualizado";
            return false;
        }
    }
    private bool ValidaClienteFisico(Label lbl)
    {
        ClienteFisicoDB cliFisicoDB = new ClienteFisicoDB();
        ClienteFisico cliFisico = new ClienteFisico();
        ContatoDB conDB = new ContatoDB();
        Contato con = new Contato();
        Pessoa pes = (Pessoa)Session["Cliente"];
        cliFisico = cliFisicoDB.Select(pes.Codigo);
        con = conDB.SelectContato(cliFisico.ContatoPessoa.Codigo);
        string email = con.Email;
        string cpf = cliFisico.Cpf;
        string rg = cliFisico.Rg;
        string login = cliFisico.UserName;

        if (string.IsNullOrWhiteSpace(txtNome.Text))
        {
            lbl.Text = "Insira um Nome";
        }
        else if (string.IsNullOrWhiteSpace(txtCelular.Text) && string.IsNullOrWhiteSpace(txtTelefone.Text))
        {
            lbl.Text = "Insira ao menos um telefone";
        }
        else if (ddlEstado.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Selecione um estado";
        }
        else if (ddlCidade.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Selecione uma cidade";
        }
        else if (ddlEstadoCivil.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Selecione um estado civil";
        }
        else if (ValidaDoc.ValidaCPF(txtCPF.Text) == false)
        {
            lbl.Text = "Insira um CPF Valido";
        }
        else if (string.IsNullOrWhiteSpace(txtRg.Text))
        {
            lbl.Text = "Insira um RG Valido";
        }
        else if (string.IsNullOrWhiteSpace(txtLogin.Text))
        {
            lbl.Text = "Insira um Nome de usuario";
        }
        else if (cliFisicoDB.ValidaLogin(txtLogin.Text) != null && login != txtLogin.Text)
        {
            lbl.Text = "Login já cadastrado!";
        }
        else if (cliFisicoDB.ValidaRG(txtRg.Text) != null && rg != txtRg.Text)
        {
            lbl.Text = "RG já cadastrado!";
        }
        else if (cliFisicoDB.ValidaCPF(txtCPF.Text) != null && cpf != txtCPF.Text)
        {
            lbl.Text = "CPF já cadastrado!";
        }
        else if (string.IsNullOrWhiteSpace(txtDataNascimento.Text))
        {
            lbl.Text = "Informe uma data de nascimento";
        }
        else if (Convert.ToDateTime(txtDataNascimento.Text) >= DateTime.Today)
        {
            lbl.Text = "Insira uma data de nascimento valida";
        }
        else if (ddlSexo.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Selecione o Sexo";
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (conDB.ValidaEmail(txtEmail.Text) != null && email != txtEmail.Text)
                {
                    lbl.Text = "Email já cadastrado!";
                    divMensagem.Attributes["class"] = "alert alert-danger";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        divMensagem.Attributes["class"] = "alert alert-danger";
        return false;
    }
    private bool ValidaClienteJuridico(Label lbl)
    {
        ClienteJuridicoDB cliJuridicoDB = new ClienteJuridicoDB();
        ClienteJuridico cliJuridico = new ClienteJuridico();
        ContatoDB conDB = new ContatoDB();
        Contato con = new Contato();
        Pessoa pes = (Pessoa)Session["Cliente"];
        cliJuridico = cliJuridicoDB.Select(pes.Codigo);
        con = conDB.SelectContato(cliJuridico.ContatoPessoa.Codigo);
        string email = con.Email;
        string cnpj = cliJuridico.Cnpj;
        string login = cliJuridico.UserName;
        if (string.IsNullOrWhiteSpace(txtNome.Text))
        {
            lbl.Text = "Insira um Nome";
        } if (string.IsNullOrWhiteSpace(txtCnpj.Text))
        {
            lbl.Text = "Insira um CNPJ";
        }
        else if (string.IsNullOrWhiteSpace(txtCelular.Text) && string.IsNullOrWhiteSpace(txtTelefone.Text))
        {
            lbl.Text = "Insira ao menos um telefone";
        }
        else if (ddlEstado.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Selecione um estado";
        }
        else if (ddlCidade.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Selecione uma cidade";
        }
        else if (string.IsNullOrWhiteSpace(txtLogin.Text))
        {
            lbl.Text = "Insira um Nome de usuario";
        }
        else if (cliJuridicoDB.ValidaLogin(txtLogin.Text) != null && login != txtLogin.Text)
        {
            lbl.Text = "Login já cadastrado!";
        }
        else if (cliJuridicoDB.ValidaCNPJ(txtCnpj.Text) != null && cnpj != txtCnpj.Text)
        {
            lbl.Text = "CNPJ já cadastrado!";
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (conDB.ValidaEmail(txtEmail.Text) != null && email != txtEmail.Text)
                {
                    lbl.Text = "Email já cadastrado!";
                    divMensagem.Attributes["class"] = "alert alert-danger";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        divMensagem.Attributes["class"] = "alert alert-danger";
        return false;
    }
}