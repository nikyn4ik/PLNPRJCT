using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Database
{
    public class Authorization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int ID_aut { get; set; }
        public string Login { get; set; }
        public string FIO { get; set; }
        public string PassHash { get; set; }
        public string Password
        {
            get { return PassHash; }
            set { PassHash = HP(value); }
        }

        public static string HP(string password)
        {
            using (SHA256 Sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = Sha256.ComputeHash(inputBytes);

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