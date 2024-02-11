using ju.Database;
using ju.entity;

namespace ju.services
{
    public class BookService : IBookService
    {
        private readonly Mycontext context = null;
        public BookService(Mycontext context)
        {
            this.context = context;
        }
        public void AddBook(Book book)
        {
            try
            {
                book.User = context.Users.Find(book.UserId);
                    context.Books.Add(book);
                    context.SaveChanges();

                

            }
            catch (Exception)
            { 

                throw;
            }
        }

        public void deletebook(int bookid)
        {
            try
            {
                var BookToDelete = context.Books.SingleOrDefault(p => p.Bookid == bookid);
                if (BookToDelete != null)
                {
                    context.Books.Remove(BookToDelete);
                    context.SaveChanges();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Book GetBookById(int bookid)
        {
            try
            {
                var res = context.Books.Find(bookid);
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Book> GetBooks()
        {
            try
            {
                var res = context.Books.ToList();
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void updatebook(Book book)
        {
            try
            {
                if (book != null)
                {
                    context.Books.Update(book);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}

