using kutuphane.Data;
using kutuphane.Interfaces;
using kutuphane.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System.Net;

namespace kutuphane.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly ApplicationDbContext _context;
        public RegisterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Registration>> GetAll()
        {
            var registers = await _context.Registrations.ToListAsync();
            return registers;
        }

        public bool Add(Registration register)
        {
            _context.Registrations.Add(register);
            return Save();
        }

        public async Task<IEnumerable<Book>> CheckBookList()
        {
            var bookList = _context.Books.Where(x => x.IsInLibrary == 1).ToList();
            return bookList;
        }

        public bool Update(Registration register)
        {
            _context.Registrations.Update(register);
            return Save();
        }

        public async Task<Registration> GetId(Guid id)
        {
            var entityOnDb = _context.Registrations.SingleOrDefault(x => x.Id == id);
            return entityOnDb;
        }

        public async Task<Book> CheckSelectedBookId(Guid id)
        {
            var selectedBook = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            return selectedBook;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
