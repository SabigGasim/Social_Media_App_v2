using System.ComponentModel.DataAnnotations;

namespace Server.Entities;

public class Profile
{
    public Guid Id { get; set; }
    public Guid MediaId { get; set; }
    [Required]
    public Guid UserId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPublic { get; set; }

    public virtual User User { get; set; }
    public virtual Media Media { get; set; }
}