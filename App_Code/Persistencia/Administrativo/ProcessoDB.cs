using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Habius.Persistencia;
using Habius.Classes.Administrativo;

namespace Habius.Persistencia.Administrativo
{
    public class ProcessoDB
    {
        public DataSet SelectAllByCliente(int cliente)
        {
            DataSet ds = new DataSet();

            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            string sql = "SELECT ";
            sql += "PRO.PRO_CODIGO AS CODIGO_PROCESSO, ";
            sql += "PRO.PRO_NUMEROPROCESSO AS NUMEROPROCESSO,  ";
            sql += "PRO.PRO_DESCRICAO AS DESCRICAO, ";
            sql += "CON_ADV.CON_NOME AS NOME_ADVOGADO, ";
            sql += "CON_CLI.CON_NOME AS NOME_CLIENTE, ";
            sql += "ASS.ASS_ASSUNTO AS ASSUNTO, ";
            sql += "POS.POS_POSICAO AS POSICAO_CLIENTE, ";
            sql += "CLA.CLA_CLASSE AS CLASSE, ";
            sql += "VAR.VAR_VARA AS VARA, ";
            sql += "CID.CID_CIDADE AS COMARCA, ";
            sql += "DAP.DAP_DATAAUDIENCIA AS DATA_PROCESSO, ";
            sql += "PMV.PMV_DATA_MOVIMENTACAO AS DATA_MOVIMENTACAO, ";
            sql += "MOV.MOV_MOVIMENTACAO AS MOVIMENTACAO, ";
            sql += "PMV.PMV_CODIGO AS CODIGO_MOVIMENTACAO ";
            sql += "FROM PRO_PROCESSO PRO ";
            sql += "INNER JOIN APR_ADVOGADO_PROCESSO APR ON (PRO.PRO_CODIGO = APR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA ADV ON (ADV.PES_CODIGO = APR.PES_CODIGO) ";
            sql += "INNER JOIN CON_CONTATO CON_ADV ON (ADV.CON_CODIGO = CON_ADV.CON_CODIGO) ";
            sql += "INNER JOIN CPR_CLIENTE_PROCESSO CPR ON (PRO.PRO_CODIGO = CPR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA CLI ON (CLI.PES_CODIGO = CPR.PES_CODIGO) ";
            sql += "INNER JOIN CON_CONTATO CON_CLI ON (CLI.CON_CODIGO = CON_CLI.CON_CODIGO) ";
            sql += "INNER JOIN ASS_ASSUNTO ASS ON (PRO.ASS_CODIGO = ASS.ASS_CODIGO) ";
            sql += "INNER JOIN POS_POSICAOCLIENTE POS ON (POS.POS_CODIGO = PRO.POS_CODIGO) ";
            sql += "INNER JOIN CLA_CLASSE CLA ON (CLA.CLA_CODIGO = PRO.CLA_CODIGO) ";
            sql += "INNER JOIN VAR_VARA VAR ON (VAR.VAR_CODIGO = PRO.VAR_CODIGO) ";
            sql += "INNER JOIN CID_CIDADE CID ON (CID.CID_CODIGO = PRO.CID_CODIGO_COMARCA) ";
            sql += "LEFT JOIN DAP_DATAPROCESSO DAP ON (PRO.PRO_CODIGO = DAP.PRO_CODIGO) ";
            sql += "INNER JOIN PMV_PROCESSO_MOVIMENTACAO PMV ON (PRO.PRO_CODIGO = PMV.PRO_CODIGO) ";
            sql += "INNER JOIN MOV_MOVIMENTACAO MOV ON (MOV.MOV_CODIGO = PMV.MOV_CODIGO) ";
            sql += "WHERE CLI.PES_CODIGO = ?cliente AND PMV.PMV_FINALIZADO = 0";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?cliente", cliente));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return ds;
        }
        public int SelectTotalGraficoByDate(int adv, int mov, DateTime initialDate, DateTime finalDate)
        {
            int total = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT COUNT(*) AS TOTAL ";
            sql += "FROM PRO_PROCESSO PRO ";
            sql += "INNER JOIN APR_ADVOGADO_PROCESSO APR ON (PRO.PRO_CODIGO = APR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA ADV ON (ADV.PES_CODIGO = APR.PES_CODIGO) ";
            sql += "INNER JOIN PMV_PROCESSO_MOVIMENTACAO PMV ON (PRO.PRO_CODIGO = PMV.PRO_CODIGO) ";
            sql += "INNER JOIN MOV_MOVIMENTACAO MOV ON (MOV.MOV_CODIGO = PMV.MOV_CODIGO) ";
            sql += "WHERE ADV.PES_CODIGO = ?adv AND PMV.MOV_CODIGO = ?mov AND PMV.PMV_FINALIZADO = 0 AND PRO.PRO_DATACRIACAO Between ?initialdate and ?finaldate ";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
            objCommand.Parameters.Add(Mapped.Parameter("?mov", mov));
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
        public int SelectTotalGrafico(int adv, int mov)
        {
            int total = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT COUNT(*) AS TOTAL ";
            sql += "FROM PRO_PROCESSO PRO ";
            sql += "INNER JOIN APR_ADVOGADO_PROCESSO APR ON (PRO.PRO_CODIGO = APR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA ADV ON (ADV.PES_CODIGO = APR.PES_CODIGO) ";
            sql += "INNER JOIN PMV_PROCESSO_MOVIMENTACAO PMV ON (PRO.PRO_CODIGO = PMV.PRO_CODIGO) ";
            sql += "INNER JOIN MOV_MOVIMENTACAO MOV ON (MOV.MOV_CODIGO = PMV.MOV_CODIGO) ";
            sql += "WHERE ADV.PES_CODIGO = ?adv AND PMV.MOV_CODIGO = ?mov AND PMV.PMV_FINALIZADO = 0 ";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?adv", adv));
            objCommand.Parameters.Add(Mapped.Parameter("?mov", mov));

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
        public DataSet SelectAllByAdvogado(int advogado)
        {
            DataSet ds = new DataSet();

            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            string sql = "SELECT ";
            sql += "PRO.PRO_CODIGO AS CODIGO_PROCESSO, ";
            sql += "PRO.PRO_NUMEROPROCESSO AS NUMEROPROCESSO,  ";
            sql += "PRO.PRO_DESCRICAO AS DESCRICAO, ";
            sql += "CON_ADV.CON_NOME AS NOME_ADVOGADO, ";
            sql += "CON_CLI.CON_NOME AS NOME_CLIENTE, ";
            sql += "ASS.ASS_ASSUNTO AS ASSUNTO, ";
            sql += "POS.POS_POSICAO AS POSICAO_CLIENTE, ";
            sql += "CLA.CLA_CLASSE AS CLASSE, ";
            sql += "VAR.VAR_VARA AS VARA, ";
            sql += "CID.CID_CIDADE AS COMARCA, ";
            sql += "DAP.DAP_DATAAUDIENCIA AS DATA_PROCESSO, ";
            sql += "PMV.PMV_DATA_MOVIMENTACAO AS DATA_MOVIMENTACAO, ";
            sql += "MOV.MOV_MOVIMENTACAO AS MOVIMENTACAO, ";
            sql += "PMV.PMV_CODIGO AS CODIGO_MOVIMENTACAO ";
            sql += "FROM PRO_PROCESSO PRO ";
            sql += "INNER JOIN APR_ADVOGADO_PROCESSO APR ON (PRO.PRO_CODIGO = APR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA ADV ON (ADV.PES_CODIGO = APR.PES_CODIGO) ";
            sql += "INNER JOIN CON_CONTATO CON_ADV ON (ADV.CON_CODIGO = CON_ADV.CON_CODIGO) ";
            sql += "INNER JOIN CPR_CLIENTE_PROCESSO CPR ON (PRO.PRO_CODIGO = CPR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA CLI ON (CLI.PES_CODIGO = CPR.PES_CODIGO) ";
            sql += "INNER JOIN CON_CONTATO CON_CLI ON (CLI.CON_CODIGO = CON_CLI.CON_CODIGO) ";
            sql += "INNER JOIN ASS_ASSUNTO ASS ON (PRO.ASS_CODIGO = ASS.ASS_CODIGO) ";
            sql += "INNER JOIN POS_POSICAOCLIENTE POS ON (POS.POS_CODIGO = PRO.POS_CODIGO) ";
            sql += "INNER JOIN CLA_CLASSE CLA ON (CLA.CLA_CODIGO = PRO.CLA_CODIGO) ";
            sql += "INNER JOIN VAR_VARA VAR ON (VAR.VAR_CODIGO = PRO.VAR_CODIGO) ";
            sql += "INNER JOIN CID_CIDADE CID ON (CID.CID_CODIGO = PRO.CID_CODIGO_COMARCA) ";
            sql += "LEFT JOIN DAP_DATAPROCESSO DAP ON (PRO.PRO_CODIGO = DAP.PRO_CODIGO) ";
            sql += "INNER JOIN PMV_PROCESSO_MOVIMENTACAO PMV ON (PRO.PRO_CODIGO = PMV.PRO_CODIGO) ";
            sql += "INNER JOIN MOV_MOVIMENTACAO MOV ON (MOV.MOV_CODIGO = PMV.MOV_CODIGO) ";
            sql += "WHERE ADV.PES_CODIGO = ?advogado AND PMV.PMV_FINALIZADO = 0 AND PMV.MOV_CODIGO <> 7";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?advogado", advogado));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return ds;
        }
        public DataSet SelectAllProcessosByDate(int advogado, DateTime initialDate, DateTime finalDate)
        {
            DataSet ds = new DataSet();

            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            string sql = "SELECT ";
            sql += "PRO.PRO_CODIGO AS CODIGO_PROCESSO, ";
            sql += "PRO.PRO_DATACRIACAO AS DATA_CRIACAO, ";
            sql += "PRO.PRO_NUMEROPROCESSO AS NUMEROPROCESSO,  ";
            sql += "PRO.PRO_DESCRICAO AS DESCRICAO, ";
            sql += "CON_ADV.CON_NOME AS NOME_ADVOGADO, ";
            sql += "CON_CLI.CON_NOME AS NOME_CLIENTE, ";
            sql += "ASS.ASS_ASSUNTO AS ASSUNTO, ";
            sql += "POS.POS_POSICAO AS POSICAO_CLIENTE, ";
            sql += "CLA.CLA_CLASSE AS CLASSE, ";
            sql += "VAR.VAR_VARA AS VARA, ";
            sql += "CID.CID_CIDADE AS COMARCA, ";
            sql += "DAP.DAP_DATAAUDIENCIA AS DATA_PROCESSO, ";
            sql += "PMV.PMV_DATA_MOVIMENTACAO AS DATA_MOVIMENTACAO, ";
            sql += "MOV.MOV_MOVIMENTACAO AS MOVIMENTACAO, ";
            sql += "PMV.PMV_CODIGO AS CODIGO_MOVIMENTACAO ";
            sql += "FROM PRO_PROCESSO PRO ";
            sql += "INNER JOIN APR_ADVOGADO_PROCESSO APR ON (PRO.PRO_CODIGO = APR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA ADV ON (ADV.PES_CODIGO = APR.PES_CODIGO) ";
            sql += "INNER JOIN CON_CONTATO CON_ADV ON (ADV.CON_CODIGO = CON_ADV.CON_CODIGO) ";
            sql += "INNER JOIN CPR_CLIENTE_PROCESSO CPR ON (PRO.PRO_CODIGO = CPR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA CLI ON (CLI.PES_CODIGO = CPR.PES_CODIGO) ";
            sql += "INNER JOIN CON_CONTATO CON_CLI ON (CLI.CON_CODIGO = CON_CLI.CON_CODIGO) ";
            sql += "INNER JOIN ASS_ASSUNTO ASS ON (PRO.ASS_CODIGO = ASS.ASS_CODIGO) ";
            sql += "INNER JOIN POS_POSICAOCLIENTE POS ON (POS.POS_CODIGO = PRO.POS_CODIGO) ";
            sql += "INNER JOIN CLA_CLASSE CLA ON (CLA.CLA_CODIGO = PRO.CLA_CODIGO) ";
            sql += "INNER JOIN VAR_VARA VAR ON (VAR.VAR_CODIGO = PRO.VAR_CODIGO) ";
            sql += "INNER JOIN CID_CIDADE CID ON (CID.CID_CODIGO = PRO.CID_CODIGO_COMARCA) ";
            sql += "LEFT JOIN DAP_DATAPROCESSO DAP ON (PRO.PRO_CODIGO = DAP.PRO_CODIGO) ";
            sql += "INNER JOIN PMV_PROCESSO_MOVIMENTACAO PMV ON (PRO.PRO_CODIGO = PMV.PRO_CODIGO) ";
            sql += "INNER JOIN MOV_MOVIMENTACAO MOV ON (MOV.MOV_CODIGO = PMV.MOV_CODIGO) ";
            sql += "WHERE ADV.PES_CODIGO = ?advogado AND PMV.PMV_FINALIZADO = 0 AND PRO.PRO_DATACRIACAO BETWEEN ?initialDate AND ?finalDate ORDER BY PMV.PMV_DATA_MOVIMENTACAO";


            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?advogado", advogado));
            objCommand.Parameters.Add(Mapped.Parameter("?initialDate", initialDate));
            objCommand.Parameters.Add(Mapped.Parameter("?finalDate", finalDate));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return ds;
        }
        public DataSet SelectAllOrderByDate(int advogado)
        {
            DataSet ds = new DataSet();

            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            string sql = "SELECT ";
            sql += "PRO.PRO_CODIGO AS CODIGO_PROCESSO, ";
            sql += "PRO.PRO_NUMEROPROCESSO AS NUMEROPROCESSO,  ";
            sql += "PRO.PRO_DESCRICAO AS DESCRICAO, ";
            sql += "CON_ADV.CON_NOME AS NOME_ADVOGADO, ";
            sql += "CON_CLI.CON_NOME AS NOME_CLIENTE, ";
            sql += "ASS.ASS_ASSUNTO AS ASSUNTO, ";
            sql += "POS.POS_POSICAO AS POSICAO_CLIENTE, ";
            sql += "CLA.CLA_CLASSE AS CLASSE, ";
            sql += "VAR.VAR_VARA AS VARA, ";
            sql += "CID.CID_CIDADE AS COMARCA, ";
            sql += "DAP.DAP_DATAAUDIENCIA AS DATA_PROCESSO, ";
            sql += "PMV.PMV_DATA_MOVIMENTACAO AS DATA_MOVIMENTACAO, ";
            sql += "MOV.MOV_MOVIMENTACAO AS MOVIMENTACAO, ";
            sql += "PMV.PMV_CODIGO AS CODIGO_MOVIMENTACAO ";
            sql += "FROM PRO_PROCESSO PRO ";
            sql += "INNER JOIN APR_ADVOGADO_PROCESSO APR ON (PRO.PRO_CODIGO = APR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA ADV ON (ADV.PES_CODIGO = APR.PES_CODIGO) ";
            sql += "INNER JOIN CON_CONTATO CON_ADV ON (ADV.CON_CODIGO = CON_ADV.CON_CODIGO) ";
            sql += "INNER JOIN CPR_CLIENTE_PROCESSO CPR ON (PRO.PRO_CODIGO = CPR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA CLI ON (CLI.PES_CODIGO = CPR.PES_CODIGO) ";
            sql += "INNER JOIN CON_CONTATO CON_CLI ON (CLI.CON_CODIGO = CON_CLI.CON_CODIGO) ";
            sql += "INNER JOIN ASS_ASSUNTO ASS ON (PRO.ASS_CODIGO = ASS.ASS_CODIGO) ";
            sql += "INNER JOIN POS_POSICAOCLIENTE POS ON (POS.POS_CODIGO = PRO.POS_CODIGO) ";
            sql += "INNER JOIN CLA_CLASSE CLA ON (CLA.CLA_CODIGO = PRO.CLA_CODIGO) ";
            sql += "INNER JOIN VAR_VARA VAR ON (VAR.VAR_CODIGO = PRO.VAR_CODIGO) ";
            sql += "INNER JOIN CID_CIDADE CID ON (CID.CID_CODIGO = PRO.CID_CODIGO_COMARCA) ";
            sql += "LEFT JOIN DAP_DATAPROCESSO DAP ON (PRO.PRO_CODIGO = DAP.PRO_CODIGO) ";
            sql += "INNER JOIN PMV_PROCESSO_MOVIMENTACAO PMV ON (PRO.PRO_CODIGO = PMV.PRO_CODIGO) ";
            sql += "INNER JOIN MOV_MOVIMENTACAO MOV ON (MOV.MOV_CODIGO = PMV.MOV_CODIGO) ";
            sql += "WHERE ADV.PES_CODIGO = ?advogado AND PMV.PMV_FINALIZADO = 0 AND PMV.MOV_CODIGO <> 7 ORDER BY DAP.DAP_DATAAUDIENCIA";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?advogado", advogado));
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return ds;
        }
        public DataSet BuscaProcesso(int advogado, string busca)
        {
            DataSet ds = new DataSet();

            IDbConnection objConexao;
            IDbCommand objCommand;
            IDataAdapter objDataAdapter;

            busca = "%" + busca + "%";

            string sql = "SELECT ";
            sql += "PRO.PRO_CODIGO AS CODIGO_PROCESSO, ";
            sql += "PRO.PRO_NUMEROPROCESSO AS NUMEROPROCESSO,  ";
            sql += "PRO.PRO_DESCRICAO AS DESCRICAO, ";
            sql += "CON_ADV.CON_NOME AS NOME_ADVOGADO, ";
            sql += "CON_CLI.CON_NOME AS NOME_CLIENTE, ";
            sql += "ASS.ASS_ASSUNTO AS ASSUNTO, ";
            sql += "POS.POS_POSICAO AS POSICAO_CLIENTE, ";
            sql += "CLA.CLA_CLASSE AS CLASSE, ";
            sql += "VAR.VAR_VARA AS VARA, ";
            sql += "CID.CID_CIDADE AS COMARCA, ";
            sql += "DAP.DAP_DATAAUDIENCIA AS DATA_PROCESSO, ";
            sql += "PMV.PMV_DATA_MOVIMENTACAO AS DATA_MOVIMENTACAO, ";
            sql += "MOV.MOV_MOVIMENTACAO AS MOVIMENTACAO, ";
            sql += "PMV.PMV_CODIGO AS CODIGO_MOVIMENTACAO ";
            sql += "FROM PRO_PROCESSO PRO ";
            sql += "INNER JOIN APR_ADVOGADO_PROCESSO APR ON (PRO.PRO_CODIGO = APR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA ADV ON (ADV.PES_CODIGO = APR.PES_CODIGO) ";
            sql += "INNER JOIN CON_CONTATO CON_ADV ON (ADV.CON_CODIGO = CON_ADV.CON_CODIGO) ";
            sql += "INNER JOIN CPR_CLIENTE_PROCESSO CPR ON (PRO.PRO_CODIGO = CPR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA CLI ON (CLI.PES_CODIGO = CPR.PES_CODIGO) ";
            sql += "INNER JOIN CON_CONTATO CON_CLI ON (CLI.CON_CODIGO = CON_CLI.CON_CODIGO) ";
            sql += "INNER JOIN ASS_ASSUNTO ASS ON (PRO.ASS_CODIGO = ASS.ASS_CODIGO) ";
            sql += "INNER JOIN POS_POSICAOCLIENTE POS ON (POS.POS_CODIGO = PRO.POS_CODIGO) ";
            sql += "INNER JOIN CLA_CLASSE CLA ON (CLA.CLA_CODIGO = PRO.CLA_CODIGO) ";
            sql += "INNER JOIN VAR_VARA VAR ON (VAR.VAR_CODIGO = PRO.VAR_CODIGO) ";
            sql += "INNER JOIN CID_CIDADE CID ON (CID.CID_CODIGO = PRO.CID_CODIGO_COMARCA) ";
            sql += "LEFT JOIN DAP_DATAPROCESSO DAP ON (PRO.PRO_CODIGO = DAP.PRO_CODIGO) ";
            sql += "INNER JOIN PMV_PROCESSO_MOVIMENTACAO PMV ON (PRO.PRO_CODIGO = PMV.PRO_CODIGO) ";
            sql += "INNER JOIN MOV_MOVIMENTACAO MOV ON (MOV.MOV_CODIGO = PMV.MOV_CODIGO) ";
            sql += "WHERE ADV.PES_CODIGO = 1 AND PMV.PMV_FINALIZADO = 0 AND CLA.CLA_CLASSE LIKE ?busca OR ASS.ASS_ASSUNTO LIKE ?busca OR PRO.PRO_NUMEROPROCESSO LIKE ?busca ";
            sql += "OR CON_ADV.CON_NOME LIKE ?busca OR CON_CLI.CON_NOME LIKE ?busca OR POS.POS_POSICAO LIKE ?busca OR PRO.PRO_DESCRICAO LIKE ?busca ";
            sql += "OR VAR.VAR_VARA LIKE ?busca OR CID.CID_CIDADE LIKE ?busca OR MOV.MOV_MOVIMENTACAO LIKE ?busca";


            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?advogado", advogado));
            objCommand.Parameters.Add(Mapped.Parameter("?busca", busca));

            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return ds;
        }
        public bool Insert(Processo pro)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO PRO_PROCESSO(POS_CODIGO, CID_CODIGO_COMARCA, VAR_CODIGO, CLA_CODIGO, REC_CODIGO, ASS_CODIGO, PRO_DATACRIACAO, PRO_NUMEROPROCESSO, PRO_DESCRICAO, PRO_OBSERVACAO) VALUE(?posicao, ?cidade, ?vara, ?classe, ?recurso, ?assunto, ?datacriacao, ?numero, ?descricao, ?observacao)";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?posicao", pro.PosicaoCliente.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?cidade", pro.Comarca.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?vara", pro.Vara.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?classe", pro.Classe.Codigo));
            if (pro.Recurso != null)
            {
                objCommand.Parameters.Add(Mapped.Parameter("?recurso", pro.Recurso.Codigo));
            }
            else
            {
                objCommand.Parameters.Add(Mapped.Parameter("?recurso", null));
            }
            objCommand.Parameters.Add(Mapped.Parameter("?assunto", pro.Assunto.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?datacriacao", pro.DataCriacao));
            objCommand.Parameters.Add(Mapped.Parameter("?numero", pro.NumeroProcesso));
            objCommand.Parameters.Add(Mapped.Parameter("?descricao", pro.Descricao));
            objCommand.Parameters.Add(Mapped.Parameter("?observacao", pro.Observacao));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public Processo Select(int codigo)
        {
            Processo pro = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT ";
            sql += "PRO.*, ";
            sql += "CLI.PES_CODIGO AS CODIGO_CLIENTE, ";
            sql += "ADV.PES_CODIGO AS CODIGO_ADVOGADO, ";
            sql += "PRO.REC_CODIGO AS CODIGO_RECURSO, ";
            sql += "PRO.PRO_NUMEROPROCESSO AS NUMEROPROCESSO, ";
            sql += "PRO.PRO_DESCRICAO AS DESCRICAO, ";
            sql += "CON_ADV.CON_NOME AS NOME_ADVOGADO, ";
            sql += "CON_CLI.CON_NOME AS NOME_CLIENTE, ";
            sql += "ASS.ASS_ASSUNTO AS ASSUNTO, ";
            sql += "POS.POS_POSICAO AS POSICAO_CLIENTE, ";
            sql += "CLA.CLA_CLASSE AS CLASSE, ";
            sql += "VAR.VAR_VARA AS VARA, ";
            sql += "CID.CID_CIDADE AS COMARCA, ";
            sql += "MOV.MOV_CODIGO AS MOVIMENTACAO ";
            sql += "FROM PRO_PROCESSO PRO ";
            sql += "INNER JOIN APR_ADVOGADO_PROCESSO APR ON (PRO.PRO_CODIGO = APR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA ADV ON (ADV.PES_CODIGO = APR.PES_CODIGO) ";
            sql += "INNER JOIN CON_CONTATO CON_ADV ON (ADV.CON_CODIGO = CON_ADV.CON_CODIGO) ";
            sql += "INNER JOIN CPR_CLIENTE_PROCESSO CPR ON (PRO.PRO_CODIGO = CPR.PRO_CODIGO) ";
            sql += "INNER JOIN PES_PESSOA CLI ON (CLI.PES_CODIGO = CPR.PES_CODIGO) ";
            sql += "INNER JOIN CON_CONTATO CON_CLI ON (CLI.CON_CODIGO = CON_CLI.CON_CODIGO) ";
            sql += "INNER JOIN ASS_ASSUNTO ASS ON (PRO.ASS_CODIGO = ASS.ASS_CODIGO) ";
            sql += "INNER JOIN POS_POSICAOCLIENTE POS ON (POS.POS_CODIGO = PRO.POS_CODIGO) ";
            sql += "INNER JOIN CLA_CLASSE CLA ON (CLA.CLA_CODIGO = PRO.CLA_CODIGO) ";
            sql += "INNER JOIN VAR_VARA VAR ON (VAR.VAR_CODIGO = PRO.VAR_CODIGO) ";
            sql += "INNER JOIN CID_CIDADE CID ON (CID.CID_CODIGO = PRO.CID_CODIGO_COMARCA) ";
            sql += "INNER JOIN PMV_PROCESSO_MOVIMENTACAO PMV ON (PRO.PRO_CODIGO = PMV.PRO_CODIGO) ";
            sql += "INNER JOIN MOV_MOVIMENTACAO MOV ON (MOV.MOV_CODIGO = PMV.MOV_CODIGO) ";
            sql += "WHERE PRO.PRO_CODIGO = ?codigo AND PMV.PMV_FINALIZADO = 0 ";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                pro = new Processo();
                pro.Codigo = Convert.ToInt32(objDataReader["PRO_CODIGO"]);
                pro.NumeroProcesso = Convert.ToString(objDataReader["PRO_NUMEROPROCESSO"]);
                pro.Descricao = Convert.ToString(objDataReader["PRO_DESCRICAO"]);
                pro.Observacao = Convert.ToString(objDataReader["PRO_OBSERVACAO"]);
                pro.DataCriacao = Convert.ToDateTime(objDataReader["PRO_DATACRIACAO"]);

                Vara var = new Vara();
                var.Codigo = Convert.ToInt32(objDataReader["VAR_CODIGO"]);
                pro.Vara = var;

                Cidade cid = new Cidade();
                cid.Codigo = Convert.ToInt32(objDataReader["CID_CODIGO_COMARCA"]);
                pro.Comarca = cid;

                Classe cla = new Classe();
                cla.Codigo = Convert.ToInt32(objDataReader["CLA_CODIGO"]);
                pro.Classe = cla;

                PosicaoCliente pos = new PosicaoCliente();
                pos.Codigo = Convert.ToInt32(objDataReader["POS_CODIGO"]);
                pro.PosicaoCliente = pos;

                Recurso rec = new Recurso();
                if (objDataReader["CODIGO_RECURSO"] != DBNull.Value)
                {
                    rec.Codigo = Convert.ToInt32(objDataReader["CODIGO_RECURSO"]);
                    pro.Recurso = rec;
                }
                else
                {
                    rec = new Recurso();
                }
                Assunto ass = new Assunto();
                ass.Codigo = Convert.ToInt32(objDataReader["ASS_CODIGO"]);
                pro.Assunto = ass;

                Pessoa pes = new Pessoa();
                pes.Codigo = Convert.ToInt32(objDataReader["CODIGO_CLIENTE"]);
                pro.PessoaCliente = pes;

                Advogado adv = new Advogado();
                adv.Codigo = Convert.ToInt32(objDataReader["CODIGO_ADVOGADO"]);
                pro.PessoaAdvogado = adv;

                Movimentacao mov = new Movimentacao();
                mov.Codigo = Convert.ToInt32(objDataReader["MOVIMENTACAO"]);
                pro.Movimentacao = mov;
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return pro;
        }
        public int GetLastId(Processo pro)
        {
            int codigo = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM PRO_PROCESSO WHERE PRO_NUMEROPROCESSO = ?numero AND PRO_DESCRICAO = ?descricao";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?numero", pro.NumeroProcesso));
            objCommand.Parameters.Add(Mapped.Parameter("?descricao", pro.Descricao));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                codigo = Convert.ToInt32(objDataReader["PRO_CODIGO"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return codigo;
        }
        public bool InsertClienteProcesso(int cliente, int processo)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO CPR_CLIENTE_PROCESSO(PES_CODIGO, PRO_CODIGO) VALUE(?cliente, ?processo);";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?cliente", cliente));
            objCommand.Parameters.Add(Mapped.Parameter("?processo", processo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public bool UpdateClienteProcesso(int cliente, int processo)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "UPDATE CPR_CLIENTE_PROCESSO SET PES_CODIGO=?cliente, PRO_CODIGO=?processo WHERE PRO_CODIGO=?processo;";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?cliente", cliente));
            objCommand.Parameters.Add(Mapped.Parameter("?processo", processo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public bool InsertAdvogadoProcesso(int advogado, int processo)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO APR_ADVOGADO_PROCESSO(PES_CODIGO, PRO_CODIGO) VALUE(?advogado, ?processo);";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);

            objCommand.Parameters.Add(Mapped.Parameter("?advogado", advogado));
            objCommand.Parameters.Add(Mapped.Parameter("?processo", processo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
        public bool Update(Processo pro)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "UPDATE PRO_PROCESSO SET POS_CODIGO =?posicao, CID_CODIGO_COMARCA = ?cidade, VAR_CODIGO = ?vara,CLA_CODIGO = ?classe, REC_CODIGO = ?recurso, PRO_DATACRIACAO = ?datacriacao, PRO_NUMEROPROCESSO = ?numero, PRO_DESCRICAO =?descricao, PRO_OBSERVACAO =?observacao WHERE PRO_CODIGO = ?codigoprocesso";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?posicao", pro.PosicaoCliente.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?cidade", pro.Comarca.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?vara", pro.Vara.Codigo));
            objCommand.Parameters.Add(Mapped.Parameter("?classe", pro.Classe.Codigo));
            if (pro.Recurso != null)
            {
                objCommand.Parameters.Add(Mapped.Parameter("?recurso", pro.Recurso.Codigo));
            }
            else
            {
                objCommand.Parameters.Add(Mapped.Parameter("?recurso", null));
            }
            objCommand.Parameters.Add(Mapped.Parameter("?datacriacao", pro.DataCriacao));
            objCommand.Parameters.Add(Mapped.Parameter("?numero", pro.NumeroProcesso));
            objCommand.Parameters.Add(Mapped.Parameter("?descricao", pro.Descricao));
            objCommand.Parameters.Add(Mapped.Parameter("?observacao", pro.Observacao));
            objCommand.Parameters.Add(Mapped.Parameter("?codigoprocesso", pro.Codigo));
            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();
            return true;
        }
    }
}