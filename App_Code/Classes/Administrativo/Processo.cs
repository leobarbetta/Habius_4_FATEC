using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Classes.Administrativo
{
    public class Processo
    {
        private Assunto _assunto;

        public Assunto Assunto
        {
            get { return _assunto; }
            set { _assunto = value; }
        }

        private DataProcesso _dataProcesso;

        public DataProcesso DataProcesso
        {
            get { return _dataProcesso; }
            set { _dataProcesso = value; }
        }

        private Recurso _recurso;

        public Recurso Recurso
        {
            get { return _recurso; }
            set { _recurso = value; }
        }

        private Movimentacao _movimentacao;

        public Movimentacao Movimentacao
        {
            get { return _movimentacao; }
            set { _movimentacao = value; }
        }

        private Pessoa _pessoaCliente;

        public Pessoa PessoaCliente
        {
            get { return _pessoaCliente; }
            set { _pessoaCliente = value; }
        }

        private Pessoa _pessoaAdvogado;

        public Pessoa PessoaAdvogado
        {
            get { return _pessoaAdvogado; }
            set { _pessoaAdvogado = value; }
        }

        private Vara _vara;

        public Vara Vara
        {
            get { return _vara; }
            set { _vara = value; }
        }

        private PosicaoCliente _posicaoCliente;

        public PosicaoCliente PosicaoCliente
        {
            get { return _posicaoCliente; }
            set { _posicaoCliente = value; }
        }

        private Cidade _comarca;

        public Cidade Comarca
        {
            get { return _comarca; }
            set { _comarca = value; }
        }

        private string _observacao;

        public string Observacao
        {
            get { return _observacao; }
            set { _observacao = value; }
        }

        private string _descricao;

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        private string _numeroProcesso;

        public string NumeroProcesso
        {
            get { return _numeroProcesso; }
            set { _numeroProcesso = value; }
        }

        private DateTime _dataCriacao;

        public DateTime DataCriacao
        {
            get { return _dataCriacao; }
            set { _dataCriacao = value; }
        }

        private Classe _classe;

        public Classe Classe
        {
            get { return _classe; }
            set { _classe = value; }
        }

        private int _codigo;

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
    }
}