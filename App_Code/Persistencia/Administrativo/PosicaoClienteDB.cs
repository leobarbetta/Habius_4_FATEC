using Habius.Classes.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Habius.Persistencia.Administrativo
{
    public class PosicaoClienteDB
    {
        public DataSet SelectAllPosicao()
        {
            DataSet ds = new DataSet();
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM POS_POSICAOCLIENTE";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objConexao.Dispose();
            objCommand.Dispose();

            return ds;
        }

        public PosicaoCliente Select(int codigo)
        {
            PosicaoCliente pos = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM POS_POSICAOCLIENTE WHERE POS_CODIGO = ?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                pos = new PosicaoCliente();
                pos.Codigo = Convert.ToInt32(objDataReader["POS_CODIGO"]);
                pos.Descricao = Convert.ToString(objDataReader["POS_POSICAO"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return pos;
        }
        public int SelectTotalGrafico(int adv, int posicao, DateTime initialDate, DateTime finalDate)
        {
            int total = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT COUNT(*) AS TOTAL ";
            sql += "FROM PRO_PROCESSO PRO ";
            sql += "INNER JOIN APR_ADVOGADO_PROCESSO APR ON (PRO.PRO_CODIGO = APR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA ADV ON (ADV.PES_CODIGO = APR.PES_CODIGO) ";
            sql += "INNER JOIN POS_POSICAOCLIENTE POS ON (PRO.POS_CODIGO = POS.POS_CODIGO) ";
            sql += "WHERE ADV.PES_CODIGO = ?adv AND POS.POS_CODIGO = ?posicao AND PRO.PRO_DATACRIACAO Between ?initialdate and ?finaldate";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
            objCommand.Parameters.Add(Mapped.Parameter("?posicao", posicao));
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
        public int TotalPosicao()
        {
            int total = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT COUNT(*) AS TOTAL FROM POS_POSICAOCLIENTE";

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