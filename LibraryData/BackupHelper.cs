using System;
using System.IO;
using BookLibraryRepository;
using System.Text.Json;
using BookLibraryModels;

namespace BackupHelper
{
  public class Backup
  {
        public void ExportBooks(List<Book> books, string filePath)
    {
      string jsonString = JsonSerializer.Serialize(books);
      FileStream fs = new FileStream(filePath, FileMode.Create);
      StreamWriter sw = new StreamWriter(fs);
      sw.WriteLine(jsonString);
      sw.Close();
    }

    public List<Book> ImportBooks(string filePath)
    {
      if (!File.Exists(filePath))
      {
        Console.WriteLine("File does not exist!");
        return new List<Book>();
      }
      FileStream fr = new FileStream(filePath, FileMode.Open);
      StreamReader sr = new StreamReader(fr);
      string data = sr.ReadToEnd();
      List<Book>? books = JsonSerializer.Deserialize<List<Book>>(data);
      sr.Close();
      if (books == null)
      {
        Console.WriteLine("Empty list");
        return new List<Book>();
      }
      return books;
    }
  }
}