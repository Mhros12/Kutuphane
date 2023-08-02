using kutuphane.Controllers;
using kutuphane.Data;
using kutuphane.Interfaces;
using kutuphane.Models;
using Microsoft.EntityFrameworkCore;

namespace kutuphane.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Book>> GetAll()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }

        public bool Add(Book book)
        {
            _context.Add(book);
            return Save();
        }

        public bool CheckName(string name)
        {
            var bookTitleLowercase = name.ToLower();
            var BookFilter = _context.Books.Any(x => x.Name.ToLower() == bookTitleLowercase);
            return BookFilter;
        }

        bool IBookService.Update(Book book)
        {
            _context.Update(book);
            return Save();
        }

        public async Task<Book> GetById(Guid id)
        {
            var bookToUpdate = _context.Books.SingleOrDefault(x => x.Id == id);
            return bookToUpdate;
        }

        public bool Delete(Book book)
        {
            _context.Books.Remove(book);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
