# 1. Khái niệm về SOLID

**SOLID** là tập hợp 5 nguyên lý thiết kế hướng đối tượng giúp lập trình viên xây dựng phần mềm dễ bảo trì, mở rộng và linh hoạt. SOLID gồm:

- **S**: Single Responsibility Principle (Nguyên lý trách nhiệm duy nhất)
- **O**: Open/Closed Principle (Nguyên lý mở rộng/đóng)
- **L**: Liskov Substitution Principle (Nguyên lý thay thế Liskov)
- **I**: Interface Segregation Principle (Nguyên lý phân tách giao diện)
- **D**: Dependency Inversion Principle (Nguyên lý đảo ngược phụ thuộc)

## Tại sao SOLID quan trọng trong thiết kế phần mềm hệ thống?

- Giảm sự phụ thuộc lẫn nhau giữa các module (loose coupling).
- Tăng khả năng mở rộng, bảo trì và tái sử dụng code.
- Giúp kiểm thử (test) dễ dàng hơn.
- Giảm rủi ro phát sinh lỗi khi thay đổi hoặc mở rộng hệ thống.

---

# 2. Nguyên lý S: Single Responsibility Principle (SRP) - Nguyên lý trách nhiệm duy nhất

## 1: Định nghĩa nguyên lý S

**Nguyên lý S** phát biểu:

> "Mỗi class chỉ nên có một lý do để thay đổi."  
> Nói cách khác, mỗi class chỉ nên đảm nhận một trách nhiệm (một chức năng chính) duy nhất.

## 2: Dấu hiệu vi phạm nguyên lý này

- Class thực hiện nhiều chức năng không liên quan (ví dụ: vừa xử lý logic nghiệp vụ, vừa ghi log, vừa lưu file).
- Khi thay đổi một yêu cầu nhỏ (ví dụ: thay đổi cách log), phải sửa nhiều chỗ trong class.
- Class có quá nhiều phương thức, hoặc các phương thức có các lý do thay đổi khác nhau.

### Ghi chú: Vì sao áp dụng SRP lại tốt hơn?

- **Dễ bảo trì:** Khi có thay đổi về logic ở một trách nhiệm nào đó (ví dụ: thay đổi cách lưu file), chỉ cần sửa ở class tương ứng, không ảnh hưởng đến các phần khác.
- **Dễ mở rộng:** Có thể thêm chức năng mới mà không làm rối loạn, xung đột các chức năng hiện tại.
- **Dễ kiểm thử:** Mỗi class nhỏ, độc lập sẽ dễ viết unit test.
- **Giảm xung đột khi làm việc nhóm:** Nhiều người có thể làm việc song song trên các phần khác nhau mà ít bị trùng code.
- **Code rõ ràng, dễ đọc:** Khi nhìn vào một class, bạn dễ dàng hiểu nó đang đảm nhận vai trò gì.

## 3: Cách áp dụng trong thực tế

- Xác định rõ trách nhiệm của mỗi class trước khi thiết kế.
- Nếu thấy một class có nhiều lý do để thay đổi, hãy tách thành các class nhỏ hơn.
- Thường xuyên review code để phát hiện các class "God Object" (làm quá nhiều việc).

## 4L Ví dụ minh họa

---

# 3. Nguyên lý O: Open/Closed Principle (OCP) - Nguyên lý mở rộng/đóng

## 1: Định nghĩa nguyên lý O

**Nguyên lý O** phát biểu:

> "Các thực thể phần mềm (class, module, function) nên đóng để sửa đổi, nhưng mở để mở rộng."  
> Nói cách khác, bạn có thể mở rộng hành vi của class mà không cần thay đổi mã nguồn gốc.

## 2: "Mở để mở rộng, đóng để sửa đổi" nghĩa là gì?

- **Đóng để sửa đổi:** Không sửa đổi code đã tồn tại (tránh ảnh hưởng đến các module khác, hạn chế bug).
- **Mở để mở rộng:** Có thể thêm chức năng mới bằng cách mở rộng (subclass, implement interface, sử dụng strategy pattern, v.v.), không động vào code cũ.

## 3: Pattern liên quan

- **Strategy Pattern**
- **Template Method Pattern**
- **Decorator Pattern**
- **Factory Method Pattern**

Các pattern này cho phép mở rộng hành vi mà không cần sửa đổi lớp gốc.

## 4: Ví dụ minh họa

---

### TUÂN THỦ OCP (`OCP_demo.cs`)

#### Code demo

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

#### Đoạn code trên tuân thủ OCP vì:

| Điểm                     | Mô tả                                                                                          |
| ------------------------ | ---------------------------------------------------------------------------------------------- |
| **Kế thừa**              | Kế thừa `GenericRepository<Book>` để sử dụng lại logic CRUD chung                              |
| **Triển khai interface** | `IBookRepository` giúp dễ dàng thay đổi/hoán đổi implement mà không thay đổi nơi sử dụng       |
| **Có thể mở rộng**       | Có thể thêm chức năng mới (như SearchByCategory) bằng cách tạo method mới mà không sửa code cũ |
| **Dễ test/mock**         | Có interface hỗ trợ mock trong unit test                                                       |

### KHÔNG TUÂN THỦ OCP (`notOCP_demo.cs`)

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

#### Đoạn code trên không tuân thủ OCP vì:

| Vấn đề                               | Giải thích                                                        |
| ------------------------------------ | ----------------------------------------------------------------- |
| Không kế thừa `GenericRepository<T>` | Logic CRUD bị lặp lại, không tái sử dụng                          |
| Không có interface                   | Không có tính trừu tượng, gây khó khăn cho test, mở rộng          |
| Sửa trực tiếp                        | Muốn thêm filter theo Category/Publisher phải **sửa thẳng class** |
| Không tái sử dụng                    | Dễ sinh lỗi, khó bảo trì khi thay đổi yêu cầu                     |

### TỔNG KẾT

| Tiêu chí                       | Tuân Thủ OCP (`OCP_demo.cs`) | Vi Phạm OCP (`notOCP_demo.cs`) |
| ------------------------------ | ---------------------------- | ------------------------------ |
| Kế thừa base class tái sử dụng | Có                           | Không                          |
| Sử dụng interface (trừu tượng) | Có                           | Không                          |
| Dễ dàng mở rộng chức năng mới  | Không cần sửa                | Phải sửa class                 |
| Hỗ trợ test/mock dễ dàng       | Có thể mock interface        | Rất khó test                   |
