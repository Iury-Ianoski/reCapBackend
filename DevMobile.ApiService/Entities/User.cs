using Microsoft.AspNetCore.Identity;

namespace DevMobile.ApiService.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
    public virtual ICollection<Role> Roles { get; set; }
}
