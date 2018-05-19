using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Classes.Administrativo
{
    public class DataProcesso
    {
        private Processo _processo;

        public Processo Processo
        {
            get { return _processo; }
            set { _processo = value; }
        }

        private DateTime _dataAudiencia;

        public DateTime DataAudiencia
        {
            get { return _dataAudiencia; }
            set { _dataAudiencia = value; }
        }

        private int _codigo;

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
    }
}