using kutuphane.Data;
using kutuphane.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kutuphane.Controllers
{
    public class KitapEditController : Controller
    {
        private readonly ApplicationDbContext _context;

        [ActivatorUtilitiesConstructor]
        public KitapEditController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult KitapEdit(int id)
        {
            try
            {
                var kitap = _context.Kitapliklar.SingleOrDefault(x=> x.Id==id);
                if (kitap != null)
                {
                    var Kitaplar = new KitaplikModel()
                    {
                        Id = kitap.Id,
                        Durum = kitap.Durum,
                        Kitap = kitap.Kitap,
                    };
                    return View(kitap);
                }
                else
                {
                    TempData["errorMessage"] = $"Kitap bigileri id:{id} de bulunmuyor";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] =ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult KitapEdit(KitaplikModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var kitap = new KitaplikModel()
                    {
                        Id = model.Id,
                        Durum = model.Durum,
                        Kitap = model.Kitap,
                    };
                    _context.Kitapliklar.Update(kitap);
                    _context.SaveChanges();
                    TempData["successMessage"] = "başarıyla kaydedildi!";
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    TempData["errorMessage"] = "Geçersiz veriler";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }
        [HttpGet]
        public IActionResult KitapSil(int id)
        {
            try
            {
                var kitap = _context.Kitapliklar.SingleOrDefault(x => x.Id == id);
                if (kitap != null)
                {
                    var Kitaplar = new KitaplikModel()
                    {
                        Id = kitap.Id,
                        Durum = kitap.Durum,
                        Kitap = kitap.Kitap,
                    };
                    return View(kitap);
                }
                else
                {
                    TempData["errorMessage"] = $"Kitap bigileri id:{id} de bulunmuyor";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult KitapSil(KayitModel model)
        {
            try
            {
                var kitap = _context.Kitapliklar.SingleOrDefault(x => x.Id == model.Id);
                if (kitap != null)
                {
                    _context.Kitapliklar.Remove(kitap);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Kitap başarıyla silindi!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["errorMessage"] = $"Kitap bigileri id:{model.Id} de bulunmuyor";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
