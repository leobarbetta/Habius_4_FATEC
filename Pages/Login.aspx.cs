using Habius.Classes.Administrativo;
using Habius.Persistencia.Administrativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Habius Software Juridico";
        txtLogin.Focus();
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Pessoa pes = new Pessoa();
        PessoaDB DB = new PessoaDB();

        pes = DB.Login(txtLogin.Text, txtLogin.Text, txtSenha.Text);

        if (pes != null)
        {
            if (pes.Nivel == 1)
            {

            }
            else if (pes.Nivel == 2)
            {
                Session["Advogado"] = pes.Codigo;
                Response.Redirect("../Pages/Administrativo/Advogado/HomeAdvogado.aspx");
            }
            else if (pes.Nivel == 3 || pes.Nivel == 4)
            {
                Session["Cliente"] = pes;
                Response.Redirect("../Pages/Administrativo/Cliente/HomeCliente.aspx");
            }
        }
        else
        {
            lblMensagem.Text = "Usuario ou Senha Invalido";
            divErro.Attributes["class"] = "alert alert-danger";
        }
    }
}