using kutuphane.Data;
using kutuphane.Interfaces;
using kutuphane.Models;
using kutuphane.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace kutuphane.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        [ActivatorUtilitiesConstructor]
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            if (_bookService == null)
            {
                return View("Error");
            }
            IEnumerable<Book> books = await _bookService.GetAll();
            return View(books);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();  
        }
        [HttpPost]
        public IActionResult Add(Book books)
        {
            if (ModelState.IsValid)
            {
                var BookFilter = _bookService.CheckName(books.Name);
                if (!BookFilter)
                {
                    books.CreatedOn = DateTime.Now;
                    books.ModifiedOn = null;
                    books.IsInLibrary = 1;
                    _bookService.Add(books);
                    TempData["BookAdded"] = "Kitap Eklendi!";
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    TempData["ErrorMessageSameBook"] = "Aynı Kitap Eklenemez!";
                    return RedirectToAction("Index", "Book");
                }
            }
            return View(books);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var entityOnDb = await _bookService.GetById(id);
            if (entityOnDb == null) return RedirectToAction("Index", "Book");
            return View(entityOnDb);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Book model)
        {
            if (ModelState.IsValid)
            {
                var bookToUpdate = await _bookService.GetById(model.Id);
                if (bookToUpdate != null)
                {
                    if (bookToUpdate.IsInLibrary == 1)
                    {
                        bookToUpdate.Name = model.Name;
                        bookToUpdate.Writer = model.Writer;
                        bookToUpdate.ModifiedOn = DateTime.Now;
                        _bookService.Update(bookToUpdate);
                        return RedirectToAction("Index", "Book");
                    }
                    else if (bookToUpdate.IsInLibrary == 2)TempData["BookEditError"] = "Kitap Dışarıdayken Güncellenemez!";
                    else if(bookToUpdate.IsInLibrary== -1)TempData["BookEditError2"] = "Kitap Bakımdayken Güncellenemez!";
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entityOnDb = await _bookService.GetById(id);
            if (entityOnDb == null) return RedirectToAction("Index", "Book");
            return View(entityOnDb);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Book model)
        {
            if (ModelState.IsValid)
            {
                var BookToDelete = await _bookService.GetById(model.Id);
                if (BookToDelete != null)
                {
                    if (BookToDelete.IsInLibrary == 1)
                    {
                        _bookService.Delete(BookToDelete);                       
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
