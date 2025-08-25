📚 BookStore

là hệ thống quản lý và bán sách trực tuyến được thiết kế theo kiến trúc hiện đại, tách biệt rõ ràng giữa frontend và backend, hỗ trợ mở rộng và phân quyền linh hoạt.

💡 Các tính năng chính

✅ Backend (.NET 8 + Clean Architecture + Modular Schema)

API RESTful cho quản lý người dùng, sách, đơn hàng...

Áp dụng Domain-Driven Design (DDD), chia module độc lập theo schema

Hỗ trợ đa ngôn ngữ, phân quyền người dùng (Admin, Staff, User)

Tích hợp OpenIddict, Middleware chuẩn hóa lỗi, DTO Validation...

✅ Frontend (Angular)

Giao diện thân thiện, responsive

Trang quản lý người dùng, giỏ hàng, đặt sách

Giao tiếp với backend qua HTTPClient + Interceptors

Dễ dàng mở rộng thêm dashboard, trang admin...

🧩 Công nghệ sử dụng

.NET 8, Entity Framework Core, OpenIddict

Angular 17, TypeScript, RxJS, SCSS

SQL Server, Docker , Swagger, Mapper

📂 Schema tách biệt

book: Bảng và logic quản lý sách

user: Người dùng và phân quyền

order: Đơn hàng, giao dịch

core: Tenant, Log hệ thống
