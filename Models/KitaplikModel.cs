using Microsoft.AspNetCore.Mvc.Rendering;

namespace kutuphane.Models
{
    public class KitaplikModel
    {
        public int Id { get; set; }
        public bool Durum { get; set; }
        public string Kitap { get; set; }
    }
}
