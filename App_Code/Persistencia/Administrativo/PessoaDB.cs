using Habius.Classes.Administrativo;
using Habius.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Habius.Persistencia.Administrativo
{
    public class PessoaDB
    {
        public Pessoa Login(string email, string login, string senha)
        {
            Pessoa pes = null;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            senha = Criptografia.GetSHA256(senha);

            string sql = "SELECT P.PES_CODIGO, P.NIV_CODIGO FROM CON_CONTATO C INNER JOIN PES_PESSOA P ON (P.CON_CODIGO = C.CON_CODIGO) WHERE C.CON_EMAIL = ?email or P.PES_LOGIN = ?login and P.PES_SENHA = ?senha AND P.PES_ATIVO = 1;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            
            objCommand.Parameters.Add(Mapped.Parameter("?email", email));
            objCommand.Parameters.Add(Mapped.Parameter("?login", login));
            objCommand.Parameters.Add(Mapped.Parameter("?senha", senha));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                pes = new Pessoa();
                pes.Codigo = Convert.ToInt32(objDataReader["PES_CODIGO"]);
                pes.Nivel = Convert.ToInt32(objDataReader["NIV_CODIGO"]);
            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return pes;
        }
        public Pessoa SelectGenerico(int id)
        {
            Pessoa pes = null;
            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataReader objDataReader;

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT * FROM PES_PESSOA WHERE PES_CODIGO = ?codigo", objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", id));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                pes = new ClienteFisico();
                pes.Codigo = Convert.ToInt32(objDataReader["PES_CODIGO"]);
                pes.UserName = Convert.ToString(objDataReader["PES_LOGIN"]);
                pes.Nivel = Convert.ToInt32(objDataReader["NIV_CODIGO"]);
            }
            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            return pes;
        }
    }
}