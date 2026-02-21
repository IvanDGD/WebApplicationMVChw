namespace OnlineBookShop.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string Text { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
