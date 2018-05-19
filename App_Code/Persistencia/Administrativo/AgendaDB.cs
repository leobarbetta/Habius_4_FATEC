using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Habius.Persistencia;
using Habius.Classes.Administrativo;

namespace Habius.Persistencia.Administrativo
{
    public class AgendaDB
    {
        public bool Insert(Agenda agenda)
        {
            IDbConnection objConexao;
            IDbCommand objCommand;
            string sql = "INSERT INTO AGE_AGENDA(PES_CODIGO_ADV, AGE_TITULO, AGE_DESCRICAO, AGE_DATACRIACAO, AGE_DATAFINALIZACAO) VALUES (?codigoadv, ?titulo, ?descricao, ?datacriacao, ?datafinalizacao)";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?codigoadv", agenda.PessoaAdvogado.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?titulo", agenda.Titulo));
            objCommand.Parameters.Add(Mapped.Parameter("?descricao", agenda.Descricao));
            objCommand.Parameters.Add(Mapped.Parameter("?datacriacao", agenda.DataCriacao = DateTime.Now));
            objCommand.Parameters.Add(Mapped.Parameter("?datafinalizacao", agenda.DataFinalizacao));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        //selectall
        public DataSet SelectAll(int codigo)
        {
            DataSet ds = new DataSet();

            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataAdapter objDataAdapter;

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT * FROM AGE_AGENDA A where A.PES_CODIGO_ADV = ?codigo AND ISNULL(A.AGE_FINALIZADO) ORDER BY AGE_DATAFINALIZACAO;", objConexao);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));
            objDataAdapter.Fill(ds);

            objConexao.Close();

            objCommand.Dispose();
            objConexao.Dispose();

            return ds;
        }
        public bool Finaliza(Agenda agenda)
        {
            IDbConnection objConexao;
            IDbCommand objCommand;
            string sql = "UPDATE AGE_AGENDA SET AGE_FINALIZADO = ?finalizado WHERE AGE_CODIGO = ?codigo";
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?finalizado", agenda.Finalizado));
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", agenda.Codigo));
            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public Agenda Select(int codigo)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            Agenda age = null;

            string sql = "SELECT * FROM AGE_AGENDA WHERE AGE_CODIGO = ?codigo";
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                age = new Agenda();

                age.Codigo = Convert.ToInt32(objDataReader["AGE_CODIGO"]);
                age.DataFinalizacao = Convert.ToDateTime(objDataReader["AGE_DATAFINALIZACAO"]);
                age.Descricao = Convert.ToString(objDataReader["AGE_DESCRICAO"]);
                age.Titulo = Convert.ToString(objDataReader["AGE_TITULO"]);
            }
            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return age;
        }
        public bool Update(Agenda agenda)
        {
            IDbConnection objConexao;
            IDbCommand objCommand;
            string sql = "UPDATE AGE_AGENDA SET AGE_TITULO =?titulo, AGE_DESCRICAO=?descricao, AGE_DATAFINALIZACAO=?datafinalizacao WHERE AGE_CODIGO = ?codigo;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?titulo", agenda.Titulo));
            objCommand.Parameters.Add(Mapped.Parameter("?descricao", agenda.Descricao));
            objCommand.Parameters.Add(Mapped.Parameter("?datafinalizacao", agenda.DataFinalizacao));
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", agenda.Codigo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
    }
}