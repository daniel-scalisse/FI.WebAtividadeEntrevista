namespace FI.AtividadeEntrevista.Util
{
    public class Texto
    {
        public static string ApenasNumeros(string valor)
        {
            var onlyNumber = "";
            foreach (var s in valor)
            {
                if (char.IsDigit(s))
                    onlyNumber += s;
            }
            return onlyNumber.Trim();
        }
    }
}