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
        private readonly ILogger<HomeController>? _logger;

        private readonly ApplicationDbContext? _context;

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
            if (_context == null)
            {
                return View("Error");
            }
            List<BookModel> Book = _context.Books.ToList();
            return View(Book);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}