// Demo DIP (Dependency Inversion Principle) cho cửa hàng bán sách

using System;
using System.Collections.Generic;

// 1. Abstraction: Định nghĩa các interface

// Định nghĩa 1 interface cho Book Repository
public interface IBookRepository
{
    List<Book> GetAllBooks();
    Book GetBookById(int id);
    void AddBook(Book book);
}

// Định nghĩa 1 interface cho Order Service
public interface IOrderService
{
    void PlaceOrder(int bookId, int quantity);
}

// 2. Entities: Định nghĩa các class thực thể

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

// 3. Implementations: Cài đặt các interface

// Cài đặt Book Repository
public class MemoryBookRepository : IBookRepository
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

// Cài đặt Order Service
public class OrderService : IOrderService
{
    private readonly IBookRepository bookRepository;

    // Nhận dependency thông qua constructor (DIP)
    public OrderService(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
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
}

// 4. Presentation/UI: Chạy demo

public class Program
{
    public static void Main(string[] args)
    {
        // Khởi tạo repository và service, inject dependency
        IBookRepository bookRepo = new MemoryBookRepository();
        IOrderService orderService = new OrderService(bookRepo);

        // Thêm một số sách vào kho
        bookRepo.AddBook(new Book(1, "C# Programming", "Nguyen Van A", 10));
        bookRepo.AddBook(new Book(2, "Design Patterns", "Le Van B", 5));
        bookRepo.AddBook(new Book(3, "Clean Code", "Tran Thi C", 8));

        // Hiển thị danh sách sách
        Console.WriteLine("Danh sách sách hiện có:");
        foreach (var book in bookRepo.GetAllBooks())
        {
            Console.WriteLine($"Id: {book.Id}, Tiêu đề: {book.Title}, Tác giả: {book.Author}, Số lượng: {book.Quantity}");
        }

        // Đặt hàng một cuốn sách
        orderService.PlaceOrder(2, 3);

        // Kiểm tra lại số lượng sau khi đặt hàng
        Console.WriteLine("\nDanh sách sách sau khi đặt hàng:");
        foreach (var book in bookRepo.GetAllBooks())
        {
            Console.WriteLine($"Id: {book.Id}, Tiêu đề: {book.Title}, Tác giả: {book.Author}, Số lượng: {book.Quantity}");
        }
    }
}