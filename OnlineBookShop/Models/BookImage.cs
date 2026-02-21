namespace OnlineBookShop.Models
{
    public class BookImage
    {
        public int Id { get; set; }
        public string FileName { get; set; } = "";
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public string Url => $"/books/{BookId}/{FileName}";
    }
}
