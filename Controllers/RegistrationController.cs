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
            if (_context == null)
            {
                return View("Error");
            }
            List<Registration> Reg = _context.Registrations.ToList();
            return View(Reg);
        }
        [HttpGet]
        public IActionResult AddRegister()
        {
            var bookList = _context.Books.Where(k => k.IsInLibrary == 1).ToList();
            var selectList = bookList.Select(book => new SelectListItem
            {
                Value = book.Id.ToString(),
                Text = book.Name,
            }).ToList();

            ViewBag.BookBag = selectList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRegister(Registration Register)
        {
            if (ModelState.IsValid)
            {
                var SelectedBook = await _context.Books.FirstOrDefaultAsync(book => book.Id == Register.BookId);
                if (SelectedBook != null && SelectedBook.IsInLibrary == 1)
                {
                    SelectedBook.IsInLibrary = 2;
                    Register.BookId = SelectedBook.Id;
                    Register.CreatedOn = DateTime.Now;
                    Register.ModifiedOn = null;
                    Register.ReturnedOn = null;
                    _context.Registrations.Add(Register);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Registration");
                }
                else
                {
                    ModelState.AddModelError("BookId", "Seçili kitap mevcut değil.");
                }
            }
            var bookList = await _context.Books.Where(k => k.IsInLibrary == 1).ToListAsync();
            var selectList = bookList.Select(book => new SelectListItem
            {
                Value = book.Id.ToString(),
                Text = book.Name,
            }).ToList();

            ViewBag.BookBag = selectList;
            return View(Register);
        }
        [HttpGet]
        public IActionResult RegisterEdit(Guid id)
        {
            var entityOnDb = _context.Registrations.SingleOrDefault(x => x.Id == id);

            if (entityOnDb == null) return RedirectToAction("Index", "Registration");

            return View(entityOnDb);
        }
        [HttpPost]
        public IActionResult RegisterEdit(Registration Register)
        {
            if (ModelState.IsValid)
            {
                Register.ModifiedOn = DateTime.Now;
                _context.Registrations.Update(Register);
                _context.SaveChanges();
                return RedirectToAction("Index", "Registration");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult RegisterStatusUpdate(Guid id)
        {
            var entityOnDb = _context.Registrations.SingleOrDefault(x => x.Id == id);

            if (entityOnDb == null) return RedirectToAction("Index", "Registration");

            return View(entityOnDb);
        }
        [HttpPost]
        public IActionResult RegisterStatusUpdate(Registration Register)
        {
            if (ModelState.IsValid)
            {
                var BookStatus = _context.Books.FirstOrDefault(book => book.Id == Register.BookId);
                BookStatus.IsInLibrary = 1;
                Register.ReturnedOn = DateTime.Now;
                _context.Registrations.Update(Register);
                _context.SaveChanges();
                return RedirectToAction("Index", "Registration");
            }
            else
            {
                return View();
            }
        }
    }
}
