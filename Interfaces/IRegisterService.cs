using kutuphane.Models;

namespace kutuphane.Interfaces
{
    public interface IRegisterService
    {
        Task<IEnumerable<Registration>> GetAll();
        bool Add(Registration register);
        bool Update(Registration register);
        bool Save();
        Task<IEnumerable<Book>> CheckBookList();
        Task<Book> CheckSelectedBookId(Guid id);
        Task<Registration> GetId(Guid id);
    }
}
