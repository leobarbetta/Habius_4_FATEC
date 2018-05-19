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

public partial class Pages_Administrativo_Advogado_teste : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fisico"] != null)
        {
            modalEditarCliF.Show();
        }
        else if (Session["Juridico"] != null)
        {
            modalEditarCliJu.Show();
        }
        Page.Title = "Clientes";
        if (!Page.IsPostBack)
        {
            CarregaEstadoCivil();
            CarregaEstados(ddlEstadoCliF);
            CarregaEstados(ddlEstadoCliJu);
            CarregaCliente();
        }
    }
    protected void gdvClienteFisico_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int idCliente = 0;
        Pessoa pes = new Pessoa();
        PessoaDB pesDB = new PessoaDB();
        switch (e.CommandName)
        {
            case "Editar":
                idCliente = Convert.ToInt32(e.CommandArgument);
                pes = pesDB.SelectGenerico(idCliente);

                if (pes.Nivel == 3)
                {
                    Session["Editar"] = e.CommandArgument;
                    CarregaCliFisicoTextBox(Convert.ToInt32(e.CommandArgument));
                    Session["Fisico"] = e.CommandArgument;
                    modalEditarCliF.Show();
                }
                else if (pes.Nivel == 4)
                {
                    Session["Editar"] = e.CommandArgument;
                    CarregaCliJuridicoTextBox(Convert.ToInt32(e.CommandArgument));
                    Session["Juridico"] = e.CommandArgument;
                    modalEditarCliJu.Show();
                }
                else
                {
                    //retornar msg de erro
                }
                break;
            case "Detalhes":
                idCliente = Convert.ToInt32(e.CommandArgument);
                pes = pesDB.SelectGenerico(idCliente);
                if (pes.Nivel == 3)
                {
                    CarregaDetalhesFisicoLabel(Convert.ToInt32(e.CommandArgument));
                    modalDetalheCliF.Show();
                }
                else if (pes.Nivel == 4)
                {
                    CarregaDetalhesJuridico(Convert.ToInt32(e.CommandArgument));
                    modalDetalhesCliJu.Show();
                }
                else
                {
                    //retornar msg de erro
                }
                break;
            case "Pagamento":
                Session["PagamentoCliente"] = e.CommandArgument;
                Response.Redirect("CadastraPagamentoCliente.aspx");
                break;
        }
    }
    protected void btnBusca_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtBusca.Text))
        {
            CarregaCliente();
        }
        else
        {
            ClienteFisicoDB cliDB = new ClienteFisicoDB();
            DataSet ds = cliDB.BuscaCliente(Convert.ToInt32(Session["Advogado"]), txtBusca.Text);
            Function.CarregaGrid(ds, gdvCliente, lblQtd);
        }
    }
    protected void btnSalvarCliF_Click(object sender, EventArgs e)
    {
        ClienteFisicoDB clifisicoDB = new ClienteFisicoDB();
        ClienteFisico clifisico = new ClienteFisico();
        clifisico = clifisicoDB.Select(Convert.ToInt32(Session["Editar"]));
        string cpf = clifisico.Cpf;
        string rg = clifisico.Rg;
        string login = clifisico.UserName;

        if (clifisicoDB.ValidaCPF(txtCPFCliF.Text) != null && cpf != txtCPFCliF.Text)
        {
            txtCPFCliF.Focus();
            lblMensagem.Text = "CPF já cadastrado";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else if (clifisicoDB.ValidaLogin(txtLoginCliF.Text) != null && login != txtLoginCliF.Text)
        {
            txtLoginCliF.Focus();
            lblMensagem.Text = "Login já existe";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else if (clifisicoDB.ValidaRG(txtRgCliF.Text) != null && rg != txtRgCliF.Text)
        {
            txtRgCliF.Focus();
            lblMensagem.Text = "RG já cadastrado";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtNomeCliF.Text))
        {
            txtNomeCliF.Focus();
            lblMensagem.Text = "Insira um Nome";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtCelularCliF.Text) && string.IsNullOrWhiteSpace(txtTelefoneCliF.Text))
        {
            txtTelefoneCliF.Focus();
            lblMensagem.Text = "Insira ao menos um telefone";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else if (!ValidaDoc.ValidaCPF(txtCPFCliF.Text))
        {
            txtCPFCliF.Focus();
            lblMensagem.Text = "Insira um CPF Valido";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtRgCliF.Text) || txtRgCliF.Text.Length < 12)
        {
            txtRgCliF.Focus();
            lblMensagem.Text = "Insira um RG Valido";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else if (Convert.ToDateTime(txtDataNascimentoCliF.Text) >= DateTime.Today)
        {
            txtDataNascimentoCliF.Focus();
            lblMensagem.Text = "Insira uma data de nascimento valida";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else if (ddlSexoCliF.SelectedItem.Text == "Selecione")
        {
            ddlSexoCliF.Focus();
            lblMensagem.Text = "Selecione o Sexo";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else if (ddlEstadoCliF.SelectedItem.Text == "Selecione")
        {
            ddlEstadoCliF.Focus();
            lblMensagem.Text = "Selecione um estado";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else if (ddlEstadoCivil.SelectedItem.Text == "Selecione")
        {
            ddlEstadoCivil.Focus();
            lblMensagem.Text = "Selecione um estado Civil";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else if (ddlCidadeCliF.SelectedItem.Text == "Selecione")
        {
            ddlCidadeCliF.Focus();
            lblMensagem.Text = "Selecione uma cidade";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtEmailCliF.Text))
        {
            txtEmailCliF.Focus();
            lblMensagem.Text = "Insira um Email";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtLoginCliF.Text))
        {
            txtLoginCliF.Focus();
            lblMensagem.Text = "Insira um Nome de usuario";
            divStatusCliF.Attributes["class"] = "alert alert-danger";
        }
        else
        {
            clifisico.Cpf = txtCPFCliF.Text;
            clifisico.Rg = txtRgCliF.Text;
            clifisico.DataNascimento = Convert.ToDateTime(txtDataNascimentoCliF.Text);
            clifisico.UserName = txtLoginCliF.Text;
            clifisico.Sexo = ddlSexoCliF.SelectedItem.Value;
            EstadoCivil estCivil = new EstadoCivil();
            estCivil.Codigo = Convert.ToInt32(ddlEstadoCivil.SelectedItem.Value);
            clifisico.EstadoCivil = estCivil;

            ContatoDB conDB = new ContatoDB();
            Contato contato = new Contato();
            contato = conDB.SelectContato(clifisico.ContatoPessoa.Codigo);
            contato.Nome = txtNomeCliF.Text;
            contato.Telefone = txtTelefoneCliF.Text;
            contato.Celular = txtCelularCliF.Text;
            contato.Email = txtEmailCliF.Text;

            EnderecoDB endDB = new EnderecoDB();
            Endereco endereco = new Endereco();
            endereco = endDB.Select(clifisico.Endereco.Codigo);
            endereco.Logradouro = txtEnderecoCliF.Text;
            endereco.Bairro = txtBairroCliF.Text;
            endereco.Complemento = txtComplementoCliF.Text;
            endereco.Numero = txtNumeroCliF.Text;
            endereco.Cep = txtCEPCliF.Text;

            CidadeDB cidDB = new CidadeDB();
            Cidade cidade = new Cidade();
            endereco.Cidade.Codigo = Convert.ToInt32(ddlCidadeCliF.SelectedValue);

            endDB.Update(endereco);
            conDB.Update(contato);

            if (clifisicoDB.Update(clifisico))
            {
                lblMensagem.Text = "Dados atualizados";
                divStatusCliF.Attributes["class"] = "alert alert-success";
                CarregaCliente();
            }
            else
            {
                lblMensagem.Text = "Cliente não foi atualizado";
            }
        }
    }
    protected void btnSalvarCliJu_Click(object sender, EventArgs e)
    {
        ClienteJuridicoDB clijuridicoDB = new ClienteJuridicoDB();
        ClienteJuridico clijuridico = new ClienteJuridico();
        clijuridico = clijuridicoDB.Select(Convert.ToInt32(Session["Editar"]));
        string cnpj = clijuridico.Cnpj;
        string login = clijuridico.UserName;
        if (clijuridicoDB.ValidaCNPJ(txtCnpjCliJu.Text) != null && cnpj != txtCnpjCliJu.Text)
        {
            txtCnpjCliJu.Focus();
            lblMensagemJu.Text = "CNPJ já cadastrado!";
            divStatusCliJu.Attributes["class"] = "alert alert-danger";
        }
        else if (clijuridicoDB.ValidaLogin(txtLoginCliJu.Text) != null && login != txtLoginCliJu.Text)
        {
            txtLoginCliJu.Focus();
            lblMensagemJu.Text = "Login já existente";
            divStatusCliJu.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtNomeCliJu.Text))
        {
            txtNomeCliJu.Focus();
            lblMensagemJu.Text = "Insira um Nome";
            divStatusCliJu.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtCelularCliJu.Text) && string.IsNullOrWhiteSpace(txtTelefoneCliJu.Text))
        {
            txtTelefoneCliJu.Focus();
            lblMensagemJu.Text = "Insira ao menos um telefone";
            divStatusCliJu.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtCnpjCliJu.Text))
        {
            txtCnpjCliJu.Focus();
            lblMensagemJu.Text = "Insira um CNPJ Valido";
            divStatusCliJu.Attributes["class"] = "alert alert-danger";
        }
        else if (ddlEstadoCliJu.SelectedItem.Text == "Selecione")
        {
            ddlEstadoCliJu.Focus();
            lblMensagemJu.Text = "Selecione um estado";
            divStatusCliJu.Attributes["class"] = "alert alert-danger";
        }
        else if (ddlCidadeCliJu.SelectedItem.Text == "Selecione")
        {
            ddlCidadeCliJu.Focus();
            lblMensagemJu.Text = "Selecione uma cidade";
            divStatusCliJu.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtEmailCliJu.Text))
        {
            txtEmailCliJu.Focus();
            lblMensagemJu.Text = "Insira um Email";
            divStatusCliJu.Attributes["class"] = "alert alert-danger";
        }
        else if (string.IsNullOrWhiteSpace(txtLoginCliJu.Text))
        {
            txtLoginCliJu.Focus();
            lblMensagemJu.Text = "Insira um Nome de usuario";
            divStatusCliJu.Attributes["class"] = "alert alert-danger";
        }
        else
        {

            clijuridico.Cnpj = txtCnpjCliJu.Text;
            clijuridico.UserName = txtLoginCliJu.Text;

            ContatoDB conDB = new ContatoDB();
            Contato contato = new Contato();
            contato = conDB.SelectContato(clijuridico.ContatoPessoa.Codigo);
            contato.Nome = txtNomeCliJu.Text;
            contato.Telefone = txtTelefoneCliJu.Text;
            contato.Celular = txtCelularCliJu.Text;
            contato.Email = txtEmailCliJu.Text;

            EnderecoDB endDB = new EnderecoDB();
            Endereco endereco = new Endereco();
            endereco = endDB.Select(clijuridico.Endereco.Codigo);
            endereco.Logradouro = txtEnderecoCliJu.Text;
            endereco.Bairro = txtBairroCliJu.Text;
            endereco.Complemento = txtComplementoCliJu.Text;
            endereco.Numero = txtNumeroCliJu.Text;
            endereco.Cep = txtCEPCliJu.Text;

            CidadeDB cidDB = new CidadeDB();
            Cidade cidade = new Cidade();
            endereco.Cidade.Codigo = Convert.ToInt32(ddlCidadeCliJu.SelectedValue);


            endDB.Update(endereco);
            conDB.Update(contato);

            if (clijuridicoDB.Update(clijuridico))
            {
                lblMensagemJu.Text = "Dados atualizados";
                divStatusCliJu.Attributes["class"] = "alert alert-success";
                CarregaCliente();
            }
            else
            {
                lblMensagem.Text = "Cliente não foi atualizado";
            }
        }
    }
    private void CarregaEstadoCivil()
    {
        EstadoCivilDB estCivilDB = new EstadoCivilDB();
        DataSet ds = estCivilDB.SelecAllEstadoCivil();
        Function.CarregaDDL(ds, ddlEstadoCivil, "ECI_CODIGO", "ECI_DESCRICAO");
    }
    protected void ddlEstadoCliF_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregaCidade(ddlCidadeCliF, Convert.ToInt32(ddlEstadoCliF.SelectedItem.Value));
    }
    protected void btnVoltarCliF_Click(object sender, EventArgs e)
    {
        Session["Juridico"] = null;
        modalEditarCliJu.Hide();
        modalDetalhesCliJu.Hide();
        Session["Fisico"] = null;
        modalEditarCliF.Hide();
        modalDetalheCliF.Hide();
        lblMensagemJu.Text = string.Empty;
        divStatusCliJu.Attributes["class"] = "";
        lblMensagem.Text = string.Empty;
        divStatusCliF.Attributes["class"] = "";
    }
    public void CarregaCliJuridicoTextBox(int id)
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

        txtCnpjCliJu.Text = clijuridico.Cnpj;
        txtLoginCliJu.Text = clijuridico.UserName;

        contato = conDB.SelectContato(clijuridico.ContatoPessoa.Codigo);
        txtNomeCliJu.Text = contato.Nome;
        txtTelefoneCliJu.Text = contato.Telefone;
        txtCelularCliJu.Text = contato.Celular;
        txtEmailCliJu.Text = contato.Email;


        endereco = endDB.Select(clijuridico.Endereco.Codigo);
        txtEnderecoCliJu.Text = endereco.Logradouro;
        txtBairroCliJu.Text = endereco.Bairro;
        txtComplementoCliJu.Text = endereco.Complemento;
        txtNumeroCliJu.Text = endereco.Numero;
        txtCEPCliJu.Text = Convert.ToString(endereco.Cep);

        cid = cidDB.SelectCidadePessoa(endereco.Cidade.Codigo);
        est = estDB.SelectEstado(cid.Estado.Codigo);

        Function.CarregaItemDDLByCodigo(ddlEstadoCliJu, est.Codigo);
        CarregaCidade(ddlCidadeCliJu, est.Codigo);
        Function.CarregaItemDDLByCodigo(ddlCidadeCliJu, cid.Codigo);
    }
    private void CarregaEstados(DropDownList ddl)
    {
        EstadoDB estDB = new EstadoDB();
        DataSet dsEstado = estDB.SelectAll();
        Function.CarregaDDL(dsEstado, ddl, "EST_CODIGO", "EST_ESTADO");
    }
    private void CarregaCidade(DropDownList ddl, int estado)
    {
        CidadeDB cidDB = new CidadeDB();
        DataSet dsCidade = cidDB.SelectAll(estado);
        Function.CarregaDDL(dsCidade, ddl, "CID_CODIGO", "CID_CIDADE");
    }
    public void CarregaCliente()
    {
        ClienteFisicoDB cliFisicoDB = new ClienteFisicoDB();
        DataSet dsCliente = cliFisicoDB.SelectAllClientes(Convert.ToInt32(Session["Advogado"]));
        Function.CarregaGrid(dsCliente, gdvCliente, lblQtd);
    }
    public void CarregaCliFisicoTextBox(int id)
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

        txtRgCliF.Text = clifisico.Rg;
        txtCPFCliF.Text = clifisico.Cpf;
        txtDataNascimentoCliF.Text = clifisico.DataNascimento.ToString("dd/MM/yyyy");
        txtLoginCliF.Text = clifisico.UserName;
        Function.CarregaItemDDLByTexto(ddlSexoCliF, clifisico.Sexo);
        Function.CarregaItemDDLByCodigo(ddlEstadoCivil, clifisico.EstadoCivil.Codigo);

        contato = conDB.SelectContato(clifisico.ContatoPessoa.Codigo);
        txtNomeCliF.Text = contato.Nome;
        txtTelefoneCliF.Text = contato.Telefone;
        txtCelularCliF.Text = contato.Celular;
        txtEmailCliF.Text = contato.Email;

        endereco = endDB.Select(clifisico.Endereco.Codigo);
        txtEnderecoCliF.Text = endereco.Logradouro;
        txtBairroCliF.Text = endereco.Bairro;
        txtComplementoCliF.Text = endereco.Complemento;
        txtNumeroCliF.Text = endereco.Numero;
        txtCEPCliF.Text = Convert.ToString(endereco.Cep);

        cid = cidDB.SelectCidadePessoa(endereco.Cidade.Codigo);
        est = estDB.SelectEstado(cid.Estado.Codigo);

        Function.CarregaItemDDLByCodigo(ddlEstadoCliF, est.Codigo);
        CarregaCidade(ddlCidadeCliF, est.Codigo);
        Function.CarregaItemDDLByCodigo(ddlCidadeCliF, cid.Codigo);

    }
    public void CarregaDetalhesFisicoLabel(int id)
    {
        EnderecoDB endDB = new EnderecoDB();
        Endereco endereco = new Endereco();
        ClienteFisicoDB cliDB = new ClienteFisicoDB();
        ClienteFisico clientefisico = new ClienteFisico();
        EstadoCivil estCivil = new EstadoCivil();
        EstadoCivilDB estCivilDB = new EstadoCivilDB();
        ContatoDB conDB = new ContatoDB();
        Contato contato = new Contato();
        CidadeDB cidDB = new CidadeDB();
        Cidade cid = new Cidade();
        EstadoDB estDB = new EstadoDB();
        Estado est = new Estado();

        clientefisico = cliDB.Select(id);

        lblCPFCliF.Text = clientefisico.Cpf;
        lblRGCliF.Text = clientefisico.Rg;
        lblDataNascimentoCliF.Text = clientefisico.DataNascimento.ToString("dd/MM/yyyy");
        lblSexoCliF.Text = clientefisico.Sexo;
        lblLoginCliF.Text = clientefisico.UserName;

        estCivil = estCivilDB.Select(clientefisico.EstadoCivil.Codigo);

        lblestadoCivilCliF.Text = estCivil.Descricao;

        contato = conDB.SelectContato(clientefisico.ContatoPessoa.Codigo);
        lblNomeCliF.Text = contato.Nome;
        lblTelefoneCliF.Text = contato.Telefone;
        lblCelularCliF.Text = contato.Celular;
        lblEmailCliF.Text = contato.Email;

        endereco = endDB.Select(clientefisico.Endereco.Codigo);
        lblEnderecoCliF.Text = endereco.Logradouro;
        lblBairroCliF.Text = endereco.Bairro;
        lblComplementoCliF.Text = endereco.Complemento;
        lblNumeroCliF.Text = endereco.Numero;
        lblCEPCliF.Text = Convert.ToString(endereco.Cep);

        cid = cidDB.SelectCidadePessoa(endereco.Cidade.Codigo);
        lblCidadeCliF.Text = cid.NomeCidade;
        est = estDB.SelectEstado(cid.Estado.Codigo);
        lblEstadoCliF.Text = est.Descricao;
    }
    public void CarregaDetalhesJuridico(int id)
    {
        EnderecoDB endDB = new EnderecoDB();
        Endereco endereco = new Endereco();
        ClienteJuridicoDB cliDB = new ClienteJuridicoDB();
        ClienteJuridico clientejuridico = new ClienteJuridico();
        ContatoDB conDB = new ContatoDB();
        Contato contato = new Contato();
        CidadeDB cidDB = new CidadeDB();
        Cidade cid = new Cidade();
        EstadoDB estDB = new EstadoDB();
        Estado est = new Estado();

        clientejuridico = cliDB.Select(id);

        lblCNPJCliJu.Text = clientejuridico.Cnpj;
        lblLoginCliJu.Text = clientejuridico.UserName;

        contato = conDB.SelectContato(clientejuridico.ContatoPessoa.Codigo);
        lblNomeCliJu.Text = contato.Nome;
        lblTelefoneCliJu.Text = contato.Telefone;
        lblCelularCliJu.Text = contato.Celular;
        lblEmailCliJu.Text = contato.Email;

        endereco = endDB.Select(clientejuridico.Endereco.Codigo);
        lblEnderecoCliJu.Text = endereco.Logradouro;
        lblBairroCliJu.Text = endereco.Bairro;
        lblComplementoCliJu.Text = endereco.Complemento;
        lblNumeroCliJu.Text = endereco.Numero;
        lblCEPCliJu.Text = Convert.ToString(endereco.Cep);

        cid = cidDB.SelectCidadePessoa(endereco.Cidade.Codigo);
        lblCidadeCliJu.Text = cid.NomeCidade;
        est = estDB.SelectEstado(cid.Estado.Codigo);
        lblEstadoCliJu.Text = est.Descricao;
    }
    protected void gdvCliente_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvCliente.PageIndex = e.NewPageIndex;
        CarregaCliente();
    }
}