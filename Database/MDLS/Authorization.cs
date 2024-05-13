using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Authorization
    {
        [Key] public int ID_aut { get; set; }
        public string Login { get; set; }
        public string PassHash { get; set; }
        public string FIO { get; set; }
    }
}