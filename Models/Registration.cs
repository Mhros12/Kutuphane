using System.ComponentModel.DataAnnotations;

namespace kutuphane.Models
{
    public class Registration
    {
        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public Guid BookId { get; set; }
        public DateTime? ReturnedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set;}
    }
}
//projeye servis eklicez servises klasörü açıcaz book ve 