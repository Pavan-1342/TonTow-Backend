using MessagePack;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace TonTow.API.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class TonTowUser
    {
        [Key]
        public int UserId { get; set; }
        public int TonTowRptId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        [Column(TypeName = "datetime")]
        public DateTime TokenCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime TokenExpires { get; set; }
        [StringLength(150)]
        public string EmailId { get; set; }
        [StringLength(15)]
        public string? Phone { get; set; }
        public bool? Status { get; set; }
    }
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}
