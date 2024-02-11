using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ju.entity
{
    public class Book
    {
        [Key]
        public int Bookid { get; set; }
        public string ?Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }

        public string? isbn { get; set; }
        public string? publishedDate { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
