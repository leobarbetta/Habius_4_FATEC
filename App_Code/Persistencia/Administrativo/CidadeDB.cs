using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Habius.Classes.Administrativo;

namespace Habius.Persistencia.Administrativo
{
    public class CidadeDB
    {
        public DataSet SelectAll(int estado)
        {
            DataSet ds = new DataSet();
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM CID_CIDADE WHERE EST_CODIGO = ?estado ORDER BY CID_CIDADE;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?estado", estado));

            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objConexao.Dispose();
            objCommand.Dispose();

            return ds;
        }

        public Cidade SelectCidadePessoa(int codigo)
        {
            //teste = false;
            Cidade obj = null;
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT * FROM CID_CIDADE WHERE CID_CODIGO = ?codigo", objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                obj = new Cidade();

                obj.Codigo = Convert.ToInt32(objDataReader["cid_codigo"]);
                obj.NomeCidade = Convert.ToString(objDataReader["cid_CIDADE"]);
                Estado est = new Estado();
                est.Codigo = Convert.ToInt32(objDataReader["EST_CODIGO"]);
                obj.Estado = est;
            }

            objDataReader.Close();
            objConexao.Close();

            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;
        }

        public DataSet SelectAllByState(int idState)
        {
            DataSet ds = new DataSet();

            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataAdapter objDataAdapter;

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT * FROM CID_CIDADE WHERE EST_CODIGO=?codigo ORDER BY CID_CIDADE", objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", idState));

            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();

            objCommand.Dispose();
            objConexao.Dispose();

            return ds;
        }
        public bool Update(Cidade cidade)
        {
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            string sql = "UPDATE CID_CIDADE SET CID_CIDADE=?cidade, EST_CODIGO=?estado WHERE CID_CODIGO=?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            //CAMPOS QUE SERÃO ATUALIZADOS
            objCommand.Parameters.Add(Mapped.Parameter("?cidade", cidade.NomeCidade));
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", cidade.Codigo));
            Estado est = new Estado();
            objCommand.Parameters.Add(Mapped.Parameter("?estado", est.Codigo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
    }
}