using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Error_Erro404 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        if (Session["Advogado"] != null)
        {
            Response.Redirect("../../../Pages/Administrativo/Advogado/HomeAdvogado.aspx");
        }
        else if (Session["Cliente"] != null)
        {
            Response.Redirect("../../../Pages/Administrativo/Cliente/HomeCliente.aspx");
        }
        else
        {
            Response.Redirect("../../../Default.aspx");
        }
    }
}