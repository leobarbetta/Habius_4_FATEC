using Habius.Classes.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Habius.Persistencia.Administrativo
{
    public class RecursoDB
    {
        public DataSet SelectAllTribunal()
        {
            DataSet ds = new DataSet();
            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataAdapter objDataAdapter;

            string sql = "SELECT * FROM TRI_TRIBUNAL";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objDataAdapter = Mapped.Adapter(objCommand);
            objDataAdapter.Fill(ds);

            objConexao.Close();
            objConexao.Dispose();
            objCommand.Dispose();
            return ds;
        }
        public Recurso Select(int codigo)
        {
            Recurso rec = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM REC_RECURSO WHERE REC_CODIGO = ?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                rec = new Recurso();
                rec.Codigo = Convert.ToInt32(objDataReader["REC_CODIGO"]);
                rec.CodigoTribunal = Convert.ToInt32(objDataReader["TRI_CODIGO"]);
                rec.CodigoCamara = Convert.ToInt32(objDataReader["CAM_CODIGO"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return rec;
        }
        public Recurso SelectCamara(int codigo)
        {
            Recurso rec = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM CAM_CAMARA WHERE CAM_CODIGO = ?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                rec = new Recurso();
                rec.CodigoCamara = Convert.ToInt32(objDataReader["CAM_CODIGO"]);
                rec.Camara = Convert.ToString(objDataReader["CAM_CAMARA"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return rec;
        }
        public bool InsertCamara(Recurso rec)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO CAM_CAMARA(CAM_CAMARA) VALUE(?camara)";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?camara", rec.Camara));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
        public bool UpdateCamara(Recurso recurso)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "UPDATE CAM_CAMARA SET CAM_CAMARA=?camara WHERE CAM_CODIGO=?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?camara", recurso.Camara));
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", recurso.CodigoCamara));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
        public int GetLastIdCamara(string camara)
        {
            int codigo = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM CAM_CAMARA WHERE CAM_CAMARA = ?camara";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?camara", camara));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                codigo = Convert.ToInt32(objDataReader["CAM_CODIGO"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return codigo;
        }
        public Recurso SelectTribunal(int codigo)
        {
            Recurso rec = null;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM TRI_TRIBUNAL WHERE TRI_CODIGO = ?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", codigo));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                rec = new Recurso();
                rec.CodigoTribunal = Convert.ToInt32(objDataReader["TRI_CODIGO"]);
                rec.Tribunal = Convert.ToString(objDataReader["TRI_TRIBUNAL"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return rec;
        }
        public bool InsertRecurso(Recurso rec)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "INSERT INTO REC_RECURSO(TRI_CODIGO, CAM_CODIGO) VALUE(?tribunal, ?camara)";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?tribunal", rec.CodigoTribunal));
            objCommand.Parameters.Add(Mapped.Parameter("?camara", rec.CodigoCamara));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
        public bool UpdateRecurso(Recurso rec)
        {
            IDbCommand objCommand;
            IDbConnection objConexao;

            string sql = "UPDATE REC_RECURSO SET TRI_CODIGO=?tribunal WHERE REC_CODIGO=?codigo";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?tribunal", rec.CodigoTribunal));
            objCommand.Parameters.Add(Mapped.Parameter("?codigo", rec.Codigo));

            objCommand.ExecuteNonQuery();
            objConexao.Close();
            objCommand.Dispose();
            objConexao.Dispose();

            return true;
        }
        public int GetLastIdRecurso(Recurso rec)
        {
            int codigo = 0;

            IDbCommand objCommand;
            IDbConnection objConexao;
            IDataReader objDataReader;

            string sql = "SELECT * FROM REC_RECURSO WHERE TRI_CODIGO = ?tribunal AND CAM_CODIGO = ?camara";

            objConexao = Mapped.Connection();
            objCommand = Mapped.Command(sql, objConexao);
            objCommand.Parameters.Add(Mapped.Parameter("?tribunal", rec.CodigoTribunal));
            objCommand.Parameters.Add(Mapped.Parameter("?camara", rec.CodigoCamara));

            objDataReader = objCommand.ExecuteReader();
            while (objDataReader.Read())
            {
                codigo = Convert.ToInt32(objDataReader["REC_CODIGO"]);
            }

            objConexao.Close();
            objDataReader.Close();

            objConexao.Dispose();
            objCommand.Dispose();
            objDataReader.Dispose();

            return codigo;
        }
    }
}