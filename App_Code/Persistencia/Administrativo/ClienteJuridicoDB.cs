using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Habius.Classes.Administrativo;

namespace Habius.Persistencia.Administrativo
{
    public class ClienteJuridicoDB
    {
        public bool Insert(ClienteJuridico cliJu)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO PES_PESSOA(CON_CODIGO, NIV_CODIGO, END_CODIGO, PES_LOGIN, PES_SENHA, PES_DATACADASTRO, PES_CNPJ) VALUE(?contato, ?nivel, ?endereco, ?login, ?senha, ?datacadastro, ?cnpj);";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?contato", cliJu.ContatoPessoa.Codigo));
            //o sistema tera 4 niveis de usuario (1 Proprietario, 2 Advogado, 3 cliente fisico, cliente juridico), porém primeiramente contara somente com 2 niveis, cliente e advogado.
            //logo o nivel de acesso será introduzido a mão, quando for advogado 2 e quando for cliente 1
            objCommand.Parameters.Add(Mapped.Parameter("?nivel", 4));
            objCommand.Parameters.Add(Mapped.Parameter("?endereco", cliJu.Endereco.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?login", cliJu.UserName));
            objCommand.Parameters.Add(Mapped.Parameter("?senha", cliJu.Senha));
            objCommand.Parameters.Add(Mapped.Parameter("?datacadastro", cliJu.Datacadastro = DateTime.Now));
            objCommand.Parameters.Add(Mapped.Parameter("?cnpj", cliJu.Cnpj));
         
            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public ClienteJuridico ValidaCNPJ(string CNPJ)
        {
            ClienteJuridico cliJu = null;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT P.PES_CNPJ FROM PES_PESSOA P WHERE P.PES_CNPJ = ?cnpj";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?cnpj", CNPJ));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                cliJu = new ClienteJuridico();
                cliJu.Cnpj = Convert.ToString(objDataReader["PES_CNPJ"]);
            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();
            return cliJu;
        }
        public ClienteJuridico ValidaLogin(string login)
        {
            ClienteJuridico cliJu = null;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT P.PES_LOGIN FROM PES_PESSOA P WHERE P.PES_LOGIN = ?login";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?login", login));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                cliJu = new ClienteJuridico();
                cliJu.UserName = Convert.ToString(objDataReader["PES_LOGIN"]);
            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return cliJu;
        }
        public ClienteJuridico Select(int id)
        {
            ClienteJuridico obj = null;
            //CRIAÇÃO DAS VARIAVEIS 
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            System.Data.IDataReader objDataReader;
            //ABRE CONEXÃO
            objConexao = Mapped.Connection();
            //DEFINE SQL QUE SERÁ EXECUTADA
            objCommand = Mapped.Command("SELECT * FROM PES_PESSOA WHERE PES_CODIGO = ?codigo", objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", id));
            //EXECUTA COMANDO NO BD
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                obj = new ClienteJuridico();
                obj.Codigo = Convert.ToInt32(objDataReader["PES_CODIGO"]);
                obj.Nivel = Convert.ToInt32(objDataReader["NIV_CODIGO"]);
                obj.Cnpj = Convert.ToString(objDataReader["PES_CNPJ"]);
                obj.UserName = Convert.ToString(objDataReader["PES_LOGIN"]);
                Endereco end = new Endereco();
                end.Codigo = Convert.ToInt32(objDataReader["END_CODIGO"]);
                obj.Endereco = end;
                Contato con = new Contato();
                con.Codigo = Convert.ToInt32(objDataReader["con_codigo"]);
                obj.ContatoPessoa = con;
            }

            objDataReader.Close();
            objConexao.Close();

            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return obj;
        }

        public bool Update(ClienteJuridico clijuridico)
        {
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            string sql = "UPDATE PES_PESSOA SET PES_LOGIN=?login, PES_CNPJ=?cnpj WHERE PES_CODIGO=?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            //CAMPOS QUE SERÃO ATUALIZADOS
            objCommand.Parameters.Add(Mapped.Parameter("?login", clijuridico.UserName));
            objCommand.Parameters.Add(Mapped.Parameter("?cnpj", clijuridico.Cnpj));
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", clijuridico.Codigo));



            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }

    }
}