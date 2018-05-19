using Habius.Classes.Administrativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//usado para chamar Cultureinfo, classe de culturas regionais
using System.Globalization;
//usado para chamar Thread que permite utilizar TextInfo.ToTitleCase()
using System.Threading;
using Habius.Common;
using Habius.Persistencia.Administrativo;
using System.Data;
//oi
public partial class Pages_Administrativo_CadastraAdvogado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EstadoCivilDB estCivilDB = new EstadoCivilDB();
        DataSet ds = estCivilDB.SelecAllEstadoCivil();
     
        lblMensagem.Text = string.Empty;
        if (!Page.IsPostBack)
        {
            Page.Title = "Cadastrar Advogado";
            CarregaEstado();
            CarregaEstadoCivil();
        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        if (ValidaADV(lblMensagem))
        {
            if (CadastraADV())
            {
                LimpaCampo();
                txtNome.Focus();
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
    private bool ValidaADV(Label lbl)
    {
        AdvogadoDB advDB = new AdvogadoDB();
        ContatoDB conDB = new ContatoDB();
        if (string.IsNullOrWhiteSpace(txtNome.Text))
        {
            lbl.Text = "Insira o nome";
        }
        else if (string.IsNullOrWhiteSpace(txtCelular.Text) && string.IsNullOrWhiteSpace(txtTelefone.Text))
        {
            lbl.Text = "Insira ao menos um telefone";
        }
        else if (string.IsNullOrWhiteSpace(txtCPF.Text) || ValidaDoc.ValidaCPF(txtCPF.Text) == false)
        {
            lbl.Text = "Insira um CPF Valido";
        }
        else if (string.IsNullOrWhiteSpace(txtRg.Text) || txtRg.Text.Length < 12)
        {
            lbl.Text = "Insira um RG Valido";
        }
        else if (string.IsNullOrWhiteSpace(txtOab.Text))
        {
            lbl.Text = "Insira um numero da OAB";
        }
        else if (Convert.ToDateTime(txtDataNascimento.Text) >= DateTime.Today)
        {
            lbl.Text = "Insira uma data de nascimento valida";
        }
        else if (ddlSexo.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Selecione o Sexo";
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
        else if (string.IsNullOrWhiteSpace(txtEmail.Text))
        {
            lbl.Text = "Insira um Email";
        }
        else if (string.IsNullOrWhiteSpace(txtLogin.Text))
        {
            lbl.Text = "Insira um Nome de usuario";
        }
        else if (string.IsNullOrWhiteSpace(txtSenha.Text) || string.IsNullOrWhiteSpace(txtConfirmaSenha.Text))
        {
            lbl.Text = "Insira sua senha";
        }
        else if (advDB.ValidaCPF(txtCPF.Text) != null)
        {
            lbl.Text = "CPF já cadastrado!";
        }
        else if (advDB.ValidaRG(txtRg.Text) != null)
        {
            lbl.Text = "RG já cadastrado!";
        }
        else if (advDB.ValidaOAB(txtOab.Text) != null)
        {
            lbl.Text = "OAB já cadastrado!";
        }
        else if (conDB.ValidaEmail(txtEmail.Text) != null)
        {
            lbl.Text = "Email já cadastrado!";
        }
        else if (advDB.ValidaLogin(txtLogin.Text) != null)
        {
            lbl.Text = "Login já cadastrado!";
        }
        else
        {
            return true;
        }
        divmensagem.Attributes["class"] = "alert alert-danger";
        return false;
    }
    private bool CadastraADV()
    {
        // Objetos
        Advogado adv = new Advogado();
        Contato con = new Contato();
        Endereco end = new Endereco();
        Cidade cid = new Cidade();
        Estado est = new Estado();
        ContatoDB conDB = new ContatoDB();
        EnderecoDB endDB = new EnderecoDB();
        AdvogadoDB advDB = new AdvogadoDB();
        EstadoCivil estCivil = new EstadoCivil();

        //Objeto da classe culture info, permite utulizar "Culturas de uma certo pais" ex: Formato como se escreve a data dd/mm/yyyy ou yyy/mm/dd
        CultureInfo PrimeiraLetra = Thread.CurrentThread.CurrentCulture;

        //contato
        con.Nome = PrimeiraLetra.TextInfo.ToTitleCase(txtNome.Text);
        con.Nome = con.Nome.Replace("De ", "de ").Replace("Da ", "da ").Replace("Das ", "das ").Replace("Dos ", "dos ");
        con.Telefone = txtTelefone.Text;
        con.Celular = txtCelular.Text;
        con.Email = txtEmail.Text;

        //advogado

        adv.Cpf = txtCPF.Text;
        adv.Rg = txtRg.Text;
        adv.OAB = txtOab.Text;
        adv.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text);
        adv.Sexo = ddlSexo.SelectedItem.Value;
        adv.Senha = Criptografia.GetSHA256(txtSenha.Text);
        adv.UserName = txtLogin.Text;
        estCivil.Codigo = Convert.ToInt32(ddlEstadoCivil.SelectedItem.Value);

        adv.EstadoCivil = estCivil;

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

        //Persistencia

        if (!conDB.InsertSemFK(con))
        {
            lblMensagem.Text = "Erro ao inserir dados, favor contatar o fabricante";
            divmensagem.Attributes["class"] = "alert alert-danger";
            return false;
        }
        else if (!endDB.Insert(end))
        {
            lblMensagem.Text = "Erro ao inserir dados, favor contatar o fabricante";
            divmensagem.Attributes["class"] = "alert alert-danger";
            return false;
        }
        else
        {
            end.Codigo = endDB.GetLastId(end);
            con.Codigo = conDB.GetLastId(con);

            adv.Endereco = end;
            adv.ContatoPessoa = con;

            if (!advDB.Insert(adv))
            {
                lblMensagem.Text = "Erro ao inserir dados, favor contatar o fabricante";
                divmensagem.Attributes["class"] = "alert alert-danger";
                return false;
            }
            else
            {
                adv = advDB.GetLastId(con.Codigo);
                con.PessoaAdvogado = adv;

                if (!conDB.UpdateCodigoAdvogado(con))
                {
                    lblMensagem.Text = "Erro ao inserir dados, favor contatar o fabricante";
                    divmensagem.Attributes["class"] = "alert alert-danger";
                    return false;
                }
                else
                {
                    lblMensagem.Text = "Advogado Cadastrado realizado com sucesso!";
                    divmensagem.Attributes["class"] = "alert alert-success";
                    return true;
                }
            }
        }
    }
    private bool LimpaCampo()
    {
        try
        {
            txtNome.Text = string.Empty;
            txtCPF.Text = string.Empty;
            txtRg.Text = string.Empty;
            txtOab.Text = string.Empty;
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
            for (int i = 0; i < ddlEstadoCivil.Items.Count; i++)
            {
                ddlEstadoCivil.Items[i].Selected = false;
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
}