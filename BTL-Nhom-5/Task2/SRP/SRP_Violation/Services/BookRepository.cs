using SRP_Violation.Models;

namespace SRP_Violation.Services
{
    // Vi phạm SRP: Class này vừa lưu trữ vừa tìm kiếm
    public class BookRepository
    {
        private readonly List<Book> _books = new();

        public void Add(Book book)
        {
            _books.Add(book);
            Console.WriteLine($"[DB] Đã thêm sách: {book.Title}");
        }

        public List<Book> Search(string? keyword)
        {
            var query = _books.Where(b => b.IsActive);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(b =>
                    b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }
    }
}