using kutuphane.Models;
using Microsoft.AspNetCore.Mvc;
using kutuphane.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using kutuphane.Interfaces;
using kutuphane.Services;
using Microsoft.Win32;

namespace kutuphane.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegisterService _registerService;

        [ActivatorUtilitiesConstructor]
        public RegistrationController(IRegisterService registerService)
        {
            _registerService = registerService;
        }
        public async Task<IActionResult> Index()
        {
            if (_registerService == null)
            {
                return View("Error");
            }
            IEnumerable<Registration> register = await _registerService.GetAll();
            return View(register);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var bookList = await _registerService.CheckBookList();
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
        public async Task<IActionResult> Add(Registration register)
        {
            if (ModelState.IsValid)
            {
                var selectedBook = await _registerService.CheckSelectedBookId(register.BookId);
                if (selectedBook != null && selectedBook.IsInLibrary == 1)
                {
                    selectedBook.IsInLibrary = 2;
                    register.BookId = selectedBook.Id;
                    register.CreatedOn = DateTime.Now;
                    register.ModifiedOn = null;
                    register.ReturnedOn = null;
                    _registerService.Add(register);
                    return RedirectToAction("Index", "Registration");
                }
                else
                {
                    ModelState.AddModelError("BookId", "Seçili kitap mevcut değil.");
                }
            }
            var bookList = await _registerService.CheckBookList();
            var selectList = bookList.Select(book => new SelectListItem
            {
                Value = book.Id.ToString(),
                Text = book.Name,
            }).ToList();

            ViewBag.BookBag = selectList;
            return View(register);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var entityOnDb = await _registerService.GetId(id);

            if (entityOnDb == null) return RedirectToAction("Index", "Registration");

            return View(entityOnDb);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Registration register)
        {
            if (ModelState.IsValid)
            {
                register.ModifiedOn = DateTime.Now;
                _registerService.Update(register);
                return RedirectToAction("Index", "Registration");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> StatusUpdate(Guid id)
        {
            var entityOnDb = await _registerService.GetId(id);

            if (entityOnDb == null) return RedirectToAction("Index", "Registration");

            return View(entityOnDb);
        }
        [HttpPost]
        public async Task<IActionResult> StatusUpdate(Registration register)
        {
            if (ModelState.IsValid)
            {
                var BookStatus = await _registerService.CheckSelectedBookId(register.BookId);
                BookStatus.IsInLibrary = 1;
                register.ReturnedOn = DateTime.Now;
                _registerService.Update(register);
                return RedirectToAction("Index", "Registration");
            }
            else
            {
                return View();
            }
        }
    }
}
