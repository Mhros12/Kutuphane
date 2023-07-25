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
        [HttpGet]
        public IActionResult AddRegister()
        {
            var bookList = _context.Books.Where(k => k.Status == true).ToList();
            var selectList = bookList.Select(book => new SelectListItem
            {
                //Value = book.BookId.ToString(),
                Text = book.Book,
            }).ToList();

            ViewBag.BookBag = selectList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRegister(RegistrationModel Register)
        {
            if (ModelState.IsValid)
        {
            var selectedBook = _context.Books.FirstOrDefault(book => book.Book == Register.BookName);
            selectedBook.Status = false;
            _context.Registrations.Add(Register);
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
        public IActionResult RegisterEdit(RegistrationModel Register)
        {
            if (ModelState.IsValid)
            {
                _context.Registrations.Update(Register);
                _context.SaveChanges();
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult RegisterStatusUpdate(Guid id)
        {
            var entityOnDb = _context.Registrations.SingleOrDefault(x => x.RegId == id);

            if (entityOnDb == null) return RedirectToAction("Privacy", "Home");

            return View(entityOnDb);
        }
        [HttpPost]
        public IActionResult RegisterStatusUpdate(RegistrationModel Register)
        {
            if (ModelState.IsValid)
            {
                var RegisterDelete = _context.Registrations.SingleOrDefault(x => x.RegId == Register.RegId);
                if (RegisterDelete != null)
                {
                    var BookStatus = _context.Books.FirstOrDefault(book => book.Book == Register.BookName);
                    BookStatus.Status = true;
                    _context.Registrations.Remove(RegisterDelete);
                    _context.SaveChanges();
                }
                return RedirectToAction("Privacy", "Home");
            }
            return View();
        }
    }
}
