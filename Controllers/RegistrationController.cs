using kutuphane.Models;
using Microsoft.AspNetCore.Mvc;
using kutuphane.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;

namespace kutuphane.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;

        [ActivatorUtilitiesConstructor]
        public RegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DropDownList(BookModel Books)
        {
            var BookList = _context.Books.Where(k => k.Status == true).ToList();
            ViewBag.BookBag = new SelectList(BookList, "Book", "Book");//Bunun üzerinde dur braz bu şekilde yapabilirsin daha sonra da
            return View();
        }
        [HttpGet]
        public IActionResult AddRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRegister(RegistrationModel Register)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Register);
                await _context.SaveChangesAsync();
                return RedirectToAction("Privacy", "Home");
            }      
            return View(Register);
        }
        [HttpGet]
        public IActionResult RegisterEdit(Guid id)
        {
            var entityOnDb = _context.Registrations.SingleOrDefault(x => x.RegId == id);

            if (entityOnDb == null) return RedirectToAction("Privacy", "Home");

            return View(entityOnDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterEdit(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var Register = new RegistrationModel()
                {
                    RegId = model.RegId,
                    NameSurname = model.NameSurname,
                    BookName = model.BookName,
                };
                _context.Registrations.Update(Register);
                _context.SaveChanges();
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}
