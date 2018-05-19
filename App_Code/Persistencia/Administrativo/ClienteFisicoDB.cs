using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Habius.Classes.Administrativo;

namespace Habius.Persistencia.Administrativo
{
    public class ClienteFisicoDB
    {
        public bool Insert(ClienteFisico cliFS)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO PES_PESSOA(CON_CODIGO, NIV_CODIGO, END_CODIGO, PES_LOGIN, PES_SENHA, PES_DATACADASTRO, PES_CPF, PES_RG, PES_DATANASCIMENTO, PES_SEXO, ECI_CODIGO) VALUE(?contato, ?nivel, ?endereco, ?login, ?senha, ?datacadastro, ?cpf, ?rg, ?datanascimento, ?sexo, ?estadocivil);";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?contato", cliFS.ContatoPessoa.Codigo));
            //o sistema tera 4 niveis de usuario (1 Proprietario, 2 Advogado, 3 cliente fisico, cliente juridico), porém primeiramente contara somente com 2 niveis, cliente e advogado.
            //logo o nivel de acesso será introduzido a mão, quando for advogado 2 e quando for cliente 1
            objCommand.Parameters.Add(Mapped.Parameter("?nivel", 3));
            objCommand.Parameters.Add(Mapped.Parameter("?endereco", cliFS.Endereco.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?login", cliFS.UserName));
            objCommand.Parameters.Add(Mapped.Parameter("?senha", cliFS.Senha));
            objCommand.Parameters.Add(Mapped.Parameter("?datacadastro", cliFS.Datacadastro = DateTime.Now));
            objCommand.Parameters.Add(Mapped.Parameter("?cpf", cliFS.Cpf));
            objCommand.Parameters.Add(Mapped.Parameter("?rg", cliFS.Rg));
            objCommand.Parameters.Add(Mapped.Parameter("?datanascimento", cliFS.DataNascimento));
            objCommand.Parameters.Add(Mapped.Parameter("?sexo", cliFS.Sexo));
            objCommand.Parameters.Add(Mapped.Parameter("?estadocivil", cliFS.EstadoCivil.Codigo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public ClienteFisico ValidaCPF(string cpf)
        {
            ClienteFisico cliFS = null;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT P.PES_CPF FROM PES_PESSOA P WHERE P.PES_CPF = ?cpf";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?cpf", cpf));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                cliFS = new ClienteFisico();
                cliFS.Cpf = Convert.ToString(objDataReader["PES_CPF"]);
            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return cliFS;
        }
        public ClienteFisico ValidaRG(string rg)
        {
            ClienteFisico cliFS = null;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT P.PES_RG FROM PES_PESSOA P WHERE P.PES_RG = ?rg";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?rg", rg));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                cliFS = new ClienteFisico();
                cliFS.Rg = Convert.ToString(objDataReader["PES_RG"]);
            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return cliFS;
        }
        public ClienteFisico ValidaLogin(string login)
        {
            ClienteFisico cliFi = null;
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
                cliFi = new ClienteFisico();
                cliFi.UserName = Convert.ToString(objDataReader["PES_LOGIN"]);
            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return cliFi;
        }
        public DataSet SelectAllClientes(int codigo)
        {
            DataSet ds = new DataSet();
            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            string sql = "SELECT CASE P.PES_ATIVO WHEN 1 THEN 'ATIVO' WHEN 0 THEN 'DESATIVADO' END AS ATIVO,  concat(ifnull(P.PES_CPF, ''), ifnull(P.PES_cnpj,'')) AS DOCS, P.*, C.*, N.* FROM PES_PESSOA P INNER JOIN CON_CONTATO C ON (C.CON_CODIGO = P.CON_CODIGO) INNER JOIN NIV_NIVEL N ON(P.NIV_CODIGO = N.NIV_CODIGO) WHERE P.NIV_CODIGO in (3, 4) AND C.PES_CODIGO = ?codigo;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return ds;
        }
        public DataSet BuscaCliente(int codigo, string busca)
        {
            DataSet ds = new DataSet();
            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            busca = "%" + busca + "%";

            string sql = "SELECT CASE P.PES_ATIVO WHEN 1 THEN 'ATIVO' WHEN 0 THEN 'DESATIVADO' END AS ATIVO,  concat(ifnull(P.PES_CPF, ''), ifnull(P.PES_cnpj,'')) AS DOCS, P.*, C.*, N.* FROM PES_PESSOA P INNER JOIN CON_CONTATO C ON (P.CON_CODIGO = C.CON_CODIGO) INNER JOIN NIV_NIVEL N ON(P.NIV_CODIGO = N.NIV_CODIGO) WHERE P.NIV_CODIGO in (3, 4) AND C.PES_CODIGO = ?codigo AND C.CON_NOME LIKE ?nome OR C.CON_CELULAR LIKE ?celular OR C.CON_TELEFONE LIKE ?telefone OR P.PES_CPF LIKE ?cpf OR P.PES_CNPJ LIKE ?cnpj;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objDataAdapter = Mapped.Adapter(objCommand);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?nome", busca));
            objCommand.Parameters.Add(Mapped.Parameter("?celular", busca));
            objCommand.Parameters.Add(Mapped.Parameter("?telefone", busca));
            objCommand.Parameters.Add(Mapped.Parameter("?cpf", busca));
            objCommand.Parameters.Add(Mapped.Parameter("?cnpj", busca));
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return ds;
        }
        public ClienteFisico Select(int id)
        {
            ClienteFisico obj = null;

            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataReader objDataReader;

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command("SELECT CASE PES_SEXO WHEN 'F' THEN 'Feminino' WHEN 'M' THEN 'Masculino' END AS SEXO, P.* FROM PES_PESSOA P WHERE PES_CODIGO = ?codigo", objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", id));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                obj = new ClienteFisico();
                obj.Codigo = Convert.ToInt32(objDataReader["PES_CODIGO"]);
                obj.Nivel = Convert.ToInt32(objDataReader["NIV_CODIGO"]);
                obj.Cpf = Convert.ToString(objDataReader["PES_CPF"]);
                obj.Rg = Convert.ToString(objDataReader["PES_RG"]);
                obj.Sexo = Convert.ToString(objDataReader["SEXO"]);
                EstadoCivil estCivil = new EstadoCivil();
                estCivil.Codigo = Convert.ToInt32(objDataReader["ECI_CODIGO"]);
                obj.EstadoCivil = estCivil;
                obj.DataNascimento = Convert.ToDateTime(objDataReader["PES_DATANASCIMENTO"]);
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
        public bool Update(ClienteFisico clifisico)
        {
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            string sql = "UPDATE PES_PESSOA SET PES_LOGIN=?login, PES_CPF=?cpf, PES_RG=?rg, ECI_CODIGO=?civil, PES_DATANASCIMENTO=?data, PES_SEXO=?sexo WHERE PES_CODIGO=?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            //CAMPOS QUE SERÃO ATUALIZADOS
            objCommand.Parameters.Add(Mapped.Parameter("?login", clifisico.UserName));
            objCommand.Parameters.Add(Mapped.Parameter("?cpf", clifisico.Cpf));
            objCommand.Parameters.Add(Mapped.Parameter("?rg", clifisico.Rg));
            objCommand.Parameters.Add(Mapped.Parameter("?data", clifisico.DataNascimento));
            objCommand.Parameters.Add(Mapped.Parameter("?sexo", clifisico.Sexo));
            objCommand.Parameters.Add(Mapped.Parameter("?civil", clifisico.EstadoCivil.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", clifisico.Codigo));



            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
        public DataSet SelectAllDDLCliente(int codigo)
        {
            DataSet ds = new DataSet();
            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM PES_PESSOA P INNER JOIN CON_CONTATO C ON (C.CON_CODIGO = P.CON_CODIGO) INNER JOIN NIV_NIVEL N ON(N.NIV_CODIGO = P.NIV_CODIGO) WHERE N.NIV_CODIGO IN (3,4) AND C.PES_CODIGO =?codigo;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return ds;
        }
    }
}



