using System;

namespace SharePointULSCatcher.Core.Correlations
{
    public static class CorrelationFactory
    {
        private static String ID_HEADER = "ID de Correlação:";
        private static String DATE_HEADER = "Data e Hora:";
        private static String[] SEPARATOR = { "\n" };

        public static bool TryParse(String text, out Correlation result)
        {
            try
            {
                result = Parse(text);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        /*
         * Parsing para o formato:
         * 
         *  ID de Correlação: xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
         *  Data e Hora: dd/MM/yyyy hh:mm:ss
         */
        public static Correlation Parse(String text)
        {
            text = text.Replace(ID_HEADER, String.Empty)
                       .Replace(DATE_HEADER, String.Empty)
                       .Replace('\r', '\n')
                       .Trim();

            var lines = text.Split(SEPARATOR, StringSplitOptions.RemoveEmptyEntries);
            
            var guid = Guid.Parse(lines[0]);
            var date = DateTime.Parse(lines[1]);

            return new Correlation(guid, date);
        }
    }
}