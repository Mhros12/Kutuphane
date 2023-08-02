using kutuphane.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace kutuphane.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();
        bool Add(Book book);
        bool Update(Book book);
        bool Delete(Book book);
        bool Save();
        bool CheckName(string name);
        Task<Book> GetById(Guid id); 
    }
}
