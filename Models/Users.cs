using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TonTow.API.Models
{
    public class LoginResponse
    {
        
        public int Id { get; set; }
        public  int TonTowRptId { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
     //   public string Role { get; set; }
    }

    public class UserRequest
    {
        public int Id { get; set; }
        public int TonTowRptId { get; set; }

    }

    public class UserCreationRequest
    {
        public int TonTowRptId { get; set; }
        public string JobNum { get; set; }

    }
    public class ChangePasswordRequest
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
