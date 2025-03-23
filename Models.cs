namespace BiblioApp.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Available { get; set; }
    }

    public class Visitor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDebtor { get; set; }
    }
}