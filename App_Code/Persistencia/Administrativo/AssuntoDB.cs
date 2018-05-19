using Habius.Classes.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Habius.Persistencia.Administrativo
{
    public class AssuntoDB
    {
        public bool Insert(Assunto assunto)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO ASS_ASSUNTO(ASS_ASSUNTO) VALUE(?assunto)";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?assunto", assunto.Descricao));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
        public bool Update(Assunto assunto)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "UPDATE ASS_ASSUNTO SET ASS_ASSUNTO=?assunto WHERE ASS_CODIGO=?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?assunto", assunto.Descricao));
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", assunto.Codigo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
        public int GetLastId(Assunto ass)
        {
            int codigo = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM ASS_ASSUNTO WHERE ASS_ASSUNTO = ?assunto";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?assunto", ass.Descricao));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                codigo = Convert.ToInt32(objDataReader["ASS_CODIGO"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return codigo;
        }
        public Assunto Select(int codigo)
        {
            Assunto ass = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM ASS_ASSUNTO WHERE ASS_CODIGO = ?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                ass = new Assunto();
                ass.Codigo = Convert.ToInt32(objDataReader["ASS_CODIGO"]);
                ass.Descricao = Convert.ToString(objDataReader["ASS_ASSUNTO"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return ass;
        }
    }
}