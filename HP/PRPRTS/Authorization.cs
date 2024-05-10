using System.ComponentModel.DataAnnotations;

namespace HP
{
    public class Authorization
    {
        [Key]
        public int ID_aut { get; set; }
        public string Login { get; set; }
        public string PassHash { get; set; }
        public string FIO { get; set; }
    }
}