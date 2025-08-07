# Phân Tích Nguyên Lý OCP (Open/Closed Principle)

## Mục Tiêu

Báo cáo này trình bày chi tiết về:

- Nguyên lý OCP trong SOLID
- So sánh giữa 2 đoạn code Demo của Project Quản Lý Sách:
  - Một phiên bản **tuân thủ OCP**
  - Một phiên bản **vi phạm OCP**

---

## OCP là gì?

**Open/Closed Principle (OCP)** là nguyên lý trong SOLID phát biểu:

> “Entities (class, module, function) nên được *mở rộng (Open for extension)* nhưng *đóng với việc sửa đổi (Closed for modification)*.”

### Mục tiêu của OCP:

- Cho phép **thêm tính năng mới** mà **không cần sửa mã gốc**
- **Giảm rủi ro phát sinh lỗi** khi sửa đổi code đang hoạt động ổn
- **Dễ dàng bảo trì**, mở rộng hệ thống

---

## TUÂN THỦ OCP (`OCP_demo.cs`)

### Code demo

```csharp
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
}
```

### Đoạn code trên tuân thủ OCP vì:

| Điểm                        | Mô tả
| --------------------------- | --------------------------------------------------------------------------- |
| **Kế thừa**              | Kế thừa `GenericRepository<Book>` để sử dụng lại logic CRUD chung                              |
| **Triển khai interface** | `IBookRepository` giúp dễ dàng thay đổi/hoán đổi implement mà không thay đổi nơi sử dụng       |
| **Có thể mở rộng**       | Có thể thêm chức năng mới (như SearchByCategory) bằng cách tạo method mới mà không sửa code cũ |
| **Dễ test/mock**         | Có interface hỗ trợ mock trong unit test                                                       |


## KHÔNG TUÂN THỦ OCP (`notOCP_demo.cs`)
```csharp
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
}
```

### Đoạn code trên không tuân thủ OCP vì:
| Vấn đề                                  | Giải thích      |
| --------------------------------------- | ----------------------------------------------------------------- |
| Không kế thừa `GenericRepository<T>` | Logic CRUD bị lặp lại, không tái sử dụng                          |
| Không có interface                   | Không có tính trừu tượng, gây khó khăn cho test, mở rộng          |
| Sửa trực tiếp                        | Muốn thêm filter theo Category/Publisher phải **sửa thẳng class** |
| Không tái sử dụng                    | Dễ sinh lỗi, khó bảo trì khi thay đổi yêu cầu                     |

## TỔNG KẾT

| Tiêu chí                         | Tuân Thủ OCP (`OCP_demo.cs`)  | Vi Phạm OCP (`notOCP_demo.cs`)  |
| -------------------------------- | ------------------------------ | -------------------------------- |
| Kế thừa base class tái sử dụng   |  Có                           |  Không                          |
| Sử dụng interface (trừu tượng)   |  Có                           |  Không                          |
| Dễ dàng mở rộng chức năng mới    |  Không cần sửa                |  Phải sửa class                 |
| Hỗ trợ test/mock dễ dàng         |  Có thể mock interface        |  Rất khó test                   |
