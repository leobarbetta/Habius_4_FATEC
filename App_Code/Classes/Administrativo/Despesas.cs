using Habius.Classes.Administrativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Classes.Administrativo
{
    public class Despesas
    {
        public TipoDespesa TipoDespesa { get; set; }
        public int Codigo { get; set; }
        public string Obs { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public Processo Processo { get; set; }
        public Pessoa PesCodigo { get; set; }

    }
}