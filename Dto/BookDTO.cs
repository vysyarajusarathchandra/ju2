using System.ComponentModel.DataAnnotations.Schema;

namespace ju.Dto
{
    public class BookDTO
    {
        public int Bookid { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }

        public string? isbn { get; set; }
        public string? publishedDate { get; set; }
       
        public int UserId { get; set; }
    }
}
