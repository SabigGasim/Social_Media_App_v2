using System.ComponentModel.DataAnnotations;

namespace Server.Entities;

public class Media
{
    public Guid Id { get; set; }
    [Required]
    public string FilePath { get; set; }
    [Required]
    public string Url { get; set; }
    [Required]
    public string ContentType { get; set; }
    [Required]
    public long ContentLength { get; set; }
}
