using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Classes.Administrativo
{
    /// <summary>
    /// Summary description for TipoDespesa
    /// </summary>
    public class TipoDespesa
    {
        private string _descricao;

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        public int Codigo { get; set; }
        public int Categoria { get; set; }

    }
}