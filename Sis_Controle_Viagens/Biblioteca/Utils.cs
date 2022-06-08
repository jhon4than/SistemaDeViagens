using System.Security.Cryptography;
using System.Text;

namespace Sis_Controle_Viagens.Biblioteca
{
    public class Utils
    {
        public static string GenHash(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = SHA256.Create().ComputeHash(bytes);
            return BytesToHex(hash);
        }

        public static string GenHashHex(string hex)
        {
            byte[] bytes = HexToBytes(hex);
            byte[] hash = SHA256.Create().ComputeHash(bytes);
            return BytesToHex(hash);
        }

        public static string BytesToHex(byte[] bytes)
        {
            return Convert.ToHexString(bytes).ToLower();
        }

        public static byte[] HexToBytes(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        public static DateTime ToDateTime(long unixTime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTime).ToLocalTime();
            return dtDateTime;
        }

        public static long GetTime()
        {
            long epochTicks = new DateTime(1970, 1, 1).Ticks;
            long nowTicks = DateTime.UtcNow.Ticks;
            long tmStamp = ((nowTicks - epochTicks) / TimeSpan.TicksPerSecond);
            return tmStamp;
        }
    }
}
