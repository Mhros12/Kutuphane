using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace kutuphane.Models
{
    public class BookModel
    {
        [Key]
        public Guid BookId { get; set; }
        public bool Status { get; set; }
        public string Book { get; set; }
    }
}
