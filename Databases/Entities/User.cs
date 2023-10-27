using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Databases.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Username { get; set; } = null!;
        [StringLength(255)]
        public string Email { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
    }
}