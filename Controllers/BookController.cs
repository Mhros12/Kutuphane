using kutuphane.Data;
using kutuphane.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                Books.Status = true;
                _context.Add(Books);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View(Books);
        }
        [HttpGet]
        public IActionResult BookEdit(Guid id)
        {
            var entityOnDb = _context.Books.SingleOrDefault(x=> x.BookId == id);
            if (entityOnDb != null) return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public IActionResult BookEdit(BookModel model)
        {
            if (ModelState.IsValid)
                {
                    var Book = new BookModel()
                    {
                        BookId = model.BookId,
                        Status = model.Status,
                        Book = model.Book,
                    };
                    _context.Books.Update(Book);
                    _context.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    return View();
                }
        }
        [HttpGet]
        public IActionResult BookDelete(Guid id)
        {
            var entityOnDb = _context.Books.SingleOrDefault(x => x.BookId == id);
            if (entityOnDb != null) return RedirectToAction("Index", "Home");
            return View(entityOnDb);
        }
        [HttpPost]
        public IActionResult BookDelete(BookModel model)
        {
            try
            {
                var Book = _context.Books.SingleOrDefault(x => x.BookId == model.BookId);
                if (Book != null)
                {
                    _context.Books.Remove(Book);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
