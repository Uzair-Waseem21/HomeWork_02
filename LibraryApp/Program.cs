using System;
using BookLibraryModels;
using BookLibraryRepository;
using BackupHelper;
int choice = -1;

BookRepository book = new BookRepository();
MemberRepository member = new MemberRepository();
IssueRepository issuebook = new IssueRepository();
Backup backup = new Backup();

while (choice != 0)
{
  Console.WriteLine("\n--- Library Management System ---");
  Console.WriteLine("1. Add Book");
  Console.WriteLine("2. View All Books");
  Console.WriteLine("3. Update Book Stock");
  Console.WriteLine("4. Delete Book");
  Console.WriteLine("5. Add Member");
  Console.WriteLine("6. View All Members");
  Console.WriteLine("7. Issue Book");
  Console.WriteLine("8. Return Book");
  Console.WriteLine("9. Export Books to JSON");
  Console.WriteLine("10. Import Books From JSON");
  Console.WriteLine("0. Exit");

  Console.Write("Enter your choice: ");
  choice = int.Parse(Console.ReadLine());

  if (choice == 1)
  {
    Console.WriteLine("Add Book Selected");
    Book book1 = new Book();
    Console.WriteLine("Enter Book Title : ");
    book1.Title = Console.ReadLine();
    Console.WriteLine("Enter Book Author : ");
    book1.Author = Console.ReadLine();
    Console.WriteLine("Enter Book Quantity : ");
    book1.Stock = int.Parse(Console.ReadLine());
    bool status = book.AddBook(book1);
    if (status)
    {
      Console.WriteLine("Book Added Successfully!");
    }
    else
    {
      Console.WriteLine("Book could not be added!");
    }
  }
  else if (choice == 2)
  {
    Console.WriteLine("View All Books Selected");
    List<Book> record = book.GetAllBooks();
    if (record.Count() == 0)
    {
      Console.WriteLine("No Books found!");
    }
    else
    {
      foreach (Book b in record)
      {
        Console.WriteLine("Book Id: " + b.BookId);
        Console.WriteLine("Book Title: " + b.Title);
        Console.WriteLine("Book Author: " + b.Author);
        Console.WriteLine("Book Quantity: " + b.Stock + "\n\n");
      }
    }
  }
  else if (choice == 3)
  {
    Console.WriteLine("Update Book Stock Selected");
    Console.Write("Enter Book ID: ");
    int bookId = int.Parse(Console.ReadLine());
    Console.Write("Enter New Stock Quantity: ");
    int stock = int.Parse(Console.ReadLine());
    bool status = book.UpdateStock(bookId, stock);

    if (status)
    {
      Console.WriteLine("Stock Updated Successfully!");
    }
    else
    {
      Console.WriteLine("Stock could not be updated!");
    }
  }
  else if (choice == 4)
  {
    Console.WriteLine("Delete Book Selected");
    Console.Write("Enter Book ID: ");
    int bookId = int.Parse(Console.ReadLine());
    bool status = book.DeleteBook(bookId);
    if (status)
    {
      Console.WriteLine("Book Deleted Successfully!");
    }
    else
    {
      Console.WriteLine("Book could not be deleted!");
    }
  }
  else if (choice == 5)
  {
    Console.WriteLine("Add Member Selected");
    Member members = new Member();
    Console.WriteLine("Enter Member Name : ");
    members.Name = Console.ReadLine();
    bool status = member.AddMember(members);
    if (status)
    {
      Console.WriteLine("Member Added Successfully!");
    }
    else
    {
      Console.WriteLine("Member could not be Added!");
    }
  }
  else if (choice == 6)
  {
    Console.WriteLine("View All Members Selected");
    List<Member> members = member.GetAllMembers();
    if (members.Count() == 0)
    {
      Console.WriteLine("No Members found!");
    }
    else
    {
      foreach (Member m in members)
      {
        Console.WriteLine("Member Id: " + m.MemberId);
        Console.WriteLine("Member Name: " + m.Name + "\n\n");
      }
    }
  }
  else if (choice == 7)
  {
    Console.WriteLine("Issue Book Selected");
    Console.Write("Enter Book id: ");
    int bookid = int.Parse(Console.ReadLine());
    Console.Write("Enter Member id: ");
    int memberid = int.Parse(Console.ReadLine());
    bool status = issuebook.IssueBook(bookid, memberid);
    if (status)
    {
      Console.WriteLine("Book Issued Successfully!");
    }
    else
    {
      Console.WriteLine("Book could not be Issued!");
    }
  }
  else if (choice == 8)
  {
    Console.WriteLine("Return Book Selected");
    Console.Write("Enter issue id: ");
    int issueid = int.Parse(Console.ReadLine());
    bool status = issuebook.ReturnBook(issueid);
    if (status)
    {
      Console.WriteLine("Book Returned Successfully!");
    }
    else
    {
      Console.WriteLine("No Issued Book with id " + issueid + " found!");
    }

  }

  else if (choice == 9)
  {
    Console.WriteLine("Export Books to JSON Selected");
    List<Book> books = book.GetAllBooks();  
    backup.ExportBooks(books, "backup.json");
    Console.WriteLine("Books exported successfully!");
  }
  else if (choice == 10)
  {
    Console.WriteLine("Import Books from JSON Selected");
    List<Book> books = backup.ImportBooks("backup.json");
    if (books.Count() == 0)
    {
      Console.WriteLine("No Backup Found! ");
    }
    else
    {
      foreach (Book b in books)
      {
        Console.WriteLine("Book Title: " + b.Title);
        Console.WriteLine("Book Quantity: " + b.Stock + "\n");
      }
    }
  }
  else if (choice == 0)
  {
    Console.WriteLine("Exiting...");
  }
  else
  {
    Console.WriteLine("Invalid choice!");
  }
}

Console.WriteLine("Program ended!");


