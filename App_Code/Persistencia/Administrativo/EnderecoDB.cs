using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Habius.Classes.Administrativo;

namespace Habius.Persistencia.Administrativo
{
    public class EnderecoDB
    {
        public bool Insert(Endereco end)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO END_ENDERECO(END_BAIRRO, END_CEP, END_COMPLEMENTO, END_LOGRADOURO, END_NUMERO, CID_CODIGO) VALUE(?bairro, ?cep, ?complemento, ?logradouro, ?numero, ?cidade);";
            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?bairro", end.Bairro));
            objCommand.Parameters.Add(Mapped.Parameter("?cep", end.Cep));
            objCommand.Parameters.Add(Mapped.Parameter("?complemento", end.Complemento));
            objCommand.Parameters.Add(Mapped.Parameter("?logradouro", end.Logradouro));
            objCommand.Parameters.Add(Mapped.Parameter("?numero", end.Numero));
            objCommand.Parameters.Add(Mapped.Parameter("?cidade", end.Cidade.Codigo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }

        public int GetLastId(Endereco end)
        {
            int codigo = 0;
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT END_CODIGO FROM END_ENDERECO WHERE END_BAIRRO=?bairro AND END_CEP=?cep AND END_NUMERO=?numero AND END_LOGRADOURO=?rua ORDER BY END_CODIGO DESC LIMIT 1";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?bairro", end.Bairro));
            objCommand.Parameters.Add(Mapped.Parameter("?cep", end.Cep));
            objCommand.Parameters.Add(Mapped.Parameter("?numero", end.Numero));
            objCommand.Parameters.Add(Mapped.Parameter("?rua", end.Logradouro));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                codigo = Convert.ToInt32(objDataReader["END_CODIGO"]);
            }

            objDataReader.Close();
            objConexao.Close();

            objCommand.Dispose();
            objConexao.Dispose();
            objDataReader.Dispose();
            
            return codigo;
        }
        public Endereco Select(int codigo)
        {
            Endereco end = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql ="SELECT * FROM END_ENDERECO WHERE END_CODIGO =?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo",codigo));

            objDataReader = objCommand.ExecuteReader();
            while(objDataReader.Read()){
                end = new Endereco();
                end.Bairro = Convert.ToString(objDataReader["END_BAIRRO"]);
                end.Cep = Convert.ToString(objDataReader["END_CEP"]);
                end.Complemento = Convert.ToString(objDataReader["END_COMPLEMENTO"]);
                end.Logradouro = Convert.ToString(objDataReader["END_LOGRADOURO"]);
                end.Numero = Convert.ToString(objDataReader["END_NUMERO"]);
                end.Codigo = Convert.ToInt32(objDataReader["END_CODIGO"]);

                Cidade cid = new Cidade();
                cid.Codigo = Convert.ToInt32(objDataReader["CID_CODIGO"]);
                end.Cidade = cid;

            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return end;
        }

        public bool Update(Endereco endereco)
        {
            System.Data.IDbConnection objConexao;
            System.Data.IDbCommand objCommand;
            string sql = "UPDATE END_ENDERECO SET END_LOGRADOURO=?logradouro, END_BAIRRO=?bairro, END_COMPLEMENTO=?complemento, END_CEP=?cep, END_NUMERO=?numero, CID_CODIGO=?cidcodigo WHERE END_CODIGO=?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?logradouro", endereco.Logradouro));
            objCommand.Parameters.Add(Mapped.Parameter("?bairro", endereco.Bairro));
            objCommand.Parameters.Add(Mapped.Parameter("?complemento", endereco.Complemento));
            objCommand.Parameters.Add(Mapped.Parameter("?cep", endereco.Cep));
            objCommand.Parameters.Add(Mapped.Parameter("?numero", endereco.Numero));
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", endereco.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?cidcodigo", endereco.Cidade.Codigo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
    }
}