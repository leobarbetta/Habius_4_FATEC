using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Classes.Administrativo
{
    public class Recurso
    {
        private string _tribunal;

        public string Tribunal
        {
            get { return _tribunal; }
            set { _tribunal = value; }
        }

        private string _camara;

        public string Camara
        {
            get { return _camara; }
            set { _camara = value; }
        }

        private int _codigoTribunal;

        public int CodigoTribunal
        {
            get { return _codigoTribunal; }
            set { _codigoTribunal = value; }
        }

        private int _codigoCamara;

        public int CodigoCamara
        {
            get { return _codigoCamara; }
            set { _codigoCamara = value; }
        }

        private int _codigoRecurso;

        public int Codigo
        {
            get { return _codigoRecurso; }
            set { _codigoRecurso = value; }
        }
    }
}