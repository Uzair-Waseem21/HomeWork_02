using MySql.Data.MySqlClient;
using System; 
using BookLibraryModels;
namespace BookLibraryRepository;

public class MemberRepository
{
  public bool AddMember(Member member)
  {
     string connectionString = "Server=localhost;Port=3306;Database=libraryDB;User ID=root;Password=admin;";
      MySqlConnection con = new MySqlConnection(connectionString);
      if (string.IsNullOrWhiteSpace(member.Name))
         return false;
      con.Open();
       string query = "INSERT INTO members (Name) VALUES (@name)";
      MySqlCommand command = new MySqlCommand(query,con);
      command.Parameters.AddWithValue("@name", member.Name);
      int rowsAffected = command.ExecuteNonQuery();
      con.Close();
      return rowsAffected > 0;
  }
   public List<Member> GetAllMembers()
    {
      List<Member> members = new List<Member>();
       string connectionString = "Server=localhost;Port=3306;Database=libraryDB;User ID=root;Password=admin;";
      MySqlConnection con = new MySqlConnection(connectionString);
      con.Open();
      string query = "Select * from members";
      MySqlCommand command = new MySqlCommand(query,con);
      MySqlDataReader reader = command.ExecuteReader();
      while (reader.Read())
      {
        Member member = new Member();
        member.MemberId = Convert.ToInt32(reader["MemberId"]);
        member.Name = reader["Name"].ToString();
        members.Add(member);
      }
      reader.Close();
      con.Close();
      return members;


    }
}