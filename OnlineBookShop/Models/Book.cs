namespace OnlineBookShop.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string Genre { get; set; } = "";
        public int Price { get; set; }

        public List<BookImage> Images { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}