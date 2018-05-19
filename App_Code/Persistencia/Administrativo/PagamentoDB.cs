using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Habius.Classes.Administrativo;

namespace Habius.Persistencia.Administrativo
{
    public class PagamentoDB
    {
        public bool Insert(Pagamento pag)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO PAG_PAGAMENTO(PAG_DATAPAGAMENTO, PAG_VALOR, PES_CODIGO_ADV, PRO_CODIGO, PES_CODIGO_CLI, SEV_CODIGO) VALUE(?data, ?valor, ?advogado, ?processo, ?cliente, ?servico);";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?data", pag.DataPagamento));
            objCommand.Parameters.Add(Mapped.Parameter("?valor", pag.Valor));
            objCommand.Parameters.Add(Mapped.Parameter("?advogado", pag.Advogado.Codigo));
            if (pag.Processo == null)
            {
                objCommand.Parameters.Add(Mapped.Parameter("?processo", null));
            }
            else
            {
                objCommand.Parameters.Add(Mapped.Parameter("?processo", pag.Processo.Codigo));
            }
            objCommand.Parameters.Add(Mapped.Parameter("?cliente", pag.Pes_cliente.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?servico", pag.Servico.Codigo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objCommand.Dispose();

            return true;
        }
        public DataSet SelectAllPagamentosByCliente(int cliente)
        {
            DataSet ds = new DataSet();
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM PAG_PAGAMENTO P INNER JOIN SEV_SERVICO S ON(P.SEV_CODIGO = S.SEV_CODIGO) LEFT JOIN PRO_PROCESSO PRO ON (PRO.PRO_CODIGO = P.PRO_CODIGO) WHERE PES_CODIGO_CLI = ?cliente ORDER BY P.PAG_DATAPAGAMENTO DESC";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?cliente", cliente));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objConexao.Dispose();
            objCommand.Dispose();

            return ds;
        }
        public DataSet SelectAllPagamentos(int adv, DateTime initialdate, DateTime finaldate)
        {
            DataSet ds = new DataSet();
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM PAG_PAGAMENTO P INNER JOIN SEV_SERVICO S ON(P.SEV_CODIGO = S.SEV_CODIGO) LEFT JOIN PRO_PROCESSO PRO ON (PRO.PRO_CODIGO = P.PRO_CODIGO) WHERE P.PES_CODIGO_ADV = ?adv AND P.PAG_DATAPAGAMENTO Between ?initialdate and ?finaldate";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?initialdate", initialdate));
            objCommand.Parameters.Add(Mapped.Parameter("?finaldate", finaldate));
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objConexao.Dispose();
            objCommand.Dispose();

            return ds;
        }
        public double TotalPagamentoMes(int adv, int mes)
        {
            double total = 0;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT SUM(P.PAG_VALOR) AS TOTAL FROM PAG_PAGAMENTO P INNER JOIN SEV_SERVICO S ON(P.SEV_CODIGO = S.SEV_CODIGO) LEFT JOIN PRO_PROCESSO PRO ON (PRO.PRO_CODIGO = P.PRO_CODIGO) WHERE MONTH(P.PAG_DATAPAGAMENTO) = ?mes AND P.PES_CODIGO_ADV = ?adv";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?mes", mes));
         
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                if (objDataReader["TOTAL"] != DBNull.Value)
                    total = Convert.ToDouble(objDataReader["TOTAL"]);

            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return total;
        }
        public double TotalPagamentoByDate(int adv, DateTime initialdate, DateTime finaldate)
        {
            double total = 0;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT SUM(P.PAG_VALOR) AS TOTAL FROM PAG_PAGAMENTO P INNER JOIN SEV_SERVICO S ON(P.SEV_CODIGO = S.SEV_CODIGO) LEFT JOIN PRO_PROCESSO PRO ON (PRO.PRO_CODIGO = P.PRO_CODIGO) WHERE P.PES_CODIGO_ADV = ?adv AND P.PAG_DATAPAGAMENTO Between ?initialdate and ?finaldate";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?initialdate", initialdate));
            objCommand.Parameters.Add(Mapped.Parameter("?finaldate", finaldate));
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                if (objDataReader["TOTAL"] != DBNull.Value)
                    total = Convert.ToDouble(objDataReader["TOTAL"]);

            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return total;
        }
        public double TotalPagamentoCliente(int cliente)
        {
            double total = 0;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT SUM(PAG_VALOR) AS TOTAL FROM PAG_PAGAMENTO WHERE PES_CODIGO_CLI = ?cliente";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?cliente", cliente));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                if (objDataReader["TOTAL"] != DBNull.Value)
                    total = Convert.ToDouble(objDataReader["TOTAL"]);

            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return total;
        }
        public double GetTotalPagamentoByDate(int adv, int servico, DateTime initialdate, DateTime finaldate)
        {
            double total = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT SUM(P.PAG_VALOR) AS TOTAL FROM PAG_PAGAMENTO P INNER JOIN SEV_SERVICO S ON (S.SEV_CODIGO = P.SEV_CODIGO) WHERE P.PES_CODIGO_ADV = ?adv AND P.SEV_CODIGO = ?servico AND P.PAG_DATAPAGAMENTO Between ?initialdate and ?finaldate";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
            objCommand.Parameters.Add(Mapped.Parameter("?servico", servico));
            objCommand.Parameters.Add(Mapped.Parameter("?initialdate", initialdate));
            objCommand.Parameters.Add(Mapped.Parameter("?finaldate", finaldate));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                if (objDataReader["TOTAL"] != DBNull.Value)
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