using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Classes.Administrativo
{
    public class Movimentacao
    {
        private DateTime _dataMovimentacao;

        public DateTime DataMovimentacao
        {
            get { return _dataMovimentacao; }
            set { _dataMovimentacao = value; }
        }

        private Processo _processo;

        public Processo Processo
        {
            get { return _processo; }
            set { _processo = value; }
        }

        private string _descricao;

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        private int _codigo;

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
    }
}