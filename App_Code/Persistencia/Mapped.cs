using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
 
namespace Habius.Persistencia
{
    public class Mapped
    {
        
        public static IDbConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["strConexao"]);
            conn.Open();
            return conn;
        }

        public static IDbCommand Command(string query, IDbConnection conexao)
        {
            IDbCommand comando = conexao.CreateCommand();
            comando.CommandText = query;
            return comando;
        }

        public static IDataAdapter Adapter(IDbCommand comando)
        {
            IDbDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = comando;
            return adap;
        }
 
        public static IDbDataParameter Parameter(string nome, object valor)
        {
            return new MySqlParameter(nome, valor);
        
        }
    }
}