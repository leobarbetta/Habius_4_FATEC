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

public partial class Pages_Administrativo_Advogado_CadastraProcesso : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCamara.CssClass = "form-control input-lg";
        ddlTribunal.CssClass = "form-control input-lg";
        if (!Page.IsPostBack)
        {
            txtDataCadastro.Text = DateTime.Today.ToString("dd/MM/yyyy");
            Page.Title = "Processo";
            CarregaCliente();
            CarregaEstado();
            CarregaTribunal();
            CarregaClasse();
            CarregaVara();
            CarregaMovimentacao();
            CarregaPosicao();
        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        if (ValidaProcesso(lblMensagem))
        {
            if (CadastraProcesso())
            {
                LimpaProcesso();
                ddlCliente.Focus();
                txtDataCadastro.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
        }
    }
    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregaCidade(Convert.ToInt32(ddlEstado.SelectedItem.Value));
    }
    protected void rblRecurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblRecurso.SelectedItem.Value == "0")
        {
            Function.LimpaDDL(ddlTribunal);
            txtCamara.Text = string.Empty;
            ddlTribunal.Enabled = false;
            txtCamara.Enabled = false;
        }
        else if (rblRecurso.SelectedItem.Value == "1")
        {
            ddlTribunal.Enabled = true;
            txtCamara.Enabled = true;
        }
    }
    private void CarregaCliente()
    {
        ClienteFisicoDB bd = new ClienteFisicoDB();
        DataSet ds = bd.SelectAllDDLCliente(Convert.ToInt32(Session["Advogado"]));
        Function.CarregaDDL(ds, ddlCliente, "PES_CODIGO", "CON_NOME");
    }
    private void CarregaEstado()
    {
        EstadoDB estDB = new EstadoDB();
        DataSet dsEstado = estDB.SelectAll();
        Function.CarregaDDL(dsEstado, ddlEstado, "EST_CODIGO", "EST_ESTADO");
    }
    private void CarregaCidade(int id)
    {
        CidadeDB cidDB = new CidadeDB();
        DataSet dsCidade = cidDB.SelectAll(id);
        Function.CarregaDDL(dsCidade, ddlCidade, "CID_CODIGO", "CID_CIDADE");
    }
    private void CarregaClasse()
    {
        ClasseDB claDB = new ClasseDB();
        DataSet dsClasse = claDB.SelectAllClasses();
        Function.CarregaDDL(dsClasse, ddlClasse, "CLA_CODIGO", "CLA_CLASSE");
    }
    private void CarregaTribunal()
    {
        RecursoDB recDB = new RecursoDB();
        DataSet dsTribunal = recDB.SelectAllTribunal();
        Function.CarregaDDL(dsTribunal, ddlTribunal, "TRI_CODIGO", "TRI_TRIBUNAL");
    }
    private void CarregaVara()
    {
        VaraDB varaDB = new VaraDB();
        DataSet dsVara = varaDB.SelectAllVara();
        Function.CarregaDDL(dsVara, ddlVara, "VAR_CODIGO", "VAR_VARA");
    }
    private void CarregaMovimentacao()
    {
        MovimentacaoDB movDB = new MovimentacaoDB();
        DataSet dsMovimentacao = movDB.SelectAllMovimentacao();
        Function.CarregaDDL(dsMovimentacao, ddlMovimentacao, "MOV_CODIGO", "MOV_MOVIMENTACAO");
    }
    private void CarregaPosicao()
    {
        PosicaoClienteDB posDB = new PosicaoClienteDB();
        DataSet dsPosicao = posDB.SelectAllPosicao();
        Function.CarregaDDL(dsPosicao, ddlPosicao, "POS_CODIGO", "POS_POSICAO");
    }
    private bool CadastraProcesso()
    {
        Recurso rec = new Recurso();
        Recurso recurso = new Recurso();
        RecursoDB recDB = new RecursoDB();
        Assunto assunto = new Assunto();
        AssuntoDB assuntoDB = new AssuntoDB();
        DataProcesso dataProcesso = new DataProcesso();
        Movimentacao mov = new Movimentacao();
        Pessoa pes = new Pessoa();
        Advogado adv = new Advogado();
        Vara vara = new Vara();
        PosicaoCliente pos = new PosicaoCliente();
        Cidade cid = new Cidade();
        Classe cla = new Classe();
        Processo pro = new Processo();
        ProcessoDB proDB = new ProcessoDB();
        if (rblRecurso.SelectedItem.Value == "1")
        {
            rec.Camara = txtCamara.Text;
            recDB.InsertCamara(rec);
            recurso.CodigoCamara = recDB.GetLastIdCamara(rec.Camara);
            recurso.CodigoTribunal = Convert.ToInt32(ddlTribunal.SelectedItem.Value);
            recDB.InsertRecurso(recurso);
            rec.Codigo = recDB.GetLastIdRecurso(recurso);
            pro.Recurso = rec;
        }
        assunto.Descricao = txtAssunto.Text;
        assuntoDB.Insert(assunto);
        assunto.Codigo = assuntoDB.GetLastId(assunto);
        pro.Assunto = assunto;


        mov.Codigo = Convert.ToInt32(ddlMovimentacao.SelectedItem.Value);
        pro.Movimentacao = mov;

        adv.Codigo = Convert.ToInt32(Session["Advogado"]);
        pro.PessoaAdvogado = adv;

        vara.Codigo = Convert.ToInt32(ddlVara.SelectedItem.Value);
        pro.Vara = vara;

        cla.Codigo = Convert.ToInt32(ddlClasse.SelectedItem.Value);
        pro.Classe = cla;

        pos.Codigo = Convert.ToInt32(ddlPosicao.SelectedItem.Value);
        pro.PosicaoCliente = pos;

        cid.Codigo = Convert.ToInt32(ddlCidade.SelectedItem.Value);
        pro.Comarca = cid;

        pro.DataCriacao = Convert.ToDateTime(txtDataCadastro.Text);
        pro.Descricao = txtDescricao.Text;
        pro.Observacao = txtObservacao.Text;
        pro.NumeroProcesso = txtNumero.Text;

        pes.Codigo = Convert.ToInt32(ddlCliente.SelectedItem.Value);
        pro.PessoaCliente = pes;


        //Persistencia
        if (proDB.Insert(pro))
        {
            //pegar ultimo ID
            Processo processo = new Processo();
            processo.Codigo = proDB.GetLastId(pro);

            //dar insert nas tabelas de cliente e na de advogado
            proDB.InsertClienteProcesso(pro.PessoaCliente.Codigo, processo.Codigo);
            proDB.InsertAdvogadoProcesso(adv.Codigo, processo.Codigo);

            //insert em movimentação
            MovimentacaoDB movDB = new MovimentacaoDB();
            mov.Processo = processo;
            mov.DataMovimentacao = Convert.ToDateTime(txtDataCadastro.Text);
            movDB.Insert(mov);

            if (!string.IsNullOrWhiteSpace(txtDataProcesso.Text))
            {
                DataProcessoDB dataDB = new DataProcessoDB();
                dataProcesso.Processo = processo;
                TimeSpan hora = Convert.ToDateTime(txtHoraProcesso.Text).TimeOfDay;
                DateTime data = Convert.ToDateTime(txtDataProcesso.Text);
                dataProcesso.DataAudiencia = data + hora;
                dataDB.Insert(dataProcesso);
            }
            lblMensagem.Text = "Processo Inserido com Sucesso!";
            divMensagem.Attributes["class"] = "alert alert-success";
            return true;
        }
        else
        {
            lblMensagem.Text = "Erro ao inserir processo";
            divMensagem.Attributes["class"] = "alert alert-danger";
            return false;
        }
    }
    private bool ValidaProcesso(Label lbl)
    {
        if (string.IsNullOrWhiteSpace(txtNumero.Text))
        {
            lbl.Text = "Insira o Numero do processo!";
        }
        else if (string.IsNullOrWhiteSpace(txtAssunto.Text))
        {
            lbl.Text = "Insira o Assunto do processo";
        }
        else if (string.IsNullOrWhiteSpace(txtDataCadastro.Text))
        {
            lbl.Text = "Insira uma data de inicio para o processo";
        }
        else if (string.IsNullOrWhiteSpace(txtDescricao.Text))
        {
            lbl.Text = "Insira uma descrição para o processo";
        }
        else if (ddlEstado.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "'Selecione uma comarca!";
        }
        else if (ddlCidade.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Selecione uma comarca!";
        }
        else if (ddlClasse.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Seleciona uma classe para o processo!";
        }
        else if (ddlCliente.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "'Seleciona um Cliente!";
        }
        else if (ddlMovimentacao.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Selecione uma movimentação inicial para o processo!";
        }
        else if (ddlVara.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Seleciona uma vara para o processo!";
        }
        else
        {
            if (rblRecurso.SelectedItem.Value == "1")
            {
                if (ddlTribunal.SelectedItem.Text == "Selecione")
                {
                    lbl.Text = "Selecione um tribunal para o processo!";
                }
                else if (string.IsNullOrWhiteSpace(txtCamara.Text))
                {
                    lbl.Text = "Insira uma Camara para o processo!";
                }
                else
                {
                    return true;
                }
                divMensagem.Attributes["class"] = "alert alert-danger";
                return false;
            }
            else
            {
                return true;
            }
        }
        divMensagem.Attributes["class"] = "alert alert-danger";
        return false;
    }
    private void LimpaProcesso()
    {
        txtNumero.Text = string.Empty;
        txtAssunto.Text = string.Empty;
        txtDataCadastro.Text = string.Empty;
        txtCamara.Text = string.Empty;
        txtDescricao.Text = string.Empty;
        txtDataProcesso.Text = string.Empty;
        txtObservacao.Text = string.Empty;
        txtHoraProcesso.Text = string.Empty;
        for (int i = 0; i < ddlCliente.Items.Count; i++)
        {
            ddlCliente.Items[i].Selected = false;
        }
        for (int i = 0; i < ddlClasse.Items.Count; i++)
        {
            ddlClasse.Items[i].Selected = false;
        }
        for (int i = 0; i < ddlMovimentacao.Items.Count; i++)
        {
            ddlMovimentacao.Items[i].Selected = false;
        }
        for (int i = 0; i < ddlCidade.Items.Count; i++)
        {
            ddlCidade.Items[i].Selected = false;
        }
        for (int i = 0; i < ddlEstado.Items.Count; i++)
        {
            ddlEstado.Items[i].Selected = false;
        }
        for (int i = 0; i < ddlPosicao.Items.Count; i++)
        {
            ddlPosicao.Items[i].Selected = false;
        }
        for (int i = 0; i < ddlTribunal.Items.Count; i++)
        {
            ddlTribunal.Items[i].Selected = false;
        }
        for (int i = 0; i < ddlVara.Items.Count; i++)
        {
            ddlVara.Items[i].Selected = false;
        }
        rblRecurso.Items[1].Selected = false;
        rblRecurso.Items[0].Selected = true;
    }
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../../Pages/Administrativo/Advogado/HomeAdvogado.aspx");
    }
}


