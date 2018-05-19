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

public partial class Pages_Administrativo_Advogado_ListarProcessos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Processos";

        txtCamara.CssClass = "form-control";
        ddlTribunal.CssClass = "form-control";
        if (Session["ManterModalAberto"] != null)
        {
            modalEditarProcesso.Show();
        }
        if (!Page.IsPostBack)
        {
            CarregaCliente();
            CarregaTribunal();
            CarregaEstado();
            CarregaClasse();
            CarregaVara();
            CarregaPosicao();
            CarregaProcesso(Convert.ToInt32(Session["Advogado"]));
        }
    }
    protected void btnBusca_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtBusca.Text))
        {
            CarregaProcesso(Convert.ToInt32(Session["Advogado"]));
        }
        else
        {
            ProcessoDB proDB = new ProcessoDB();
            DataSet ds = proDB.BuscaProcesso(Convert.ToInt32(Session["Advogado"]), txtBusca.Text);
            Function.CarregaGrid(ds, gdvProcesso, lblQtd);
            txtBusca.Focus();
        }
    }
    protected void gdvProcesso_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Processo processo = new Processo();
        ProcessoDB ageDB = new ProcessoDB();
        switch (e.CommandName)
        {
            case "Editar":
                Session["Processo"] = e.CommandArgument;
                Session["ManterModalAberto"] = e.CommandArgument;
                CarregaProcessoModal(Convert.ToInt32(Session["Processo"]));
                modalEditarProcesso.Show();
                break;
            case "Custos":
                Session["ProcessoCusto"] = e.CommandArgument;
                Response.Redirect("CadastraCustoProcesso.aspx");
                break;
            case "Detalhes":
                LimpaLBL();
                CarregaDetalhesProcesso(Convert.ToInt32(e.CommandArgument));
                modalDetalhes.Show();
                break;
        }
    }
    protected void gdvProcesso_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvProcesso.EditIndex = -1;
        CarregaProcesso(Convert.ToInt32(Session["Advogado"]));
    }
    protected void gdvProcesso_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdvProcesso.EditIndex = e.NewEditIndex;
        CarregaProcesso(Convert.ToInt32(Session["Advogado"]));
    }
    protected void gdvProcesso_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList ddlMovimentacao = (DropDownList)gdvProcesso.Rows[e.RowIndex].FindControl("ddlMovimentacao");
        Label codigo_processo = (Label)gdvProcesso.Rows[e.RowIndex].FindControl("lblProcesso");
        Label codigo_movimentacao = (Label)gdvProcesso.Rows[e.RowIndex].FindControl("lblCodigo_movimentacao");
        if (ddlMovimentacao.SelectedItem.Text == "Selecione")
        {
            lblMensagemTopo.Text = "Dados alterados com Sucesso";
            divMensagemTopo.Attributes["class"] = "alert alert-success";
        }
        else
        {
            Movimentacao mov = new Movimentacao();
            MovimentacaoDB movDB = new MovimentacaoDB();
            Processo pro = new Processo();
            mov.Codigo = Convert.ToInt32(ddlMovimentacao.SelectedItem.Value);
            pro.Codigo = Convert.ToInt32(codigo_processo.Text);
            mov.Processo = pro;
            mov.DataMovimentacao = DateTime.Today;

            int codigo = Convert.ToInt32(codigo_movimentacao.Text);

            if (movDB.Finaliza(codigo))
            {
                if (movDB.Insert(mov))
                {
                    gdvProcesso.EditIndex = -1;
                    CarregaProcesso(Convert.ToInt32(Session["Advogado"]));
                }
                else
                {
                    lblMensagemTopo.Text = "Erro ao alterar status";
                    divMensagemTopo.Attributes["class"] = "alert alert-danger";
                }
            }
        }
    }
    protected void gdvProcesso_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                MovimentacaoDB movDB = new MovimentacaoDB();
                DataSet dsMovimentacao = movDB.SelectAllMovimentacao();
                DropDownList ddlMovimentacao2 = (DropDownList)e.Row.FindControl("ddlMovimentacao");
                Function.CarregaDDL(dsMovimentacao, ddlMovimentacao2, "MOV_CODIGO", "MOV_MOVIMENTACAO");
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
            CarregaTribunal();
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session["ManterModalAberto"] = null;
        modalEditarProcesso.Hide();
        lblMensagem.Text = string.Empty;
        divStatusPro.Attributes["class"] = "";
    }
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (ValidaProcesso(lblMensagem))
        {
            UpdateProcesso();
            CarregaProcesso(Convert.ToInt32(Session["Advogado"]));
        }
    }
    private bool UpdateProcesso()
    {
        Recurso rec = new Recurso();
        Recurso recurso = new Recurso();
        RecursoDB recDB = new RecursoDB();
        Assunto assunto = new Assunto();
        AssuntoDB assuntoDB = new AssuntoDB();
        DataProcesso dataProcesso = new DataProcesso();
        DataProcessoDB dataDB = new DataProcessoDB();
        Movimentacao mov = new Movimentacao();
        Pessoa pes = new Pessoa();
        Advogado adv = new Advogado();
        Vara vara = new Vara();
        PosicaoCliente pos = new PosicaoCliente();
        Cidade cid = new Cidade();
        Classe cla = new Classe();
        Processo pro = new Processo();
        ProcessoDB proDB = new ProcessoDB();
        pro = proDB.Select(Convert.ToInt32(Session["Processo"]));
        int valor = Convert.ToInt32(Session["Processo"]);

        if (pro.Recurso == null)
        {
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
            else if (rblRecurso.SelectedItem.Value == "0")
            {
                txtCamara.Text = string.Empty;
                Function.LimpaDDL(ddlTribunal);
            }
        }
        else
        {
            rec = recDB.Select(pro.Recurso.Codigo);
            if (rblRecurso.SelectedItem.Value == "1")
            {
                rec.Camara = txtCamara.Text;
                //update
                recDB.UpdateCamara(rec);
                rec.CodigoTribunal = Convert.ToInt32(ddlTribunal.SelectedItem.Value);
                //update
                recDB.UpdateRecurso(rec);
            }
            else if (rblRecurso.SelectedItem.Value == "0")
            {
                txtCamara.Text = string.Empty;
                Function.LimpaDDL(ddlTribunal);
            }
        }
        assunto.Codigo = pro.Assunto.Codigo;
        assunto.Descricao = txtAssunto.Text;
        //update
        assuntoDB.Update(assunto);

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
        //Update
        if (proDB.Update(pro))
        {
            //Update cliente
            //dar insert nas tabelas de cliente e na de advogado
            proDB.UpdateClienteProcesso(pro.PessoaCliente.Codigo, Convert.ToInt32(Session["Processo"]));

            dataProcesso = dataDB.SelectByProcesso(pro.Codigo);
            if (dataProcesso != null)
            {
                if (!string.IsNullOrWhiteSpace(txtDataProcesso.Text))
                {
                    //pegar do cadastra processo ja atualizado e dar update
                    pro.Codigo = Convert.ToInt32(Session["Processo"]);
                    dataProcesso.Processo = pro;
                    TimeSpan hora = Convert.ToDateTime(txtHoraAudiencia.Text).TimeOfDay;
                    DateTime data = Convert.ToDateTime(txtDataProcesso.Text);
                    dataProcesso.DataAudiencia = data + hora;
                    dataDB.Update(dataProcesso);
                }
                else if (string.IsNullOrWhiteSpace(txtDataProcesso.Text))
                {
                    dataDB.Delete(Convert.ToInt32(Session["Processo"]));
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(txtDataProcesso.Text))
                {
                    if (string.IsNullOrWhiteSpace(txtHoraAudiencia.Text))
                    {
                        //msg para entrar com uma hora para o update rolar
                    }
                    else if (!string.IsNullOrWhiteSpace(txtDataProcesso.Text) && !string.IsNullOrWhiteSpace(txtHoraAudiencia.Text))
                    {
                        pro.Codigo = Convert.ToInt32(Session["Processo"]);
                        DataProcesso dataa = new DataProcesso();
                        dataa.Processo = pro;
                        TimeSpan hora = Convert.ToDateTime(txtHoraAudiencia.Text).TimeOfDay;
                        DateTime data = Convert.ToDateTime(txtDataProcesso.Text);
                        dataa.DataAudiencia = data + hora;
                        dataDB.Insert(dataa);
                    }
                }

            }
        }
        lblMensagem.Text = "Dados alterados com sucesso!";
        divStatusPro.Attributes["class"] = "alert alert-success";
        return true;
    }
    private bool CarregaProcessoModal(int codigo)
    {
        try
        {
            Recurso rec = new Recurso();
            Recurso recurso = new Recurso();
            RecursoDB recDB = new RecursoDB();
            Assunto assunto = new Assunto();
            AssuntoDB assuntoDB = new AssuntoDB();
            DataProcesso dataProcesso = new DataProcesso();
            DataProcessoDB dataDB = new DataProcessoDB();
            Movimentacao mov = new Movimentacao();
            Pessoa pes = new Pessoa();
            Advogado adv = new Advogado();
            Vara vara = new Vara();
            PosicaoCliente pos = new PosicaoCliente();
            Cidade cid = new Cidade();
            CidadeDB cidDB = new CidadeDB();
            Classe cla = new Classe();
            Processo pro = new Processo();
            ProcessoDB proDB = new ProcessoDB();

            pro = proDB.Select(codigo);

            txtNumero.Text = pro.NumeroProcesso;
            txtDescricao.Text = pro.Descricao;
            txtObservacao.Text = pro.Observacao;
            txtDataCadastro.Text = pro.DataCriacao.ToString("dd/MM/yyyy");

            Function.CarregaItemDDLByCodigo(ddlVara, pro.Vara.Codigo);

            cid = cidDB.SelectCidadePessoa(pro.Comarca.Codigo);
            Function.CarregaItemDDLByCodigo(ddlEstado, cid.Estado.Codigo);

            CarregaCidade(cid.Estado.Codigo);
            Function.CarregaItemDDLByCodigo(ddlCidade, pro.Comarca.Codigo);

            Function.CarregaItemDDLByCodigo(ddlClasse, pro.Classe.Codigo);

            Function.CarregaItemDDLByCodigo(ddlPosicao, pro.PosicaoCliente.Codigo);

            assunto = assuntoDB.Select(pro.Assunto.Codigo);
            txtAssunto.Text = assunto.Descricao;

            Function.CarregaItemDDLByCodigo(ddlCliente, pro.PessoaCliente.Codigo);

            //selecionar dataaudiencia
            dataProcesso = dataDB.SelectByProcesso(codigo);
            if (dataProcesso != null)
            {
                txtDataProcesso.Text = dataProcesso.DataAudiencia.ToString("dd/MM/yyyy");
                txtHoraAudiencia.Text = dataProcesso.DataAudiencia.ToString("t");
            }
            else
            {
                txtDataProcesso.Text = string.Empty;
                txtHoraAudiencia.Text = string.Empty;
            }
            if (pro.Recurso != null)
            {
                txtCamara.Enabled = true;
                ddlTribunal.Enabled = true;
                rec = recDB.Select(pro.Recurso.Codigo);
                int tribunal = rec.CodigoTribunal;
                int camara = rec.CodigoCamara;

                rblRecurso.Items[0].Selected = false;
                rblRecurso.Items[1].Selected = true;

                Function.CarregaItemDDLByCodigo(ddlTribunal, tribunal);
                rec = recDB.SelectCamara(camara);
                txtCamara.Text = rec.Camara;
            }
            else
            {
                txtCamara.Text = string.Empty;
                Function.LimpaDDL(ddlTribunal);
                txtCamara.Enabled = false;
                ddlTribunal.Enabled = false;
                rblRecurso.Items[0].Selected = true;
                rblRecurso.Items[1].Selected = false;
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    private void CarregaCliente()
    {
        ClienteFisicoDB bd = new ClienteFisicoDB();
        DataSet ds = bd.SelectAllDDLCliente(Convert.ToInt32(Session["Advogado"]));
        Function.CarregaDDL(ds, ddlCliente, "PES_CODIGO", "CON_NOME");
    }
    private void CarregaTipoDespesas(DropDownList ddl)
    {
        TipoDespesasDB bd = new TipoDespesasDB();
        DataSet ds = bd.SelectAllDDLCategoriaProcesso();
        Function.CarregaDDL(ds, ddl, "TID_CODIGO", "TID_TIPODESPESA");
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
    private void CarregaProcesso(int adv)
    {
        ProcessoDB proDB = new ProcessoDB();
        DataSet ds = proDB.SelectAllByAdvogado(adv);
        Function.CarregaGrid(ds, gdvProcesso, lblQtd);
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
    private void CarregaPosicao()
    {
        PosicaoClienteDB posDB = new PosicaoClienteDB();
        DataSet dsPosicao = posDB.SelectAllPosicao();
        Function.CarregaDDL(dsPosicao, ddlPosicao, "POS_CODIGO", "POS_POSICAO");
    }
    private bool ValidaProcesso(Label lbl)
    {
        if (string.IsNullOrWhiteSpace(txtNumero.Text))
        {
            lbl.Text = "Insirua o Numero do processo!";
        }
        else if (string.IsNullOrWhiteSpace(txtAssunto.Text))
        {
            lbl.Text = "Insira o Assunto do processo";
        }
        else if (string.IsNullOrWhiteSpace(txtDataCadastro.Text))
        {
            lbl.Text = "Insira uma data para o processo";
        }
        else if (string.IsNullOrWhiteSpace(txtDescricao.Text))
        {
            lbl.Text = "Insira uma descrição para o processo";
        }
        else if (ddlEstado.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Selecione uma comarca!";
        }
        else if (ddlCidade.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Selecione uma comarca!";
        }
        else if (ddlClasse.SelectedItem.Text == "Selecione")
        {
            lbl.Text = "Seleciona uma classe para o processo!";
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
            }
            else
            {
                return true;
            }
        }
        divStatusPro.Attributes["class"] = "alert alert-danger";
        return false;
    }
    public void CarregaDetalhesProcesso(int id)
    {

        Recurso rec = new Recurso();
        Recurso recurso = new Recurso();
        RecursoDB recDB = new RecursoDB();
        Assunto assunto = new Assunto();
        AssuntoDB assuntoDB = new AssuntoDB();
        DataProcesso dataProcesso = new DataProcesso();
        DataProcessoDB dataDB = new DataProcessoDB();
        Movimentacao mov = new Movimentacao();
        MovimentacaoDB movDB = new MovimentacaoDB();
        Pessoa pes = new Pessoa();
        PessoaDB pesDB = new PessoaDB();
        ClienteFisico clifisico = new ClienteFisico();
        ClienteFisicoDB clifisicoDB = new ClienteFisicoDB();
        ClienteJuridico clijuridico = new ClienteJuridico();
        ClienteJuridicoDB clijuridicoDB = new ClienteJuridicoDB();
        Contato contato = new Contato();
        ContatoDB contatoDB = new ContatoDB();
        Advogado adv = new Advogado();
        Vara vara = new Vara();
        VaraDB varaDB = new VaraDB();
        PosicaoCliente pos = new PosicaoCliente();
        PosicaoClienteDB posDB = new PosicaoClienteDB();
        Cidade cid = new Cidade();
        CidadeDB cidDB = new CidadeDB();
        Estado est = new Estado();
        EstadoDB estDB = new EstadoDB();
        Classe cla = new Classe();
        ClasseDB claDB = new ClasseDB();
        Processo pro = new Processo();
        ProcessoDB proDB = new ProcessoDB();


        pro = proDB.Select(id);

        pes = pesDB.SelectGenerico(pro.PessoaCliente.Codigo);
        if (pes.Nivel == 3)
        {
            clifisico = clifisicoDB.Select(pes.Codigo);
            contato = contatoDB.SelectContato(clifisico.ContatoPessoa.Codigo);
            lblClienteDetalhes.Text = contato.Nome;

        }
        else if (pes.Nivel == 4)
        {
            clijuridico = clijuridicoDB.Select(pes.Codigo);
            contato = contatoDB.SelectContato(clijuridico.ContatoPessoa.Codigo);
            lblClienteDetalhes.Text = contato.Nome;
        }


        lblNumeroDetalhes.Text = pro.NumeroProcesso;
        lblDataCadastroDetalhes.Text = pro.DataCriacao.ToString("dd/MM/yyyy");
        lblObservacaoDetalhes.Text = pro.Observacao;
        lblDescricaoDetalhes.Text = pro.Descricao;


        assunto = assuntoDB.Select(pro.Assunto.Codigo);
        lblAssuntoDetalhes.Text = assunto.Descricao;

        mov = movDB.Select(pro.Movimentacao.Codigo);
        lblMovDetalhes.Text = mov.Descricao;

        vara = varaDB.Select(pro.Vara.Codigo);
        lblVaraDetalhes.Text = vara.Descricao;

        pos = posDB.Select(pro.PosicaoCliente.Codigo);
        lblPosicaoDetalhes.Text = pos.Descricao;

        cla = claDB.Select(pro.Classe.Codigo);
        lblClasseDetalhes.Text = cla.Descricao;

        dataProcesso = dataDB.SelectByProcesso(id);
        if (dataProcesso != null)
        {
            lblDataAudiencia.Text = dataProcesso.DataAudiencia.ToString("dd/MM/yyyy");
            lblHoraAudienciaDetalhes.Text = dataProcesso.DataAudiencia.ToString("t");
        }
        if (pro.Recurso != null)
        {
            lblRecursoDetalhes.Text = "Sim";
            recurso = recDB.Select(pro.Recurso.Codigo);
            rec = recDB.SelectCamara(recurso.CodigoCamara);
            lblCamaraDetalhes.Text = rec.Camara;
            rec = recDB.SelectTribunal(recurso.CodigoTribunal);
            lblTribunalDetalhes.Text = rec.Tribunal;
        }
        else
        {
            lblRecursoDetalhes.Text = "Não";
        }



        cid = cidDB.SelectCidadePessoa(pro.Comarca.Codigo);
        lblComarcaDetalhes.Text = cid.NomeCidade;
        est = estDB.SelectEstado(cid.Estado.Codigo);
        lblEstado.Text = est.Descricao;
    }
    protected void btnCancelarDetalhes_Click(object sender, EventArgs e)
    {
        modalDetalhes.Hide();
    }
    protected void gdvProcesso_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvProcesso.PageIndex = e.NewPageIndex;
        CarregaProcesso(Convert.ToInt32(Session["Advogado"]));
    }
    private void LimpaLBL()
    {
        lblClienteDetalhes.Text = string.Empty;
        lblClienteDetalhes.Text = string.Empty;
        lblNumeroDetalhes.Text = string.Empty;
        lblDataCadastroDetalhes.Text = string.Empty;
        lblObservacaoDetalhes.Text = string.Empty;
        lblDescricaoDetalhes.Text = string.Empty;
        lblAssuntoDetalhes.Text = string.Empty;
        lblMovDetalhes.Text = string.Empty;
        lblVaraDetalhes.Text = string.Empty;
        lblPosicaoDetalhes.Text = string.Empty;
        lblClasseDetalhes.Text = string.Empty;
        lblDataAudiencia.Text = string.Empty;
        lblHoraAudienciaDetalhes.Text = string.Empty;
        lblRecursoDetalhes.Text = string.Empty;
        lblCamaraDetalhes.Text = string.Empty;
        lblTribunalDetalhes.Text = string.Empty;
        lblRecursoDetalhes.Text = string.Empty;
        lblComarcaDetalhes.Text = string.Empty;
        lblEstado.Text = string.Empty;
    }
}