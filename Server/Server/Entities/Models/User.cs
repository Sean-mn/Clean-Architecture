using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Entities.Models;

[Table("users")]
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    
    [Column("passwordhash")]
    public string Password { get; set; } = null!;
}