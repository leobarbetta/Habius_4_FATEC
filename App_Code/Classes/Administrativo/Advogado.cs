using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Classes.Administrativo
{
    public class Advogado : PessoaFisica
    {
        private string _OAB;

        public string OAB
        {
            get { return _OAB; }
            set { _OAB = value; }
        }
    }
}