using Habius.Common;
using Habius.Persistencia.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using Habius.Classes.Administrativo;

public partial class Pages_Administrativo_Advogado_CadastraCliente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCnpj.CssClass = "form-control input-lg";
        txtRg.CssClass = "form-control input-lg";
        txtCPF.CssClass = "form-control input-lg";
        txtDataNascimento.CssClass = "form-control input-lg";
        ddlSexo.CssClass = "form-control input-lg";
        ddlEstadoCivil.CssClass = "form-control input-lg";
        lblMensagem.Text = string.Empty;
        if (!Page.IsPostBack)
        {
            Page.Title = "Cadastrar Cliente";
            CarregaEstado();
            CarregaEstadoCivil();
        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        if (rblTipoCliente.SelectedItem.Value == "1")
        {
            if (ValidaClienteFisico(lblMensagem))
            {
                CadastraClienteFisico();
                LimpaClienteFisico();
            }
        }
        else if (rblTipoCliente.SelectedItem.Value == "2")
        {
            if (ValidaClienteJuridico(lblMensagem))
            {
                CadastraClienteJuridico();
                LimpaClienteJuridico();
            }
        }
    }
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../../Pages/Administrativo/Advogado/HomeAdvogado.aspx");
    }
    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregaCidade(Convert.ToInt32(ddlEstado.SelectedItem.Value));
    }
    protected void rblTipoCliente_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblTipoCliente.SelectedItem.Value == "2")
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
            AsteriscoCnpj.Text = "*";
            AsteriscoCPF.Text = "";
            AsteriscoData.Text = "";
            AsteriscoEstadoCivil.Text = "";
            AsteriscoRG.Text = "";
            AsteriscoSexo.Text = "";
            
        }
        else
        {
            txtCnpj.Text = string.Empty;
            txtCnpj.Enabled = false;
            txtRg.Enabled = true;
            txtCPF.Enabled = true;
            ddlSexo.Enabled = true;
            ddlEstadoCivil.Enabled = true;
            txtDataNascimento.Enabled = true;
            AsteriscoCnpj.Text = "";
            AsteriscoCPF.Text = "*";
            AsteriscoData.Text = "*";
            AsteriscoEstadoCivil.Text = "*";
            AsteriscoRG.Text = "*";
            AsteriscoSexo.Text = "*";

        }
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
    private void CarregaEstado()
    {
        EstadoDB estDB = new EstadoDB();
        DataSet dsEstado = estDB.SelectAll();
        Function.CarregaDDL(dsEstado, ddlEstado, "EST_CODIGO", "EST_ESTADO");
    }
    private bool ValidaClienteFisico(Label lbl)
    {
        ClienteFisicoDB cliFisicoDB = new ClienteFisicoDB();
        ContatoDB conDB = new ContatoDB();
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
        else if (string.IsNullOrWhiteSpace(txtCPF.Text) || ValidaDoc.ValidaCPF(txtCPF.Text) == false)
        {
            lbl.Text = "Insira um CPF Valido";
        }
        else if (string.IsNullOrWhiteSpace(txtRg.Text) || txtRg.Text.Length < 12)
        {
            lbl.Text = "Insira um RG Valido";
        }
        else if (string.IsNullOrWhiteSpace(txtLogin.Text))
        {
            lbl.Text = "Insira um Nome de usuario";
        }
        else if (string.IsNullOrWhiteSpace(txtSenha.Text) || string.IsNullOrWhiteSpace(txtConfirmaSenha.Text))
        {
            lbl.Text = "Insira sua senha";
        }
        else if (cliFisicoDB.ValidaLogin(txtLogin.Text) != null)
        {
            lbl.Text = "Login já cadastrado!";
        }
        else if (cliFisicoDB.ValidaRG(txtRg.Text) != null)
        {
            lbl.Text = "RG já cadastrado!";
        }
        else if (cliFisicoDB.ValidaCPF(txtCPF.Text) != null)
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
                if (conDB.ValidaEmail(txtEmail.Text) != null)
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
        ContatoDB conDB = new ContatoDB();
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
        else if (string.IsNullOrWhiteSpace(txtSenha.Text) || string.IsNullOrWhiteSpace(txtConfirmaSenha.Text))
        {
            lbl.Text = "Insira sua senha";
        }
        else if (cliJuridicoDB.ValidaLogin(txtLogin.Text) != null)
        {
            lbl.Text = "Login já cadastrado!";
        }
        else if (cliJuridicoDB.ValidaCNPJ(txtCnpj.Text) != null)
        {
            lbl.Text = "CNPJ já cadastrado!";
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (conDB.ValidaEmail(txtEmail.Text) != null)
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
    private bool LimpaClienteFisico()
    {
        try
        {
            txtNome.Text = string.Empty;
            txtCPF.Text = string.Empty;
            txtRg.Text = string.Empty;
            txtCnpj.Text = string.Empty;
            txtDataNascimento.Text = string.Empty;
            for (int i = 0; i < ddlSexo.Items.Count; i++)
            {
                ddlSexo.Items[i].Selected = false;
            }
            for (int i = 0; i < ddlCidade.Items.Count; i++)
            {
                ddlCidade.Items[i].Selected = false;
            }
            for (int i = 0; i < ddlEstado.Items.Count; i++)
            {
                ddlEstado.Items[i].Selected = false;
            }
            txtCelular.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCEP.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtComplemento.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtLogin.Text = string.Empty;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    private bool LimpaClienteJuridico()
    {
        try
        {
            txtNome.Text = string.Empty;
            txtCPF.Text = string.Empty;
            txtRg.Text = string.Empty;
            txtCnpj.Text = string.Empty;
            txtDataNascimento.Text = string.Empty;

            txtCelular.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            for (int i = 0; i < ddlSexo.Items.Count; i++)
            {
                ddlSexo.Items[i].Selected = false;
            }
            for (int i = 0; i < ddlCidade.Items.Count; i++)
            {
                ddlCidade.Items[i].Selected = false;
            }
            for (int i = 0; i < ddlEstado.Items.Count; i++)
            {
                ddlEstado.Items[i].Selected = false;
            }
            txtBairro.Text = string.Empty;
            txtCEP.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtComplemento.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtLogin.Text = string.Empty;
            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }
    private bool CadastraClienteFisico()
    {
        ClienteFisico cliFisico = new ClienteFisico();
        Advogado adv = new Advogado();
        Contato con = new Contato();
        Endereco end = new Endereco();
        EstadoCivil estCivil = new EstadoCivil();
        Cidade cid = new Cidade();
        Estado est = new Estado();
        ContatoDB conDB = new ContatoDB();
        EnderecoDB endDB = new EnderecoDB();
        ClienteFisicoDB cliFisicoDB = new ClienteFisicoDB();

        //Objeto da classe culture info, permite utulizar "Culturas de uma certo pais" ex: Formato como se escreve a data dd/mm/yyyy ou yyy/mm/dd
        CultureInfo PrimeiraLetra = Thread.CurrentThread.CurrentCulture;

        con.Nome = PrimeiraLetra.TextInfo.ToTitleCase(txtNome.Text);
        con.Nome = con.Nome.Replace("De ", "de ").Replace("Da ", "da ").Replace("Das ", "das ").Replace("Dos ", "dos ");
        con.Telefone = txtTelefone.Text;
        con.Celular = txtCelular.Text;
        con.Email = txtEmail.Text;

        //Cliente
        cliFisico.Cpf = txtCPF.Text;
        cliFisico.Rg = txtRg.Text;
        cliFisico.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text);
        cliFisico.Sexo = ddlSexo.SelectedItem.Value;
        cliFisico.Senha = Criptografia.GetSHA256(txtSenha.Text);
        cliFisico.UserName = txtLogin.Text;
        estCivil.Codigo = Convert.ToInt32(ddlEstadoCivil.SelectedItem.Value);

        cliFisico.EstadoCivil = estCivil;

        //endereco
        cid.Codigo = Convert.ToInt32(ddlCidade.SelectedItem.Value);
        end.Cidade = cid;
        end.Bairro = PrimeiraLetra.TextInfo.ToTitleCase(txtBairro.Text);
        end.Bairro = end.Bairro.Replace("De ", "de ").Replace("Da ", "da ").Replace("Das ", "das ").Replace("Dos ", "dos ");
        end.Cep = txtCEP.Text;
        end.Logradouro = PrimeiraLetra.TextInfo.ToTitleCase(txtEndereco.Text);
        end.Logradouro = end.Logradouro.Replace("De ", "de ").Replace("Da ", "da ").Replace("Das ", "das ").Replace("Dos ", "dos ");
        end.Numero = txtNumero.Text;
        end.Complemento = txtComplemento.Text;

        adv.Codigo = Convert.ToInt32(Session["Advogado"]);
        con.PessoaAdvogado = adv;

        //Persistencia
        if (!conDB.InsertComFK(con))
        {
            lblMensagem.Text = "Erro ao inserir dados, favor contatar o fabricante";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else if (!endDB.Insert(end))
        {
            lblMensagem.Text = "Erro ao inserir dados, favor contatar o fabricante";
            divMensagem.Attributes["class"] = "alert alert-danger";
        }
        else
        {
            end.Codigo = endDB.GetLastId(end);
            con.Codigo = conDB.GetLastId(con);

            cliFisico.Endereco = end;
            cliFisico.ContatoPessoa = con;

            if (!cliFisicoDB.Insert(cliFisico))
            {
                lblMensagem.Text = "Erro ao inserir dados, favor contatar o fabricante";
                divMensagem.Attributes["class"] = "alert alert-danger";
            }
            else
            {
                lblMensagem.Text = "Cliente Cadastrado com sucesso!";
                divMensagem.Attributes["class"] = "alert alert-success";
                return true;
            }
        }
        return false;
    }
    private bool CadastraClienteJuridico()
    {
        ClienteJuridico cliJuridico = new ClienteJuridico();
        Advogado adv = new Advogado();
        Contato con = new Contato();
        Endereco end = new Endereco();
        Cidade cid = new Cidade();
        Estado est = new Estado();
        ContatoDB conDB = new ContatoDB();
        EnderecoDB endDB = new EnderecoDB();
        ClienteJuridicoDB cliJuridicoDB = new ClienteJuridicoDB();

        CultureInfo PrimeiraLetra = Thread.CurrentThread.CurrentCulture;

        con.Nome = PrimeiraLetra.TextInfo.ToTitleCase(txtNome.Text);
        con.Nome = con.Nome.Replace("De ", "de ").Replace("Da ", "da ").Replace("Das ", "das ").Replace("Dos ", "dos ");
        con.Telefone = txtTelefone.Text;
        con.Celular = txtCelular.Text;
        con.Email = txtEmail.Text;

        //Cliente
        cliJuridico.Cnpj = txtCnpj.Text;
        cliJuridico.Senha = Criptografia.GetSHA256(txtSenha.Text);
        cliJuridico.UserName = txtLogin.Text;

        //endereco
        cid.Codigo = Convert.ToInt32(ddlCidade.SelectedItem.Value);
        end.Cidade = cid;
        end.Bairro = PrimeiraLetra.TextInfo.ToTitleCase(txtBairro.Text);
        end.Bairro = end.Bairro.Replace("De ", "de ").Replace("Da ", "da ").Replace("Das ", "das ").Replace("Dos ", "dos ");
        end.Cep = txtCEP.Text;
        end.Logradouro = PrimeiraLetra.TextInfo.ToTitleCase(txtEndereco.Text);
        end.Logradouro = end.Logradouro.Replace("De ", "de ").Replace("Da ", "da ").Replace("Das ", "das ").Replace("Dos ", "dos ");
        end.Numero = txtNumero.Text;
        end.Complemento = txtComplemento.Text;

        adv.Codigo = Convert.ToInt32(Session["Advogado"]);
        con.PessoaAdvogado = adv;

        //Persistencia
        if (!conDB.InsertComFK(con))
        {
            lblMensagem.Text = "Erro ao inserir dados, favor contatar o fabricante";
            divMensagem.Attributes["class"] = "alert alert-danger";
            return false;
        }
        else if (!endDB.Insert(end))
        {
            lblMensagem.Text = "Erro ao inserir dados, favor contatar o fabricante";
            divMensagem.Attributes["class"] = "alert alert-danger";
            return false;
        }
        else
        {
            end.Codigo = endDB.GetLastId(end);
            con.Codigo = conDB.GetLastId(con);

            cliJuridico.Endereco = end;
            cliJuridico.ContatoPessoa = con;

            if (!cliJuridicoDB.Insert(cliJuridico))
            {
                lblMensagem.Text = "Erro ao inserir dados, favor contatar o fabricante";
                divMensagem.Attributes["class"] = "alert alert-danger";
            }
            else
            {
                lblMensagem.Text = "Cliente Cadastrado com sucesso!";
                divMensagem.Attributes["class"] = "alert alert-success";
                return true;
            }
        }
        return false;
    }
}