using System.ComponentModel.DataAnnotations;

namespace YaseinPortfolio.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Enter Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Not Match")]
        public string ConfirmPassword { get; set; }
        public string Mobile { get; set; }
    }
}
