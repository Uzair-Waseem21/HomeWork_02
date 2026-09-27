using MySql.Data.MySqlClient;
using System;
using BookLibraryModels;
namespace BookLibraryRepository
{
  public class IssueRepository
  {
    public bool IssueBook(int bookId, int memberId)
    {
      string connectionString = "Server=localhost;Port=3306;Database=libraryDB;User ID=root;Password=admin;";
      MySqlConnection con = new MySqlConnection(connectionString);
      con.Open();
      MySqlTransaction? transaction = null;
      transaction = con.BeginTransaction();
      string query1 = "Select Stock from books where BookId = @bookid AND Stock>0";
      string query2 = "Insert into issuedbooks (BookId,MemberId,IssueDate) Values (@bookid,@memberid,@issuedate)";
      string query3 = "Update books set Stock = Stock-1 where BookId = @bookid AND Stock>0";
      MySqlCommand command1 = new MySqlCommand(query1, con);
      command1.Parameters.AddWithValue("@bookid", bookId);
      MySqlCommand command2 = new MySqlCommand(query2, con);
      command2.Parameters.AddWithValue("@bookid", bookId);
      command2.Parameters.AddWithValue("@memberid", memberId);
      command2.Parameters.AddWithValue("@issuedate", DateTime.Now);
      MySqlCommand command3 = new MySqlCommand(query3, con);
      command3.Parameters.AddWithValue("@bookid", bookId);
      try
      {
        command1.Transaction = transaction;
        command2.Transaction = transaction;
        command3.Transaction = transaction;
        object? result = command1.ExecuteScalar();
        if (result == null)
        {
          transaction.Rollback();
          con.Close();
          return false;
        }
        command2.ExecuteNonQuery();
        int affectedRows = command3.ExecuteNonQuery();
        if (affectedRows > 0)
        {
          transaction.Commit();
          con.Close();
          return true;
        }
        else
        {
          transaction.Rollback();
          con.Close();
          return false;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error: " + ex.Message);
        transaction.Rollback();
        con.Close();
        return false;
      }
    }

    public bool ReturnBook(int issueId)
    {
      string connectionString = "Server=localhost;Port=3306;Database=libraryDB;User ID=root;Password=admin;";
      MySqlConnection con = new MySqlConnection(connectionString);
      con.Open();
      MySqlTransaction? transaction = null;
      transaction = con.BeginTransaction();
      string query1 = "Select BookId from issuedbooks where IssueId = @issueid and ReturnDate IS NULL";
      MySqlCommand command1 = new MySqlCommand(query1, con);
      command1.Parameters.AddWithValue("@issueid", issueId);
      string query2 = "Update issuedbooks set ReturnDate = CURDATE() where IssueId = @issueid AND ReturnDate IS NULL";
      MySqlCommand command2 = new MySqlCommand(query2, con);
      command2.Parameters.AddWithValue("@issueid", issueId);
      string query3 = "Update books set Stock = Stock+1 where bookId = @bookid";
      MySqlCommand command3 = new MySqlCommand(query3, con);
      int bookId;
      try
      {
        command1.Transaction = transaction;
        command2.Transaction = transaction;
        command3.Transaction = transaction;
        object? result = command1.ExecuteScalar();
        if (result == null)
        {
          transaction.Rollback();
          con.Close();
          return false;
        }
        else
        {
          bookId = Convert.ToInt32(result);
          int affectedRows2 = command2.ExecuteNonQuery();
          command3.Parameters.AddWithValue("@bookid", bookId);
          int affectedRows3 = command3.ExecuteNonQuery();
          if (affectedRows2 > 0 && affectedRows3 > 0)
          {
            transaction.Commit();
            con.Close();
            return true;
          }
          else
          {
            transaction.Rollback();
            con.Close();
            return false;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error: " + ex.Message);
        transaction.Rollback();
        con.Close();
        return false;
      }
    }
  }
}