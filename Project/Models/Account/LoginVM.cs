using System.ComponentModel.DataAnnotations;

namespace Project.Models.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(255)]
        public string Username { get; set; } = "";
        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(255)]
        public string Password { get; set; } = "";

        public bool isBan { get; set; } = false;

        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }

    }
}
