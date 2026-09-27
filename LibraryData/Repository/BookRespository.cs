using MySql.Data.MySqlClient;
using System;
using BookLibraryModels;
namespace BookLibraryRepository
{

  public class BookRepository
  {
    public bool AddBook(Book book)
    {
      string connectionString = "Server=localhost;Port=3306;Database=libraryDB;User ID=root;Password=admin;";
      MySqlConnection con = new MySqlConnection(connectionString);
      if (string.IsNullOrWhiteSpace(book.Title))
      {
        return false;
      }
      if (string.IsNullOrWhiteSpace(book.Author))
      {
        return false;
      }
      if (book.Stock < 0)
      {
        return false;
      }
      con.Open();
      string query = "INSERT INTO books (Title, Author, Stock) VALUES (@Title, @Author, @Stock)";
      MySqlCommand command = new MySqlCommand(query, con);
      command.Parameters.AddWithValue("@Title", book.Title);
      command.Parameters.AddWithValue("@Author", book.Author);
      command.Parameters.AddWithValue("@Stock", book.Stock);
      int rowsAffected = command.ExecuteNonQuery();
      con.Close();
      return rowsAffected > 0;
    }
    public List<Book> GetAllBooks()
    {
      List<Book> books = new List<Book>();
      string connectionString = "Server=localhost;Port=3306;Database=libraryDB;User ID=root;Password=admin;";
      MySqlConnection con = new MySqlConnection(connectionString);
      con.Open();
      string query = "Select * from books";
      MySqlCommand command = new MySqlCommand(query, con);
      MySqlDataReader reader = command.ExecuteReader();
      while (reader.Read())
      {
        Book book = new Book();
        book.BookId = Convert.ToInt32(reader["BookId"]);
        book.Title = reader["Title"].ToString();
        book.Author = reader["Author"].ToString();
        book.Stock = Convert.ToInt32(reader["Stock"]);

        books.Add(book);

      }
      reader.Close();
      con.Close();
      return books;


    }
    public bool UpdateStock(int bookid, int stock)
    {
      string connectionString = "Server=localhost;Port=3306;Database=libraryDB;User ID=root;Password=admin;";
      MySqlConnection con = new MySqlConnection(connectionString);
      if (stock < 0)
      {
        return false;
      }
      con.Open();
      string query = "Update books set Stock=@stock where BookId=@bookid";
      MySqlCommand command = new MySqlCommand(query, con);
      command.Parameters.AddWithValue("@bookid", bookid);
      command.Parameters.AddWithValue("@stock", stock);
      int rowsAffected = command.ExecuteNonQuery();
      con.Close();
      return (rowsAffected > 0);

    }
    public bool DeleteBook(int bookid)
    {
      string connectionString = "Server=localhost;Port=3306;Database=libraryDB;User ID=root;Password=admin;";
      MySqlConnection con = new MySqlConnection(connectionString);
      con.Open();
      string query = "DELETE FROM books WHERE BookId = @bookid AND NOT EXISTS (SELECT 1 FROM issuedbooks WHERE BookId = @bookid)";
      MySqlCommand command = new MySqlCommand(query, con);
      command.Parameters.AddWithValue("@bookid", bookid);
      int rowsAffected = command.ExecuteNonQuery();
      con.Close();
      return (rowsAffected > 0);
    }
  }

}