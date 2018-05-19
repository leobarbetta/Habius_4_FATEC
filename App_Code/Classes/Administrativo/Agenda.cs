using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Classes.Administrativo
{
    public class Agenda
    {
        private Pessoa _pessoaAdvogado;

        public Pessoa PessoaAdvogado
        {
            get { return _pessoaAdvogado; }
            set { _pessoaAdvogado = value; }
        }

        private int _finalizado;

        public int Finalizado
        {
            get { return _finalizado; }
            set { _finalizado = value; }
        }

        private string _descricao;

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        private DateTime _dataFinalizacao;

        public DateTime DataFinalizacao
        {
            get { return _dataFinalizacao; }
            set { _dataFinalizacao = value; }
        }

        private DateTime _dataCriacao;

        public DateTime DataCriacao
        {
            get { return _dataCriacao; }
            set { _dataCriacao = value; }
        }

        private string _titulo;

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        private int _codigo;

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
    }
}