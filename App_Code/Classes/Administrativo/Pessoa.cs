using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Classes.Administrativo
{
    public class Pessoa
    {
        private EstadoCivil _estadoCivil;

        public EstadoCivil EstadoCivil
        {
            get { return _estadoCivil; }
            set { _estadoCivil = value; }
        }

        private int _nivel;

        public int Nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        private string _sexo;

        public string Sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }

        private Contato _contatoPessoa;

        public Contato ContatoPessoa
        {
            get { return _contatoPessoa; }
            set { _contatoPessoa = value; }
        }

        private Endereco _endereco;

        public Endereco Endereco
        {
            get { return _endereco; }
            set { _endereco = value; }
        }

        private int _ativo;

        public int Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        private DateTime _datacadastro;

        public DateTime Datacadastro
        {
            get { return _datacadastro; }
            set { _datacadastro = value; }
        }

        private string _senha;

        public string Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private int _codigo;

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
    }
}