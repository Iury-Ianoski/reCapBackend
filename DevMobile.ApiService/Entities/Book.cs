namespace DevMobile.ApiService.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }
    public string CoverImageUrl { get; set; }
    public int Chapters { get; set; }
    public string Summary { get; set; }
    public virtual ICollection<Genre> Genres { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }

}
