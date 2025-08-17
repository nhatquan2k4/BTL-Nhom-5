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



# Nghiên cứu sâu về DIP

### 1. **Tổng quan về DIP và SOLID**

**SOLID** là bộ nguyên tắc thiết kế OOP do *Robert C. Martin (Uncle Bob)* đề xuất, giúp code sạch hơn, dễ bảo trì và mở rộng. Các nguyên tắc bao gồm:

- **S**: Single Responsibility Principle (Mỗi lớp chỉ làm một việc).
- **O**: Open-Closed Principle (Mở cho mở rộng, đóng cho sửa đổi).
- **L**: Liskov Substitution Principle (Lớp con có thể thay thế lớp cha mà không làm hỏng chương trình).
- **I**: Interface Segregation Principle (Giao diện nên nhỏ và cụ thể).
- **D**: Dependency Inversion Principle (DIP) – Nguyên tắc chúng ta sẽ tập trung.

**DIP - Dependency Inversion Principle (Nguyên lý đảo ngược phụ thuộc)** được phát biểu:  

- Các **module cấp cao (high-level modules)** không nên phụ thuộc vào các **module cấp thấp (low-level modules)**. Cả hai nên phụ thuộc vào **abstraction** (trừu tượng hóa, thường là interface hoặc abstract class).  
- Abstraction không nên phụ thuộc vào chi tiết cụ thể; chi tiết cụ thể nên phụ thuộc vào abstraction.  

  - **Module cấp cao (High-level modules):** chứa logic nghiệp vụ chính, định nghĩa quy tắc.  
  - **Module cấp thấp (Low-level modules):** chứa chi tiết triển khai, thao tác trực tiếp với hệ thống (database, API, file, v.v.).  

👉 Nói đơn giản: Thay vì lớp **A** trực tiếp phụ thuộc vào lớp **B** cụ thể, hãy để **A** phụ thuộc vào một **interface** mà **B** implement.  
Điều này giúp **decoupling** (giảm sự phụ thuộc chặt chẽ), dễ thay thế component, và dễ test (ví dụ: dùng mock trong unit test).  

Trong lập trình phía **server** (backend như Node.js, Java Spring, .NET, Python Django, v.v.), DIP rất hữu ích vì:

- Server thường xử lý nhiều layer: **Controller** (xử lý request), **Service** (logic business), **Repository** (kết nối DB).  
- Không áp dụng DIP dẫn đến code *cứng nhắc*, khó scale (ví dụ: thay đổi DB từ MySQL sang MongoDB sẽ phải sửa nhiều nơi).  

---

### 2. **Tại sao DIP quan trọng trong lập trình phía server?**

- **Decoupling**: Giảm rủi ro khi thay đổi. Ví dụ, trong một API server, nếu service layer phụ thuộc trực tiếp vào một DB cụ thể, việc migrate DB sẽ đau đầu.  
- **Testability**: Dễ viết unit test bằng cách inject mock objects.  
- **Scalability**: Dễ mở rộng hệ thống, ví dụ thêm caching hoặc logging mà không ảnh hưởng core logic.  
- **Ví dụ thực tế**: Trong microservices, các service giao tiếp qua **interface trừu tượng**, không phụ thuộc implementation cụ thể.  

❌ Không áp dụng DIP có thể dẫn đến *spaghetti code* – code rối rắm, khó debug, đặc biệt ở server-side nơi xử lý **concurrent requests**, **security**, và **integration với external services** (như API bên thứ ba).  

---

### 3. **Cách áp dụng DIP**

DIP thường được thực hiện qua **Dependency Injection (DI)** – một pattern để *tiêm* dependencies vào class thay vì class tự tạo chúng.  

Ba loại DI phổ biến:  
- **Constructor Injection**: Tiêm qua constructor (phổ biến nhất).  
- **Setter Injection**: Tiêm qua setter method.  
- **Method Injection**: Tiêm qua parameter của method.  

Các framework server-side hỗ trợ DI tốt:  
- **Java**: Spring Boot (sử dụng `@Autowired`).  
- **.NET**: ASP.NET Core (built-in DI container).  
- **Node.js**: Không built-in, nhưng dùng thư viện như **InversifyJS** hoặc tự implement.  
- **Python**: Flask/Django có extension cho DI.  

---

### 4. **Ưu nhược điểm của DIP**

- **Ưu điểm**:  
  - Tăng tính linh hoạt và tái sử dụng code.  
  - Dễ dàng thay thế, mở rộng (chỉ cần viết class mới implement interface).  
  - Dễ test (sử dụng Mockito ở Java, Jest mocks ở JS).  
  - Phù hợp với kiến trúc như **Hexagonal (Ports & Adapters)** ở server-side.  

- **Nhược điểm**:  
  - Tăng độ phức tạp ban đầu (thêm interface, DI container).  
  - Overkill cho project nhỏ.  
  - Cần học thêm về DI containers (như Spring IoC).  
