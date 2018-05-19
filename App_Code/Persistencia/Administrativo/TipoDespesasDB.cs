using Habius.Classes.Administrativo;
using Habius.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Habius.Persistencia.Administrativo
{
    public class TipoDespesasDB
    {
        public DataSet SelectAllDDLCategoria()
        {
            DataSet ds = new DataSet();
            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM TID_TIPODESPESA WHERE TID_CATEGORIA <> 2;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return ds;
        }
        public DataSet SelectAllDDLCategoriaProcesso()
        {
            DataSet ds = new DataSet();
            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM TID_TIPODESPESA WHERE TID_CATEGORIA <> 1;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return ds;
        }
        public bool Insert(string descricao)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO TID_TIPODESPESA(TID_CATEGORIA, TID_TIPODESPESA) VALUE (?categoria, ?descricao);";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?categoria", 1));
            objCommand.Parameters.Add(Mapped.Parameter("?descricao", descricao));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public int GetLastId(string descricao)
        {
            int codigo = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM TID_TIPODESPESA WHERE TID_TIPODESPESA = ?descricao ORDER BY TID_CODIGO DESC LIMIT 1";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?descricao", descricao));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                codigo = Convert.ToInt32(objDataReader["TID_CODIGO"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return codigo;
        }
        public bool InsertCustoProcesso(string descricao)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO TID_TIPODESPESA(TID_CATEGORIA, TID_TIPODESPESA) VALUE (?categoria, ?descricao);";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?categoria", 2));
            objCommand.Parameters.Add(Mapped.Parameter("?descricao", descricao));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public TipoDespesa SelectTipoDespesaEscritorio(int codigo)
        {
            TipoDespesa tipoDespesa = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM TID_TIPODESPESA WHERE TID_CODIGO = ?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {

                tipoDespesa = new TipoDespesa();
                tipoDespesa.Codigo = Convert.ToInt32(objDataReader["TID_CODIGO"]);
                tipoDespesa.Categoria = Convert.ToInt32(objDataReader["TID_CATEGORIA"]);
                tipoDespesa.Descricao = Convert.ToString(objDataReader["TID_TIPODESPESA"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return tipoDespesa;
        }
        public int SelectTotalTipoDespesaEscritorio()
        {
            int total = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT COUNT(*) AS TOTAL FROM TID_TIPODESPESA";

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