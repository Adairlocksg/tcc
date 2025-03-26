using System.Security.Cryptography;
using System.Text;

namespace TCC.Infra.Services
{
    public class HashService
    {
        private static readonly HashAlgorithm _algorithm = SHA512.Create();

        public static string Encrypt(string stringToEncrypt)
        {
            if (string.IsNullOrEmpty(stringToEncrypt))
                throw new NullReferenceException("Erro ao criptografar: Informe a string para criptografar");

            var encryptedString = _algorithm.ComputeHash(Encoding.UTF8.GetBytes(stringToEncrypt));

            var sb = new StringBuilder();

            foreach (var character in encryptedString)
            {
                sb.Append(character.ToString("X2"));
            }

            return sb.ToString();
        }

        public static bool IsEqual(string stringToEncrypt, string encryptedStringToCompare)
        {
            if (string.IsNullOrEmpty(encryptedStringToCompare))
                throw new NullReferenceException("Erro ao criptografar: Informe a string para comparação");

            var encryptedString = Encrypt(stringToEncrypt);

            return encryptedString == encryptedStringToCompare;
        }
    }
}
