using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name ="User Name")]
        public string UserName { get; set; }        
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
