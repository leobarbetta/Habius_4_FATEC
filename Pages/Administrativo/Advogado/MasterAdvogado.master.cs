using Habius.Classes.Administrativo;
using Habius.Persistencia.Administrativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Administrativo_MasterAdvogado : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Advogado"] != null)
        //{
        //    Advogado adv = new Advogado();
        //    AdvogadoDB advDB = new AdvogadoDB();
        //    adv = advDB.Select(Convert.ToInt32(Session["Advogado"]));
        //    lblUsuarioAtivo.Text = adv.UserName;
        //    // fazer select para retornar nome do usuario
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
