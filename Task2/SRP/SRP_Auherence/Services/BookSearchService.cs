using SRP_Adherence.Models;

namespace SRP_Adherence.Services
{
    // Chỉ chịu trách nhiệm tìm kiếm sách
    public class BookSearchService
    {
        public List<Book> Search(List<Book> books, string? keyword)
        {
            var query = books.AsEnumerable();

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
