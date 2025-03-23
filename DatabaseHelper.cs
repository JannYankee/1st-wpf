using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace biblioDATA
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["BiblioDb"].ConnectionString;

        public static DataTable GetAuthors()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM authors";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetBooks(bool onlyAvailable = false)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM books";
                if (onlyAvailable) query += " WHERE available = 1";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetVisitors()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM visitors";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static void AddBook(string title)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO books (title, available) VALUES (@title, 1)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.ExecuteNonQuery();
            }
        }

        public static void AddAuthor(string name)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO authors (name) VALUES (@name)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
            }
        }

        public static void AddVisitor(string name, bool isDebtor)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO visitors (name, isDebtor) VALUES (@name, @isDebtor)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@isDebtor", isDebtor);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateBookTitle(int bookId, string newTitle)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE books SET title = @newTitle WHERE id = @bookId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@newTitle", newTitle);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateAuthor(int authorId, string newName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE authors SET name = @newName WHERE id = @authorId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@newName", newName);
                cmd.Parameters.AddWithValue("@authorId", authorId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateVisitor(int visitorId, string newName, bool isDebtor)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE visitors SET name = @newName, isDebtor = @isDebtor WHERE id = @visitorId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@newName", newName);
                cmd.Parameters.AddWithValue("@isDebtor", isDebtor);
                cmd.Parameters.AddWithValue("@visitorId", visitorId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteBook(int bookId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM books WHERE id = @bookId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.ExecuteNonQuery();
            }
        }

        public static bool DeleteAuthor(int authorId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM book_authors WHERE author_id = @authorId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@authorId", authorId);
                int bookCount = (int)checkCmd.ExecuteScalar();

                if (bookCount > 0) return false; 

                // Видаляємо автора
                string query = "DELETE FROM authors WHERE id = @authorId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@authorId", authorId);
                cmd.ExecuteNonQuery();
                return true;
            }
        }


        public static void DeleteVisitor(int visitorId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM visitors WHERE id = @visitorId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@visitorId", visitorId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
