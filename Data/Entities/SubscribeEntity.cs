

using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class SubscribeEntity
{
    [Key]
    public string Email { get; set; } = null!;
    public string? PreferredEmail { get; set; }
    public bool Circle1 { get; set; } = false;
    public bool Circle2 { get; set; } = false;
    public bool Circle3 { get; set; } = false;
    public bool Circle4 { get; set; } = false;
    public bool Circle5 { get; set; } = false;
    public bool Circle6 { get; set; } = false;
}
