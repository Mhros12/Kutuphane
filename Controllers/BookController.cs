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
            if (_context == null)
            {
                return View("Error");
            }
            List<Book> Book = _context.Books.ToList();
            return View(Book);
        }
        [HttpGet]
        public IActionResult AddBook()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(Book Books)
        {
            if (ModelState.IsValid)
            {
                var bookTitleLowercase = Books.Name.ToLower();
                var BookFilter = _context.Books.Any(x => x.Name.ToLower() == bookTitleLowercase);
                if (!BookFilter)
                {
                    Books.CreatedOn = DateTime.Now;
                    Books.ModifiedOn = null;
                    Books.IsInLibrary = 1;
                    _context.Add(Books);
                    await _context.SaveChangesAsync();
                    TempData["BookAdded"] = "Kitap Eklendi!";
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    TempData["ErrorMessageSameBook"] = "Aynı Kitap Eklenemez!";
                    return RedirectToAction("Index", "Book");
                }
            }
            return View(Books);
        }
        [HttpGet]
        public IActionResult BookEdit(Guid id)
        {
            var entityOnDb = _context.Books.SingleOrDefault(x=> x.Id == id);
            if (entityOnDb == null) return RedirectToAction("Index", "Book");
            return View(entityOnDb);
        }
        [HttpPost]
        public IActionResult BookEdit(Book model)
        {
            if (ModelState.IsValid)
            {
                var bookToUpdate = _context.Books.SingleOrDefault(x => x.Id == model.Id);
                if (bookToUpdate != null)
                {
                    if (bookToUpdate.IsInLibrary == 1)
                    {
                        bookToUpdate.Name = model.Name;
                        bookToUpdate.Writer = model.Writer;
                        bookToUpdate.ModifiedOn = DateTime.Now;
                        _context.Books.Update(bookToUpdate);
                        _context.SaveChanges();
                        return RedirectToAction("Index", "Book");
                    }
                    else if (bookToUpdate.IsInLibrary == 2)TempData["BookEditError"] = "Kitap Dışarıdayken Güncellenemez!";
                    else if(bookToUpdate.IsInLibrary== -1)TempData["BookEditError2"] = "Kitap Bakımdayken Güncellenemez!";
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult BookDelete(Guid id)
        {
            var entityOnDb = _context.Books.SingleOrDefault(x => x.Id == id);
            if (entityOnDb == null) return RedirectToAction("Index", "Book");
            return View(entityOnDb);
        }
        [HttpPost]
        public IActionResult BookDelete(Book model)
        {
            if (ModelState.IsValid)
            {
                var BookToDelete = _context.Books.SingleOrDefault(x => x.Id == model.Id);
                if (BookToDelete != null)
                {
                    if (BookToDelete.IsInLibrary == 1)
                    {
                        _context.Books.Remove(BookToDelete);
                        _context.SaveChanges();
                        TempData["ErrorMessage"] = "Kitap Silindi!";
                        return RedirectToAction("Index", "Book");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Dışarıda veya Bakımda Olan Kitap Silinemez!";
                    }
                }
            }
            return View();
        }
    }
}
