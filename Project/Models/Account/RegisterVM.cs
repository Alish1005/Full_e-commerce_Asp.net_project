using System.ComponentModel.DataAnnotations;

namespace Project.Models.Account
{
    public class RegisterVM
    {
        
        [StringLength(12)]
        [Required(ErrorMessage = "Please enter a username.")]
        public string Username { get; set; } = "Ali";
        [Required(ErrorMessage = "Please enter your phone number.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter your email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your home address for delivery.")]
        public string Address { get; set; }

        
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; } = "123456^Aa";
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please confirm your password.")]
        public string ConfirmPassword { get; set; } = "123456^Aa";
    }
}
