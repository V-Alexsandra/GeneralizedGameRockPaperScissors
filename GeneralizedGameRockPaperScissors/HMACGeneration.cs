using System.Security.Cryptography;
using System.Text;

namespace GeneralizedGameRockPaperScissors
{
    internal static class HMACGeneration
    {
        public static void GenerateHMAC(byte[] key, string computerMove)
        {
            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                byte[] hmacBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(computerMove));
                string hmacString = BitConverter.ToString(hmacBytes).Replace("-", "").ToUpper();
                Console.WriteLine("HMAC: " + hmacString.ToUpper());
            }
        }
    }
}
