using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habius.Common
{
    public class ValidaDoc
    {
        #region Valida CPF
        public static bool ValidaCPF(string cpf)
        {
            cpf = cpf.Replace(".", "");
            cpf = cpf.Replace("-", "");
            int[] num = new int[11];
            int soma;
            int i;
            int resultado1;
            int resultado2;
            if (cpf.Length == 11)
            {
                for (i = 0; i <= num.Length - 1; i++)
                {
                    num[i] = Convert.ToInt32(cpf.Substring(i, 1));
                }
                soma = num[0] * 10 + num[1] * 9 + num[2] * 8 + num[3] * 7 + num[4] * 6 + num[5] * 5 + num[6] * 4 + num[7] * 3 + num[8] * 2;
                soma = soma - (11 * ((int)(soma / 11)));
                if (soma == 0 | soma == 1)
                {
                    resultado1 = 0;
                }
                else
                {
                    resultado1 = 11 - soma;
                }
                if (resultado1 == num[9])
                {
                    soma = num[0] * 11 + num[1] * 10 + num[2] * 9 + num[3] * 8 + num[4] * 7 + num[5] * 6 + num[6] * 5 + num[7] * 4 + num[8] * 3 + num[9] * 2;
                    soma = soma - (11 * ((int)(soma / 11)));
                    if (soma == 0 | soma == 1)
                    {
                        resultado2 = 0;
                    }
                    else
                    {
                        resultado2 = 11 - soma;
                    }
                    if (resultado2 == num[10])
                    {
                        if (cpf == "11111111111" | cpf == "22222222222" | cpf == "33333333333" | cpf == "44444444444" | cpf == "55555555555" | cpf == "66666666666" | cpf == "77777777777" | cpf == "88888888888" | cpf == "99999999999" | cpf == "00000000000")
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
        #endregion
}