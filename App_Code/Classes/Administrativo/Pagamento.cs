using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Classes.Administrativo
{
    public class Pagamento
    {
        private Servico _servico;

        public Servico Servico
        {
            get { return _servico; }
            set { _servico = value; }
        }

        private Processo _processo;

        public Processo Processo
        {
            get { return _processo; }
            set { _processo = value; }
        }

        private Advogado advogado;

        public Advogado Advogado
        {
            get { return advogado; }
            set { advogado = value; }
        }

        private Pessoa pes_cliente;

        public Pessoa Pes_cliente
        {
            get { return pes_cliente; }
            set { pes_cliente = value; }
        }

        private DateTime _dataPagamento;

        public DateTime DataPagamento
        {
            get { return _dataPagamento; }
            set { _dataPagamento = value; }
        }

        private decimal _valor;

        public decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        private int _codigo;

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
    }
}