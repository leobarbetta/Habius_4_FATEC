using Habius.Classes.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Habius.Persistencia.Administrativo
{
    public class MovimentacaoDB
    {
        public DataSet SelectAllMovimentacao()
        {
            DataSet ds = new DataSet();
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM MOV_MOVIMENTACAO";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objConexao.Dispose();
            objCommand.Dispose();

            return ds;
        }
        public bool Insert(Movimentacao mov)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO PMV_PROCESSO_MOVIMENTACAO(PRO_CODIGO, MOV_CODIGO, PMV_DATA_MOVIMENTACAO) VALUE(?processo, ?movimentacao, ?data);";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?processo", mov.Processo.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?movimentacao", mov.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?data", mov.DataMovimentacao));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public bool Finaliza(int codigo)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "UPDATE PMV_PROCESSO_MOVIMENTACAO SET PMV_FINALIZADO = 1 WHERE PMV_CODIGO = ?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));
            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public Movimentacao Select(int codigo)
        {
            Movimentacao mov = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM MOV_MOVIMENTACAO WHERE MOV_CODIGO = ?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                mov = new Movimentacao();
                mov.Codigo = Convert.ToInt32(objDataReader["MOV_CODIGO"]);
                mov.Descricao = Convert.ToString(objDataReader["MOV_MOVIMENTACAO"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return mov;
        }
        public int TotalMovimentacao()
        {
            int total = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT COUNT(*) AS TOTAL FROM MOV_MOVIMENTACAO";

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