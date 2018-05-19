using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Habius.Classes.Administrativo;

namespace Habius.Persistencia.Administrativo
{
    public class EstadoDB
    {
        public DataSet SelectAll()
        {
            DataSet ds = new DataSet();
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM EST_ESTADO ORDER BY EST_ESTADO";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objConexao.Dispose();
            objCommand.Dispose();

            return ds;
        }

        public Estado SelectEstado(int codigo)
        {
            //teste = false;
            Estado obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT * FROM EST_ESTADO WHERE EST_CODIGO = ?codigo", objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                obj = new Estado();

                obj.Codigo = Convert.ToInt32(objDataReader["EST_CODIGO"]);
                obj.Sigla = Convert.ToString(objDataReader["EST_SIGLA"]);
                obj.Descricao = Convert.ToString(objDataReader["EST_ESTADO"]);
            }

            objDataReader.Close();
            objConexao.Close();

            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;

        }
    }
}