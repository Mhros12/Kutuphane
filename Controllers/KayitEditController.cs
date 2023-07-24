using kutuphane.Models;
using Microsoft.AspNetCore.Mvc;
using kutuphane.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;

namespace kutuphane.Controllers
{
    public class KayitEditController : Controller
    {
        private readonly ApplicationDbContext _context;

        [ActivatorUtilitiesConstructor]
        public KayitEditController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(KitaplikModel kitap)
        {
            var kitaplist = _context.Kitapliklar.Where(k => k.Durum == true).ToList();
            ViewBag.kitapL = new SelectList(kitaplist, "Kitap", "Kitap");
            return View();
            var kitaplist = _context.Kitapliklar.Where(k => k.Durum == true).ToList();
            ViewBag.kitapL = new SelectList(kitaplist, "Kitap","Kitap");
            return View();
        }
        public IActionResult KayitEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KayitEkle(KayitModel kayit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kayit);
                await _context.SaveChangesAsync();
                return RedirectToAction("Privacy", "Home");
            }      
            return View(kayit);
        }
        [HttpGet]
        public IActionResult KayitEdit(int id)
        {
            try
            {
                var kayit = _context.Kayitlar.SingleOrDefault(x => x.Id == id);
                if (kayit != null)
                {
                    var Kitaplar = new KayitModel()
                    {
                        Id = kayit.Id,
                        AdSoyad = kayit.AdSoyad,
                        KitapAdi = kayit.KitapAdi,
                    };
                    return View(kayit);
                }
                else
                {
                    return RedirectToAction("Privacy", "Home");
                }
            }
            catch (Exception )
            {
                return RedirectToAction("Privacy", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult KayitEdit(KayitModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Kayit = new KayitModel()
                    {
                        Id = model.Id,
                        AdSoyad = model.AdSoyad,
                        KitapAdi = model.KitapAdi,
                    };
                    _context.Kayitlar.Update(Kayit);
                    _context.SaveChanges();
                    return RedirectToAction("Privacy", "Home");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }

        }
    }
}
