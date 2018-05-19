using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Classes.Administrativo
{
    public abstract class PessoaJuridica : Pessoa
    {
        private string _cnpj;

        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }
    }
}