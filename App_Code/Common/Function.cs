using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Habius.Common
{
    public class Function
    {
        #region Quantidade de Registros
        public static int QuantidadeRegistro(DataSet ds)
        {
            int qtd = 0;
            qtd = ds.Tables[0].Rows.Count;
            return qtd;
        }
        #endregion

        #region Carrega GridView
        public static void CarregaGrid(DataSet ds, GridView gdv, Label lbl)
        {
            int qtd = ds.Tables[0].Rows.Count;
            if (qtd > 0)
            {
                gdv.DataSource = ds.Tables[0].DefaultView;
                gdv.DataBind();
                lbl.Text = "Foram encontrado(s) " + qtd + " Registro(s)";
                gdv.Visible = true;
            }
            else
            {
                lbl.Text = "Não foram encontrados registros";
                gdv.Visible = false;
            }
        }
        #endregion

        #region Carrega DropDownList
        public static bool CarregaDDL(DataSet ds, DropDownList ddl, string valor, string texto)
        {
            int qtd = ds.Tables[0].Rows.Count;

            ddl.DataSource = ds.Tables[0].DefaultView;
            ddl.DataValueField = valor;
            ddl.DataTextField = texto;
            ddl.DataBind();
            ddl.Items.Insert(0, "Selecione");
            return true;
        }
        #endregion

        #region Carrega Item DDL By Codigo
        public static void CarregaItemDDLByCodigo(DropDownList ddl, int id)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                ddl.Items[i].Selected = false;
            }

            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (ddl.Items[i].Text != "Selecione")
                {
                    if (Convert.ToInt32(ddl.Items[i].Value) == id)
                    {
                        ddl.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }
        #endregion
        #region Carrega Item DDL By String
        public static void CarregaItemDDLByTexto(DropDownList ddl, string texto)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                ddl.Items[i].Selected = false;
            }

            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (ddl.Items[i].Text != "Selecione")
                {
                    if (ddl.Items[i].Text == texto)
                    {
                        ddl.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }
        #endregion
        #region Limpa DropDownList
        public static void LimpaDDL(DropDownList ddl)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                ddl.Items[i].Selected = false;
            }
        }
        #endregion
        #region Limpa TextBox
        public static void LimparTxt(Control controles)
        {
            foreach (Control ctl in controles.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = string.Empty;
                }
                if (ctl.HasControls())
                    LimparTxt(ctl);
            }
        }
        #endregion
    }
}