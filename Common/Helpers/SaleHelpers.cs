namespace web_store_server.Common.Helpers
{
    public class SaleHelpers
    {
        private static readonly Random random = new();
        private const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string GenerateOrderNumber()
        {
            string fechaHoraActual = DateTimeOffset.Now.ToString("yyMMddHHmmss");

            string numeroAleatorio = random.Next(100000, 999999).ToString();

            char[] letrasAleatorias = new char[3];
            for (int i = 0; i < letrasAleatorias.Length; i++)
            {
                letrasAleatorias[i] = letras[random.Next(letras.Length)];
            }
            string letrasString = new string(letrasAleatorias);

            string numeroOrden = $"OD{fechaHoraActual}{letrasString}{numeroAleatorio}";

            return numeroOrden;
        }
    }
}
