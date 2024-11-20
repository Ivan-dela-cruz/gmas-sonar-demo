using System.Security.Cryptography;
using System.Text;

namespace BUE.Inscriptions.Shared.Utils
{
    public class CryptoPassword
    {
        public static CryptoPassword Instance { get; } = new CryptoPassword();
        public void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        public bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            var cc = Convert.FromBase64String(passwordHash);
            var cc2 = Convert.FromBase64String(passwordSalt);
            using (var hmac = new HMACSHA512(cc))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(cc2);
            }
        }
    }
}
