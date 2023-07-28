using kutuphane.Data;
using kutuphane.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace kutuphane.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        [ActivatorUtilitiesConstructor]
        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddBook()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel Books)
        {
            if (ModelState.IsValid)
            {
                var bookTitleLowercase = Books.Book.ToLower();
                var BookFilter = _context.Books.Any(x => x.Book.ToLower() == bookTitleLowercase);
                if (!BookFilter)
                {
                    Books.Status = true;
                    _context.Add(Books);
                    await _context.SaveChangesAsync();
                    TempData["BookAdded"] = "Kitap Eklendi!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessageSameBook"] = "Aynı Kitap Eklenemez!";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(Books);
        }
        [HttpGet]
        public IActionResult BookEdit(Guid id)
        {
            var entityOnDb = _context.Books.SingleOrDefault(x=> x.BookId == id);
            if (entityOnDb == null) return RedirectToAction("Index", "Home");
            return View(entityOnDb);
        }
        [HttpPost]
        public IActionResult BookEdit(BookModel model)
        {
            if (ModelState.IsValid)
            {
                var bookToUpdate = _context.Books.SingleOrDefault(x => x.BookId == model.BookId);
                if (bookToUpdate != null)
                {
                    if (bookToUpdate.Status != false)
                    {
                        bookToUpdate.Book = model.Book;
                        _context.Books.Update(bookToUpdate);
                        _context.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["BookEditError"]= "Kitap Dışarıdayken Güncellenemez!";
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult BookDelete(Guid id)
        {
            var entityOnDb = _context.Books.SingleOrDefault(x => x.BookId == id);
            if (entityOnDb == null) return RedirectToAction("Index", "Home");
            return View(entityOnDb);
        }
        [HttpPost]
        public IActionResult BookDelete(BookModel model)
        {
            if (ModelState.IsValid)
            {
                var BookToDelete = _context.Books.SingleOrDefault(x => x.BookId == model.BookId);
                if (BookToDelete != null)
                {
                    if (BookToDelete.Status != false)
                    {
                        _context.Books.Remove(BookToDelete);
                        _context.SaveChanges();
                        TempData["ErrorMessage"] = "Kitap Silindi!";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Dışarıda Olan Kitap Silinemez!";
                    }
                }
            }
            return View();
        }
    }
}
