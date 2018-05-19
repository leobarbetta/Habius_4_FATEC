using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Habius.Classes.Administrativo;

namespace Habius.Persistencia.Administrativo
{
    public class AdvogadoDB
    {
        public bool Insert(Advogado adv)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO PES_PESSOA(CON_CODIGO, NIV_CODIGO, END_CODIGO, PES_LOGIN, PES_SENHA, PES_DATACADASTRO, PES_CPF, PES_RG, PES_DATANASCIMENTO, PES_OAB, PES_SEXO, ECI_CODIGO) VALUE(?contato, ?nivel, ?endereco, ?login, ?senha, ?datacadastro, ?cpf, ?rg, ?datanascimento, ?oab, ?sexo, ?estadocivil );";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?contato", adv.ContatoPessoa.Codigo));
            //o sistema tera 4 niveis de usuario (1 Proprietario, 2 Advogado, 3 cliente fisico, cliente juridico), porém primeiramente contara somente com 2 niveis, cliente e advogado.
            //logo o nivel de acesso será introduzido a mão, quando for advogado 2 e quando for cliente 1
            objCommand.Parameters.Add(Mapped.Parameter("?nivel", 2));
            objCommand.Parameters.Add(Mapped.Parameter("?endereco", adv.Endereco.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?login", adv.UserName));
            objCommand.Parameters.Add(Mapped.Parameter("?senha", adv.Senha));
            objCommand.Parameters.Add(Mapped.Parameter("?datacadastro", adv.Datacadastro = DateTime.Now));
            objCommand.Parameters.Add(Mapped.Parameter("?cpf", adv.Cpf));
            objCommand.Parameters.Add(Mapped.Parameter("?rg", adv.Rg));
            objCommand.Parameters.Add(Mapped.Parameter("?datanascimento", adv.DataNascimento));
            objCommand.Parameters.Add(Mapped.Parameter("?oab", adv.OAB));
            objCommand.Parameters.Add(Mapped.Parameter("?sexo", adv.Sexo));
            objCommand.Parameters.Add(Mapped.Parameter("?estadocivil", adv.EstadoCivil.Codigo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
        public Advogado GetLastId(int codigo)
        {
            Advogado adv = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT P.PES_CODIGO FROM CON_CONTATO C INNER JOIN PES_PESSOA P ON (P.CON_CODIGO = C.CON_CODIGO) WHERE P.CON_CODIGO = ?codigo;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                adv = new Advogado();
                adv.Codigo = Convert.ToInt32(objDataReader["PES_CODIGO"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return adv;
        }
        public Advogado ValidaCPF(string cpf)
        {
            Advogado adv = null;
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
                adv = new Advogado();
                adv.Cpf = Convert.ToString(objDataReader["PES_CPF"]);
            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return adv;
        }
        public Advogado ValidaRG(string rg)
        {
            Advogado adv = null;
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
                adv = new Advogado();
                adv.Rg = Convert.ToString(objDataReader["PES_RG"]);
            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return adv;
        }
        public Advogado ValidaOAB(string oab)
        {
            Advogado adv = null;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT P.PES_OAB FROM PES_PESSOA P WHERE P.PES_OAB = ?oab";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?oab", oab));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                adv = new Advogado();
                adv.OAB = Convert.ToString(objDataReader["PES_OAB"]);
            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return adv;
        }
        public Advogado ValidaLogin(string login)
        {
            Advogado adv = null;
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
                adv = new Advogado();
                adv.UserName = Convert.ToString(objDataReader["PES_LOGIN"]);
            }
            objConexao.Close();
            objDataReader.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return adv;
        }

        public Advogado Select(int codigo)
        {
            Advogado adv = null;

            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataReader objDataReader;

            string sql = "SELECT CASE PES_SEXO WHEN 'F' THEN 'Feminino' WHEN 'M' THEN 'Masculino' END AS SEXO, P.* FROM PES_PESSOA P WHERE P.PES_CODIGO =?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));
            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                adv = new Advogado();
                adv.Codigo = Convert.ToInt32(objDataReader["PES_CODIGO"]);
                adv.UserName = Convert.ToString(objDataReader["PES_LOGIN"]);
                adv.Sexo = Convert.ToString(objDataReader["SEXO"]);
                adv.Ativo = Convert.ToInt32(objDataReader["PES_ATIVO"]);
                adv.Cpf = Convert.ToString(objDataReader["PES_CPF"]);
                adv.Rg = Convert.ToString(objDataReader["PES_RG"]);
                adv.DataNascimento = Convert.ToDateTime(objDataReader["PES_DATANASCIMENTO"]); ;
                adv.OAB = Convert.ToString(objDataReader["PES_OAB"]);
                Contato con = new Contato();
                con.Codigo = Convert.ToInt32(objDataReader["CON_CODIGO"]);
                adv.ContatoPessoa = con;
                Endereco end = new Endereco();
                end.Codigo = Convert.ToInt32(objDataReader["END_CODIGO"]);
                adv.Endereco = end;
                EstadoCivil estCivil = new EstadoCivil();
                estCivil.Codigo = Convert.ToInt32(objDataReader["ECI_CODIGO"]);
                adv.EstadoCivil = estCivil;
            }

            objDataReader.Close();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();

            return adv;
        }

        public bool Update(Advogado advogado)
        {
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            string sql = "UPDATE PES_PESSOA SET PES_LOGIN=?login, PES_CPF=?cpf, PES_RG=?rg, PES_DATANASCIMENTO=?data,  PES_OAB=?oab, PES_SEXO=?sexo, ECI_CODIGO=?estadocivil WHERE PES_CODIGO=?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            //CAMPOS QUE SERÃO ATUALIZADOS
            objCommand.Parameters.Add(Mapped.Parameter("?login", advogado.UserName));
            objCommand.Parameters.Add(Mapped.Parameter("?cpf", advogado.Cpf));
            objCommand.Parameters.Add(Mapped.Parameter("?rg", advogado.Rg));
            objCommand.Parameters.Add(Mapped.Parameter("?data", advogado.DataNascimento));
            objCommand.Parameters.Add(Mapped.Parameter("?oab", advogado.OAB));
            objCommand.Parameters.Add(Mapped.Parameter("?sexo", advogado.Sexo));
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", advogado.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?estadocivil", advogado.EstadoCivil.Codigo));



            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
    }
}