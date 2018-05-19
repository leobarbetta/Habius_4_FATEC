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

public partial class Pages_Administrativo_Cliente_HomeCliente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Cliente"] != null)
        {
            Master.Page.Title = "Home Page";
            CarregaProcesso();
        }
    }
    private void CarregaProcesso()
    {
        ProcessoDB proDB = new ProcessoDB();
        Pessoa pes = (Pessoa)Session["Cliente"];
        DataSet ds = proDB.SelectAllByCliente(pes.Codigo);
        Function.CarregaGrid(ds, gdvProcesso, lbl);
    }
    protected void gdvProcesso_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Processo processo = new Processo();
        ProcessoDB ageDB = new ProcessoDB();
        switch (e.CommandName)
        {
            case "Detalhes":
                CarregaDetalhesProcesso(Convert.ToInt32(e.CommandArgument));
                modalDetalhes.Show();
                break;
        }
    }
    protected void gdvProcesso_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DateTime data = new DateTime();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                data = Convert.ToDateTime(e.Row.Cells[1].Text);
            }
            catch (FormatException)
            {

            }
            finally
            {
                if (data < DateTime.Today)
                {
                    e.Row.BackColor = System.Drawing.Color.IndianRed;
                }
                else if (data == DateTime.Today)
                {
                    e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
                }
            }
        }
    }
    protected void gdvProcesso_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvProcesso.PageIndex = e.NewPageIndex;
        CarregaProcesso();
    }
    private void LimpaLBL()
    {
        lblAdvogadoCliente.Text = string.Empty;
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
        AdvogadoDB advDB = new AdvogadoDB();

        pro = proDB.Select(id);

        adv = advDB.Select(pro.PessoaAdvogado.Codigo);

        contato = contatoDB.SelectContato(adv.ContatoPessoa.Codigo);
        lblAdvogadoCliente.Text = contato.Nome;


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
        LimpaLBL();
    }
}