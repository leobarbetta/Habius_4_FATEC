using Habius.Classes.Administrativo;
using Habius.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Habius.Persistencia.Administrativo
{
    public class DespesasDB
    {
        public bool Insert(Despesas despesas)
        {
            IDbConnection objConexao;
            IDbCommand objCommand;
            string sql = "INSERT INTO DES_DESPESA(DES_DATA, DES_VALOR, DES_OBS, TID_CODIGO, PES_CODIGO_ADV, PRO_CODIGO) VALUES (?data, ?valor, ?obs, ?codtid, ?adv, ?processo)";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?data", despesas.Data));
            objCommand.Parameters.Add(Mapped.Parameter("?valor", despesas.Valor));
            objCommand.Parameters.Add(Mapped.Parameter("?obs", despesas.Obs));
            objCommand.Parameters.Add(Mapped.Parameter("codtid", despesas.TipoDespesa.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?adv", despesas.PesCodigo.Codigo));
            if (despesas.Processo == null)
            {
                objCommand.Parameters.Add(Mapped.Parameter("?processo", null));
            }
            else
            {
                objCommand.Parameters.Add(Mapped.Parameter("?processo", despesas.Processo.Codigo));
            }
            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
        public DataSet SelectAllCustoProcesso(int processo)
        {
            DataSet ds = new DataSet();
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM DES_DESPESA D INNER JOIN TID_TIPODESPESA T ON (D.TID_CODIGO = T.TID_CODIGO) WHERE D.PRO_CODIGO = ?processo ORDER BY DES_DATA DESC;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?processo", processo));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return ds;
        }
        public double TotalCustoProcesso(int processo)
        {
            double total = 0;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT SUM(DES_VALOR) AS TOTAL FROM DES_DESPESA WHERE PRO_CODIGO = ?processo;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?processo", processo));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                if (objDataReader["TOTAL"] != DBNull.Value)
                {
                    total = Convert.ToDouble(objDataReader["TOTAL"]);
                }
            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return total;
        }
        public double TotalDespesaEscritorio(int adv, int mes)
        {
            double total = 0;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT SUM(DES_VALOR) AS TOTAL FROM DES_DESPESA D LEFT JOIN TID_TIPODESPESA T ON (D.TID_CODIGO = T.TID_CODIGO) WHERE T.TID_CATEGORIA =1 AND D.PES_CODIGO_ADV = ?adv AND month(D.DES_DATA) =?mes ORDER BY D.DES_DATA";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
            objCommand.Parameters.Add(Mapped.Parameter("?mes", mes));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                if (objDataReader["TOTAL"] != DBNull.Value)
                {
                    total = Convert.ToDouble(objDataReader["TOTAL"]);
                }
            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return total;
        }
        public DataSet SelectAllDespesasEscritorio(int adv, int mes)
        {
            DataSet ds = new DataSet();
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM DES_DESPESA D LEFT JOIN TID_TIPODESPESA T ON (D.TID_CODIGO = T.TID_CODIGO) WHERE T.TID_CATEGORIA =1 AND D.PES_CODIGO_ADV = ?adv AND month(D.DES_DATA) =?mes ORDER BY D.DES_DATA";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
            objCommand.Parameters.Add(Mapped.Parameter("?mes", mes));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return ds;
        }
        public DataSet SelectAllDespesasEscritorioByDate(int adv, DateTime initialdate, DateTime finaldate)
        {
            DataSet ds = new DataSet();
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM DES_DESPESA D LEFT JOIN TID_TIPODESPESA T ON (D.TID_CODIGO = T.TID_CODIGO) WHERE T.TID_CATEGORIA =1 AND D.PES_CODIGO_ADV = ?adv AND D.DES_DATA Between ?initialdate and ?finaldate ORDER BY D.DES_DATA";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
            objCommand.Parameters.Add(Mapped.Parameter("?initialdate", initialdate));
            objCommand.Parameters.Add(Mapped.Parameter("?finaldate", finaldate));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return ds;
        }
        public double GetTotalDespesaEscritorio(int adv, int mes)
        {
            double total = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT SUM(D.DES_VALOR) AS TOTAL FROM DES_DESPESA D LEFT JOIN TID_TIPODESPESA T ON (D.TID_CODIGO = T.TID_CODIGO) WHERE T.TID_CATEGORIA =1 AND D.PES_CODIGO_ADV = ?adv AND month(D.DES_DATA) = ?mes ORDER BY D.DES_DATA";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
            objCommand.Parameters.Add(Mapped.Parameter("?mes", mes));

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
        public double GetTotalDespesaEscritorioByDate(int adv, DateTime initialdate, DateTime finaldate)
        {
            double total = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT SUM(D.DES_VALOR) AS TOTAL FROM DES_DESPESA D LEFT JOIN TID_TIPODESPESA T ON (D.TID_CODIGO = T.TID_CODIGO) WHERE T.TID_CATEGORIA =1 AND D.PES_CODIGO_ADV = ?adv AND D.DES_DATA Between ?initialdate and ?finaldate ORDER BY D.DES_DATA";            

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?initialdate", initialdate));
            objCommand.Parameters.Add(Mapped.Parameter("?finaldate", finaldate));
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));

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
        public double GetTotalDespesaEscritorioByDate(int adv, int tipo ,DateTime initialdate, DateTime finaldate)
        {
            double total = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT SUM(D.DES_VALOR) AS TOTAL FROM DES_DESPESA D INNER JOIN TID_TIPODESPESA T ON (D.TID_CODIGO = T.TID_CODIGO) WHERE D.PES_CODIGO_ADV = ?adv AND T.TID_CODIGO = ?tipo AND D.DES_DATA BETWEEN ?initialdate and ?finaldate";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
            objCommand.Parameters.Add(Mapped.Parameter("?tipo", tipo));
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

        public Despesas Select(int codigo)
        {
            Despesas despesa = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM DES_DESPESA WHERE DES_CODIGO = ?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                despesa = new Despesas();
                despesa.Codigo = Convert.ToInt32(objDataReader["DES_CODIGO"]);
                despesa.Valor = Convert.ToDecimal(objDataReader["DES_VALOR"]);
                despesa.Data = Convert.ToDateTime(objDataReader["DES_DATA"]);
                despesa.Obs = Convert.ToString(objDataReader["DES_OBS"]);

                TipoDespesa tipo = new TipoDespesa();
                tipo.Codigo = Convert.ToInt32(objDataReader["TID_CODIGO"]);
                despesa.TipoDespesa = tipo;
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return despesa;
        }

        public bool Update(Despesas despesa)
        {
            IDbConnection objConexao;
            IDbCommand objCommand;
            string sql = "UPDATE DES_DESPESA SET DES_VALOR =?valor, DES_data=?data, DES_OBS=?obs, TID_CODIGO=?tipo WHERE DES_CODIGO = ?codigo;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?valor", despesa.Valor));
            objCommand.Parameters.Add(Mapped.Parameter("?data", despesa.Data));
            objCommand.Parameters.Add(Mapped.Parameter("?obs", despesa.Obs));
            objCommand.Parameters.Add(Mapped.Parameter("?tipo", despesa.TipoDespesa.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", despesa.Codigo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
    }
}