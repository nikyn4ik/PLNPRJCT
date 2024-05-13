using System;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Certificate
{
        [Key] public int IdQuaCertificate { get; set; }
    public string StandardPerMark { get; set; }
    public string Manufacturer { get; set; }
    public string ProductStandard { get; set; }
    public DateTime DateAddCertificate { get; set; }
}
}