using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Classes.Administrativo
{
    public abstract class PessoaFisica : Pessoa
    {
        private DateTime _dataNascimento;

        public DateTime DataNascimento
        {
            get { return _dataNascimento; }
            set { _dataNascimento = value; }
        }

        private string _rg;

        public string Rg
        {
            get { return _rg; }
            set { _rg = value; }
        }

        private string _cpf;

        public string Cpf
        {
            get { return _cpf; }
            set { _cpf = value; }
        }

    }
}