using System.Security.Cryptography;

namespace GeneralizedGameRockPaperScissors
{
    internal static class KeyGeneration
    {
        public static byte[] GenerateKey()
        {
            byte[] key = new byte[32];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }

            return key;
        }
    }
}
