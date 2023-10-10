using System.ComponentModel.DataAnnotations;

namespace Learn2Code.Areas.Admins.Models
{
    public class SignUp
    {
        [Required(ErrorMessage = "UserName không để trống")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Địa chỉ email không để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu không để trống")]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
