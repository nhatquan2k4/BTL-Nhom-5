# Giới thiệu về LSP và ISP trong SOLID

## 1. LSP - Liskov Substitution Principle (Nguyên lý thay thế Liskov)

### Định nghĩa
**LSP** phát biểu rằng:
> "Các lớp dẫn xuất (subclass) phải có thể thay thế cho lớp cơ sở (base class) mà không làm thay đổi tính đúng đắn của chương trình."

Nói cách khác, nếu một class kế thừa từ class cha, thì ở mọi nơi sử dụng class cha, bạn đều có thể thay thế bằng class con mà chương trình vẫn hoạt động đúng.

### Ý nghĩa
- Đảm bảo tính kế thừa đúng nghĩa trong OOP.
- Giúp code dễ mở rộng, bảo trì, tránh lỗi tiềm ẩn khi thay thế subclass.

### Dấu hiệu vi phạm LSP
- Subclass ghi đè phương thức nhưng thay đổi hành vi không mong muốn.
- Subclass throw lỗi hoặc không hỗ trợ đầy đủ các chức năng của class cha.
- Khi thay thế subclass, chương trình bị lỗi hoặc kết quả sai.


---

## 2. ISP - Interface Segregation Principle (Nguyên lý phân tách giao diện)

### Định nghĩa
**ISP** phát biểu rằng:
> "Không nên ép một class phải implement những phương thức mà nó không sử dụng."

Nói cách khác, hãy chia nhỏ các interface thành các nhóm chức năng riêng biệt, thay vì tạo một interface lớn cho nhiều mục đích.

### Ý nghĩa
- Giúp các class chỉ phụ thuộc vào những gì chúng cần.
- Tránh việc implement các phương thức thừa, không cần thiết.
- Code dễ bảo trì, mở rộng, giảm rủi ro lỗi.

### Dấu hiệu vi phạm ISP
- Interface có quá nhiều phương thức, class implement phải viết các phương thức không dùng.
- Khi thêm chức năng mới, phải sửa nhiều class không liên quan.


---

## 3. Tổng kết

- **LSP** giúp đảm bảo tính kế thừa đúng nghĩa, tránh subclass làm sai lệch hành vi của class cha.
- **ISP** giúp thiết kế interface rõ ràng, mỗi class chỉ cần implement những gì nó thực sự sử dụng.

Tuân thủ LSP và ISP giúp hệ thống phần mềm dễ mở rộng, bảo trì, giảm lỗi và tăng tính linh hoạt trong thiết kế hướng