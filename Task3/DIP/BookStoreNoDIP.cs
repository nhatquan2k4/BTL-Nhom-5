// Demo code KHÔNG tuân thủ DIP (Dependency Inversion Principle) cho cửa hàng bán sách

using System;
using System.Collections.Generic;

// 1. Entities: Định nghĩa các class thực thể

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Quantity { get; set; }

    public Book(int id, string title, string author, int quantity)
    {
        Id = id;
        Title = title;
        Author = author;
        Quantity = quantity;
    }
}

// 2. Implementations: Không dùng abstraction, code phụ thuộc trực tiếp vào class cụ thể

public class BookRepository
{
    private List<Book> books = new List<Book>();

    public List<Book> GetAllBooks()
    {
        return books;
    }

    public Book GetBookById(int id)
    {
        return books.Find(b => b.Id == id);
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }
}

public class OrderService
{
    // Phụ thuộc trực tiếp vào class cụ thể (BookRepository), không có interface
    private BookRepository bookRepository;

    public OrderService()
    {
        // Khởi tạo trực tiếp bên trong class này, không inject từ ngoài vào
        bookRepository = new BookRepository();
    }

    public void PlaceOrder(int bookId, int quantity)
    {
        Book book = bookRepository.GetBookById(bookId);
        if (book == null)
        {
            Console.WriteLine("Book not found!");
            return;
        }
        if (book.Quantity < quantity)
        {
            Console.WriteLine("Not enough books in stock.");
            return;
        }
        book.Quantity -= quantity;
        Console.WriteLine($"Order placed for {quantity} '{book.Title}'");
    }

    // Cho phép thêm sách thông qua service này (ví dụ minh họa)
    public void AddBook(Book book)
    {
        bookRepository.AddBook(book);
    }

    public List<Book> GetAllBooks()
    {
        return bookRepository.GetAllBooks();
    }
}

// 3. Presentation/UI: Chạy demo

public class Program
{
    public static void Main(string[] args)
    {
        // OrderService tự tạo BookRepository, không inject, vi phạm DIP
        OrderService orderService = new OrderService();

        // Thêm sách và đặt hàng
        orderService.AddBook(new Book(1, "C# Programming", "Nguyen Van A", 10));
        orderService.AddBook(new Book(2, "Design Patterns", "Le Van B", 5));
        orderService.AddBook(new Book(3, "Clean Code", "Tran Thi C", 8));

        Console.WriteLine("Danh sách sách hiện có:");
        foreach (var book in orderService.GetAllBooks())
        {
            Console.WriteLine($"Id: {book.Id}, Tiêu đề: {book.Title}, Tác giả: {book.Author}, Số lượng: {book.Quantity}");
        }

        orderService.PlaceOrder(2, 3);

        Console.WriteLine("\nDanh sách sách sau khi đặt hàng:");
        foreach (var book in orderService.GetAllBooks())
        {
            Console.WriteLine($"Id: {book.Id}, Tiêu đề: {book.Title}, Tác giả: {book.Author}, Số lượng: {book.Quantity}");
        }
    }
}