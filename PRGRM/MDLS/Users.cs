using System.Security.Cryptography;
using System.Text;

namespace PRGRM.MDLS
{
    class Users
    {
        public int id_authorization { get; set; }
        public string login { get; set; }
        private string passwordHash;
        public string Password
        {
            get { return passwordHash; }
            set { passwordHash = HP(value); }
        }

        public static string HP(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}