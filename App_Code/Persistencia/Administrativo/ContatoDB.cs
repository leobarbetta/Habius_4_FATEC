using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Habius.Persistencia;
using Habius.Classes.Administrativo;

namespace Habius.Persistencia.Administrativo
{
    public class ContatoDB
    {
        public bool InsertSemFK(Contato con)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO CON_CONTATO(CON_NOME, CON_CELULAR, CON_TELEFONE, CON_EMAIL) VALUE(?nome, ?celular, ?telefone, ?email);";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?nome", con.Nome));
            objCommand.Parameters.Add(Mapped.Parameter("?celular", con.Celular));
            objCommand.Parameters.Add(Mapped.Parameter("?telefone", con.Telefone));
            objCommand.Parameters.Add(Mapped.Parameter("?email", con.Email));
        
            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
        public int GetLastId(Contato con)
        {
            int codigo = 0;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM CON_CONTATO WHERE CON_NOME = ?nome AND CON_EMAIL = ?email AND CON_CELULAR = ?celular AND CON_TELEFONE = ?telefone ORDER BY CON_CODIGO DESC LIMIT 1;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?nome", con.Nome));
            objCommand.Parameters.Add(Mapped.Parameter("?email", con.Email));
            objCommand.Parameters.Add(Mapped.Parameter("?celular", con.Celular));
            objCommand.Parameters.Add(Mapped.Parameter("?telefone", con.Telefone));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                codigo = Convert.ToInt32(objDataReader["CON_CODIGO"]);
            }

            objDataReader.Close();
            objConexao.Close();

            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return codigo;
        }
        public Contato ValidaEmail(string email)
        {
            Contato con = null;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT C.CON_EMAIL FROM CON_CONTATO C WHERE C.CON_EMAIL = ?email";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?email", email));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                con = new Contato();
                con.Email = Convert.ToString(objDataReader["CON_EMAIL"]);
            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return con;
        }
        public bool UpdateCodigoAdvogado(Contato con)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "UPDATE CON_CONTATO SET PES_CODIGO = ?pessoa WHERE CON_CODIGO =?codigo;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?pessoa", con.PessoaAdvogado.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", con.Codigo));
            
            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
        public bool InsertComFK(Contato con)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO CON_CONTATO(CON_NOME, CON_CELULAR, CON_TELEFONE, CON_EMAIL, PES_CODIGO) VALUE(?nome, ?celular, ?telefone, ?email, ?advogado);";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?nome", con.Nome));
            objCommand.Parameters.Add(Mapped.Parameter("?celular", con.Celular));
            objCommand.Parameters.Add(Mapped.Parameter("?telefone", con.Telefone));
            objCommand.Parameters.Add(Mapped.Parameter("?email", con.Email));
            objCommand.Parameters.Add(Mapped.Parameter("?advogado", con.PessoaAdvogado.Codigo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }

        public Contato SelectContato(int id)
        {
            Contato obj = null;
            //CRIAÇÃO DAS VARIAVEIS 
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            //ABRE CONEXÃO
            objConexao = Mapped.Connection();
            //DEFINE SQL QUE SERÁ EXECUTADA
            objCommand = Mapped.Command("SELECT * FROM CON_CONTATO WHERE CON_CODIGO = ?codigo", objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", id));
            //EXECUTA COMANDO NO BD
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                obj = new Contato();
                obj.Codigo = Convert.ToInt32(objDataReader["CON_CODIGO"]);
                obj.Nome = Convert.ToString(objDataReader["CON_NOME"]);
                obj.Telefone = Convert.ToString(objDataReader["CON_TELEFONE"]);
                obj.Celular = Convert.ToString(objDataReader["CON_CELULAR"]);
                obj.Email = Convert.ToString(objDataReader["CON_EMAIL"]);


            }

            objDataReader.Close();
            objConexao.Close();

            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;
        }

        public bool Update(Contato contato)
        {
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            string sql = "UPDATE CON_CONTATO SET CON_NOME=?nome, CON_CELULAR=?celular, CON_TELEFONE=?telefone, CON_EMAIL=?email WHERE CON_CODIGO=?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            //CAMPOS QUE SERÃO ATUALIZADOS
            objCommand.Parameters.Add(Mapped.Parameter("?nome", contato.Nome));
            objCommand.Parameters.Add(Mapped.Parameter("?celular", contato.Celular));
            objCommand.Parameters.Add(Mapped.Parameter("?telefone", contato.Telefone));
            objCommand.Parameters.Add(Mapped.Parameter("?email", contato.Email));
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", contato.Codigo));
            


            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
    }
}