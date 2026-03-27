using System.ComponentModel.DataAnnotations;

namespace DevMobile.ApiService.Entities;

public class Review
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int Chapter { get; set; }
    public bool Spoiler { get; set; }
    [Range(1, 5)]
    public int Rating { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}

