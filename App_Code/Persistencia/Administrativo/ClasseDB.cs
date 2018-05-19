using Habius.Classes.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Habius.Persistencia.Administrativo
{
    public class ClasseDB
    {
        public DataSet SelectAllClasses()
        {
            DataSet ds = new DataSet();
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM CLA_CLASSE";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objConexao.Dispose();
            objCommand.Dispose();

            return ds;
        }

        public Classe Select(int codigo)
        {
            Classe cla = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM CLA_CLASSE WHERE CLA_CODIGO = ?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                cla = new Classe();
                cla.Codigo = Convert.ToInt32(objDataReader["CLA_CODIGO"]);
                cla.Descricao = Convert.ToString(objDataReader["CLA_CLASSE"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return cla;
        }
        public int SelectTotalGrafico(int adv, int classe, DateTime initialDate, DateTime finalDate)
        {
            int total = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT COUNT(*) AS TOTAL ";
            sql += "FROM PRO_PROCESSO PRO ";
            sql += "INNER JOIN APR_ADVOGADO_PROCESSO APR ON (PRO.PRO_CODIGO = APR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA ADV ON (ADV.PES_CODIGO = APR.PES_CODIGO) ";
            sql += "INNER JOIN CLA_CLASSE CLA ON (PRO.CLA_CODIGO = CLA.CLA_CODIGO) ";
            sql += "WHERE ADV.PES_CODIGO = ?adv AND CLA.CLA_CODIGO = ?classe AND PRO.PRO_DATACRIACAO Between ?initialdate and ?finaldate";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
            objCommand.Parameters.Add(Mapped.Parameter("?classe", classe));
            objCommand.Parameters.Add(Mapped.Parameter("?initialdate", initialDate));
            objCommand.Parameters.Add(Mapped.Parameter("?finaldate", finalDate));

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
        public int TotalClasse()
        {
            int total = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT COUNT(*) AS TOTAL FROM CLA_CLASSE";

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