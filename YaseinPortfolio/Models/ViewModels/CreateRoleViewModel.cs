using System.ComponentModel.DataAnnotations;

namespace YaseinPortfolio.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Enter Role Name")]
        public string RoleName { get; set; }
    }
}
