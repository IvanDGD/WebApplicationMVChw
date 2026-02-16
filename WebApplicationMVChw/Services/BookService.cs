using WebApplicationMVChw.Models;

namespace WebApplicationMVChw.Services
{
    public class BookService
    {
        private readonly List<Book> books = new();
        private int nextId = 1;

        public List<Book> GetAll()
        {
            return books;
        }

        public void Add(Book book)
        {
            book.Id = nextId++;
            books.Add(book);
        }
    }
}
