using System.ComponentModel.DataAnnotations;

namespace YaseinPortfolio.Models.ViewModels
{
    public class RoleViewModel
    {
        public string RoleViewModelId { get; set; }

        [Required(ErrorMessage = "Enter Role Name")]
        public string RoleName { get; set; }
    }
}
