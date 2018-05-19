using Habius.Classes.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Habius.Persistencia.Administrativo
{
    public class DataProcessoDB
    {
        public bool Insert(DataProcesso data)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO DAP_DATAPROCESSO(DAP_DATAAUDIENCIA, PRO_CODIGO) VALUE(?data, ?processo);";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?data", data.DataAudiencia));
            objCommand.Parameters.Add(Mapped.Parameter("?processo", data.Processo.Codigo));
          
            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public bool Update(DataProcesso data)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "UPDATE DAP_DATAPROCESSO SET DAP_DATAAUDIENCIA=?data, PRO_CODIGO=?processo WHERE PRO_CODIGO=?processo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?data", data.DataAudiencia));
            objCommand.Parameters.Add(Mapped.Parameter("?processo", data.Processo.Codigo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public bool Delete(int codigo)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;
            string sql = "DELETE FROM DARP_DATAPROCESSO WHERE PRO_CODIGO = ?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public DataProcesso SelectByProcesso(int codigo)
        {
            DataProcesso data = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM DAP_DATAPROCESSO WHERE PRO_CODIGO = ?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                data = new DataProcesso();
                data.Codigo = Convert.ToInt32(objDataReader["DAP_CODIGO"]);
                data.DataAudiencia = Convert.ToDateTime(objDataReader["DAP_DATAAUDIENCIA"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return data;
        }
    }
}