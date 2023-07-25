using System.ComponentModel.DataAnnotations;

namespace kutuphane.Models
{
    public class RegistrationModel
    {
        [Key]
        public Guid RegId { get; set; }
        public string NameSurname { get; set; }
        public string BookName { get; set; }
    }
}
