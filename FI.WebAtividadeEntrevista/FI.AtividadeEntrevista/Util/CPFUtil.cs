using System.Linq;
using System.Text;

namespace FI.AtividadeEntrevista.Util
{
    static public class CPFUtil
    {
        private const int TamanhoCpf = 11;

        public static bool Validar(string cpf)
        {
            var cpfNumeros = Texto.ApenasNumeros(cpf);

            if (!TamanhoValido(cpfNumeros)) return false;
            return !TemDigitosRepetidos(cpfNumeros) && TemDigitosValidos(cpfNumeros);
        }

        private static bool TamanhoValido(string valor)
        {
            return valor.Length == TamanhoCpf;
        }

        private static bool TemDigitosRepetidos(string valor)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
            return invalidNumbers.Contains(valor);
        }

        private static bool TemDigitosValidos(string valor)
        {
            var number = valor.Substring(0, TamanhoCpf - 2);
            var digitoVerificador = new DigitoVerificador(number)
                .ComMultiplicadoresDeAte(2, 11)
                .Substituindo("0", 10, 11);
            var firstDigit = digitoVerificador.CalculaDigito();
            digitoVerificador.AddDigito(firstDigit);
            var secondDigit = digitoVerificador.CalculaDigito();

            return string.Concat(firstDigit, secondDigit) == valor.Substring(TamanhoCpf - 2, 2);
        }

        public static string Format(string s)
        {
            if (!string.IsNullOrWhiteSpace(s) && s.Trim().Length == 11)
                return s.Substring(0, 3) + "." + s.Substring(3, 3) + "." + s.Substring(6, 3) + "-" + s.Substring(9, 2);
            return s;
        }

        public static string RemoveMask(string s)
        {
            StringBuilder cpf = new StringBuilder("");
            if (!string.IsNullOrWhiteSpace(s))
            {
                int i, qt = s.Length;
                for (i = 0; i < qt; i++)
                {
                    switch (s[i])
                    {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            cpf.Append(s[i]);
                            break;
                    }
                }
            }
            return cpf.ToString();
        }
    }
}