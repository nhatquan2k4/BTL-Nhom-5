# Nguyên Lý DIP (Dependency Inversion Principle) - Lý thuyết & Ví dụ

## 1. Định nghĩa DIP

**DIP (Dependency Inversion Principle)** là một trong 5 nguyên lý SOLID trong lập trình hướng đối tượng.

> **"Các module cấp cao không nên phụ thuộc vào các module cấp thấp. Cả hai nên phụ thuộc vào abstraction. Abstraction không nên phụ thuộc vào chi tiết, mà chi tiết nên phụ thuộc vào abstraction."**

- **Module cấp cao:** Chứa logic nghiệp vụ chính (business logic).
- **Module cấp thấp:** Xử lý chi tiết kỹ thuật (database, file, API…).
- **Abstraction:** Interface hoặc abstract class.


---

## 2. Tầm quan trọng của DIP

- **Tăng khả năng mở rộng:** Dễ thay đổi, mở rộng chi tiết mà không ảnh hưởng logic chính.
- **Tăng tái sử dụng:** Module cấp cao có thể tái sử dụng ở nhiều nơi khác nhau.
- **Dễ kiểm thử:** Có thể mock các dependency để test dễ dàng.
- **Giảm phụ thuộc cứng (tight coupling):** Dễ bảo trì, nâng cấp hệ thống.


---

## 3. Ví dụ về DIP

### A. Ví dụ vi phạm DIP

```csharp
public class BookRepository
{
    public Book GetBookById(int id) { /*...*/ }
    // ...
}

public class OrderService
{
    private BookRepository repo = new BookRepository(); // Phụ thuộc trực tiếp

    public void PlaceOrder(int bookId, int quantity)
    {
        var book = repo.GetBookById(bookId);
        // Xử lý đặt hàng
    }
}
```
**→ `OrderService` phụ thuộc trực tiếp vào `BookRepository` (module cấp thấp).**

---


### B. Ví dụ tuân thủ DIP


```csharp
public interface IBookRepository
{
    Book GetBookById(int id);
    // ...
}

public class MemoryBookRepository : IBookRepository
{
    public Book GetBookById(int id) { /*...*/ }
    // ...
}

public class OrderService
{
    private IBookRepository repo;

    public OrderService(IBookRepository repo) // Inject abstraction
    {
        this.repo = repo;
    }

    public void PlaceOrder(int bookId, int quantity)
    {
        var book = repo.GetBookById(bookId);
        // Xử lý đặt hàng
    }
}
```
**→ `OrderService` chỉ phụ thuộc vào abstraction (`IBookRepository`).**

---

## 4. Sơ đồ minh họa DIP

### A. **Vi phạm DIP**

```
OrderService  --->  BookRepository
(module cấp cao)    (module cấp thấp)
```

### B. **Tuân thủ DIP**

```
                +----------------------+
                |   IBookRepository    |  (Abstraction)
                +----------------------+
                        ^
                        |
            +--------------------------+
            |  MemoryBookRepository    |  (chi tiết)
            +--------------------------+
                        ^
                        |
                +-------------------+
                |   OrderService    |   (module cấp cao)
                +-------------------+
```

## 5. Tổng kết

- **DIP** giúp code linh hoạt, dễ mở rộng, giảm phụ thuộc cứng giữa các module.
- **Tuân thủ DIP:** Module cấp cao chỉ phụ thuộc abstraction (interface/abstract class), không dựa trực tiếp vào module cấp thấp.
- **Vi phạm DIP:** Module cấp cao gọi thẳng module cấp thấp, khó mở rộng/bảo trì.

---