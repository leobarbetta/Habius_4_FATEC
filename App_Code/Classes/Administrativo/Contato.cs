using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Classes.Administrativo
{
    public class Contato
    {
        private Pessoa _pessoaAdvogado;

        public Pessoa PessoaAdvogado
        {
            get { return _pessoaAdvogado; }
            set { _pessoaAdvogado = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private string _telefone;

        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }

        private string _celular;

        public string Celular
        {
            get { return _celular; }
            set { _celular = value; }
        }

        private int _codigo;

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        } 
    }
}