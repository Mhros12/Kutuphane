using kutuphane.Data;
using kutuphane.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace kutuphane.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        [ActivatorUtilitiesConstructor]
        public HomeController (ApplicationDbContext context)
        {
            _context = context; 
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<KitaplikModel> kayit = _context.Kitapliklar.ToList();
            return View(kayit);
        }
        public IActionResult Privacy()
        {
            List<KayitModel> kayit = _context.Kayitlar.ToList();
            return View(kayit);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult KitapEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KitapEkle(KitaplikModel Kitap1)
        {
            if (ModelState.IsValid)
            {
                Kitap1.Durum = true;
                _context.Add(Kitap1);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(Kitap1);
        }
    }
}