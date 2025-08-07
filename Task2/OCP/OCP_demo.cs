using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Book>> SearchAsync(string? keyword, int page, int pageSize)
        {
            var query = _context.Books
                .Where(b => b.IsActive) // chỉ lấy sách chưa bị xóa mềm
                .AsQueryable().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(b =>
                     b.Title.ToLower().Contains(keyword) || // cho tìm gần đúng tiêu đề
                    b.Author.ToLower() == keyword);        // chính xác tên tác giả
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> CountAsync(string? keyword)
        {
            var query = _context.Books.AsNoTracking(); //Truy vấn tổng hợp

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(b => b.Title.Contains(keyword) || b.Author.Contains(keyword));
            }

            return await query.CountAsync();
        }
    }
}