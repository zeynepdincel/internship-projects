using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    internal class Program
    {
        static Library library = new Library();
        static void Main(string[] args)
        {
            while (true)
            {


                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Delete Book");
                Console.WriteLine("3. Borrow a Book");
                Console.WriteLine("4. Return a Book");
                Console.WriteLine("5. View Books");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        Console.WriteLine("title: ");
                        string title = Console.ReadLine();
                        Console.WriteLine("author: ");
                        string author = Console.ReadLine();
                        int id = library.Books.Count + 1;
                        library.AddBook(id, title, author);
                        break;
                    case 2:
                        Console.WriteLine("Enter book id: ");
                        int bookId = Convert.ToInt32(Console.ReadLine());
                        library.DeleteBook(bookId);
                        break;
                    case 3:
                        Console.WriteLine("Enter user id: ");
                        int userId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter book id: ");
                        bookId = Convert.ToInt32(Console.ReadLine());
                        library.ReturnBook(userId, bookId);
                        break;
                    case 4:
                        Console.WriteLine("Enter user id: ");
                        int userID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter book id: ");
                        bookId = Convert.ToInt32(Console.ReadLine());
                        library.ReturnBook(userID, bookId);
                        break;
                    case 5:
                        library.ViewBooks();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
        class Book
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public bool IsBorrowed { get; set; }

            public Book(int id, string title, string author)
            {
                Id = id;
                Title = title;
                Author = author;
                IsBorrowed = false;
            }
        }
        class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<Book> BorrowedBooks { get; set; }
            public User(int id, string name)
            {
                Id = id;
                Name = name;
                BorrowedBooks = new List<Book>();
            }

        }
        class Library
        {
            public List<Book> Books = new List<Book>();
            public List<User> Users = new List<User>();
            public Library()
            {
                Books = new List<Book>();
                Users = new List<User>();
            }
            public void AddBook(int id, string title, string author)
            {
                Books.Add(new Book(id, title, author));
            }
            public void DeleteBook(int id)
            {
                Book book = Books.FirstOrDefault(b => b.Id == id);
                if (book != null)
                {
                    Books.Remove(book);
                }
                else
                {
                    Console.WriteLine("Book not found");
                }
            }
            public void BorrowBook(int userId, int bookId)
            {
                User user = Users.FirstOrDefault(u => u.Id == userId);
                Book book = Books.FirstOrDefault(b => b.Id == bookId);
                if (user != null && book != null)
                {
                    if (book.IsBorrowed)
                    {
                        Console.WriteLine("Book is already borrowed");
                    }
                    else
                    {
                        book.IsBorrowed = true;
                        user.BorrowedBooks.Add(book);
                    }
                }
                else
                {
                    Console.WriteLine("User or Book not found");
                }
            }
            public void ReturnBook(int userId, int bookId)
            {
                User user = Users.FirstOrDefault(u => u.Id == userId);
                Book book = Books.FirstOrDefault(b => b.Id == bookId);
                if (user != null && book != null)
                {
                    if (book.IsBorrowed)
                    {
                        book.IsBorrowed = false;
                        user.BorrowedBooks.Remove(book);
                    }
                    else
                    {
                        Console.WriteLine("Book is not borrowed");
                    }
                }
                else
                {
                    Console.WriteLine("User or Book not found");
                }
            }
            public void ViewBooks()
            {
                foreach (var book in Books)
                {
                    Console.WriteLine($"Id: {book.Id}, Title: {book.Title}, Author: {book.Author}");
                }
                if(Books.Count == 0)
                {
                    Console.WriteLine("No books available");
                }
            }

        }
    }
}

