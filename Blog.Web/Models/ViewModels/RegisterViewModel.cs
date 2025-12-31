using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
