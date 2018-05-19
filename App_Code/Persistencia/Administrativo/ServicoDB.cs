using Habius.Classes.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Habius.Persistencia.Administrativo
{
    public class ServicoDB
    {
        public DataSet SelectAll()
        {
            DataSet ds = new DataSet();

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM SEV_SERVICO;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objConexao.Dispose();
            objCommand.Dispose();

            return ds;
        }
        public bool Insert(Servico sev)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO SEV_SERVICO(SEV_SERVICO) VALUE (?servico);";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?servico", sev.Descricao));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public Servico GetLastId(string texto)
        {
            Servico sev = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM SEV_SERVICO WHERE SEV_SERVICO = ?servico";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?servico", texto));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                sev = new Servico();
                sev.Codigo = Convert.ToInt32(objDataReader["SEV_CODIGO"]);
            }

            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return sev;
        }
        public Servico Select(int codigo)
        {
            Servico sev = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM SEV_SERVICO WHERE SEV_CODIGO = ?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                sev = new Servico();
                sev.Codigo = Convert.ToInt32(objDataReader["SEV_CODIGO"]);
                sev.Descricao = Convert.ToString(objDataReader["SEV_SERVICO"]);
            }

            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return sev;
        }
        public int SelectTotalServico()
        {
            int total = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT COUNT(*) AS TOTAL FROM SEV_SERVICO";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                total = Convert.ToInt32(objDataReader["TOTAL"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return total;
        }
    }
}