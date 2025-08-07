using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repository
{
    public class BookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> SearchAsync(string? keyword, int page, int pageSize)
        {
            var query = _context.Books
                .Where(b => b.IsActive)
                .AsQueryable()
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(b =>
                     b.Title.ToLower().Contains(keyword) ||
                     b.Author.ToLower() == keyword);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync(string? keyword)
        {
            var query = _context.Books.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(b => b.Title.Contains(keyword) || b.Author.Contains(keyword));
            }

            return await query.CountAsync();
        }

        // Vi phạm OCP: Nếu cần thêm logic khác (ví dụ lọc theo thể loại),
        // phải sửa đổi trực tiếp class này, thay vì mở rộng từ base hoặc interface.
    }
}
