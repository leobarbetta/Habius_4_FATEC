using Habius.Classes.Administrativo;
using Habius.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace Habius.Persistencia.Administrativo
{
    public class EstadoCivilDB
    {
        public DataSet SelecAllEstadoCivil()
        {
            DataSet ds = new DataSet();
            IDbCommand objCommand;
            IDbConnection objconexao;
            IDataAdapter objDataAdapter;
            string sql = "SELECT * FROM ECI_ESTADOCIVIL ORDER BY ECI_DESCRICAO;";

            objconexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objconexao);
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);
            objconexao.Close();
            objCommand.Dispose();
            objconexao.Dispose();
            return ds;
        }
        public EstadoCivil Select(int id)
        {
            EstadoCivil estCivil = null;

            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataReader objDataReader;

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT * FROM ECI_ESTADOCIVIL WHERE ECI_CODIGO = ?codigo", objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", id));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                estCivil = new EstadoCivil();
                estCivil.Descricao = Convert.ToString(objDataReader["ECI_DESCRICAO"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return estCivil;
        }
    }
}