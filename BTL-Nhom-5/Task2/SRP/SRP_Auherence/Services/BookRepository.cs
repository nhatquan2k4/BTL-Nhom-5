using SRP_Adherence.Models;

namespace SRP_Adherence.Services
{
    // Chỉ chịu trách nhiệm lưu trữ
    public class BookRepository
    {
        private readonly List<Book> _books = new();

        public void Add(Book book)
        {
            _books.Add(book);
            Console.WriteLine($"[DB] Đã thêm sách: {book.Title}");
        }

        public List<Book> GetAllActiveBooks()
        {
            return _books.Where(b => b.IsActive).ToList();
        }
    }
}
