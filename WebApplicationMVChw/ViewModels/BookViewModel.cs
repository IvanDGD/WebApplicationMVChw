using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVChw.ViewModels
{
    public class BookViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
    }
}
