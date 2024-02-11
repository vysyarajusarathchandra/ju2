using ju.entity;
using Microsoft.Extensions.Hosting;

namespace ju.services
{
    public interface IBookService
    {
        List<Book> GetBooks();
        void AddBook(Book book);
        Book GetBookById(int bookid);
        void updatebook(Book book);
        void deletebook(int bookid);
    }
}
