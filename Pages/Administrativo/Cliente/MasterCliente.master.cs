using Habius.Classes.Administrativo;
using Habius.Persistencia.Administrativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Administrativo_MasterCliente : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Cliente"] != null)
        //{
        //    Pessoa pes = new Pessoa();
        //    PessoaDB pesDB = new PessoaDB();
        //    Pessoa pessoa = (Pessoa)Session["Cliente"];
        //    pes = pesDB.SelectGenerico(pessoa.Codigo);
        //    lblUsuarioAtivo.Text = pes.UserName;
        //}
        //else
        //{
        //    Response.Redirect("../../Login.aspx");
        //}
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Session.Abandon();
        Session.Clear();
        Session.RemoveAll();
        Response.ClearHeaders();
        Response.Redirect("../../../Index.html");
    }
}
