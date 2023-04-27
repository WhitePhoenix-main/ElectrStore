using System.Globalization;
using System.Text.RegularExpressions;

namespace ElectrStore
{
    public interface INormalizer
    {
        int GetPriceInPennies(string strToNorm);
    }
    public class Normalizer : INormalizer
    {
        public int GetPriceInPennies(string strToConvert)
        {
            strToConvert = strToConvert.Replace(',', '.');
            strToConvert = Regex.Replace(strToConvert, "[^0-9/.]", "");
            strToConvert = Regex.Replace(strToConvert, @"(?<!^\d+)\.", "");
            return (int)(Decimal.Parse(strToConvert, CultureInfo.InvariantCulture)*100);
        }
    }
}