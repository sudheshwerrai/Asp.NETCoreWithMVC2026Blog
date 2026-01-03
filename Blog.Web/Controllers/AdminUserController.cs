using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace Blog.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminUserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AdminUserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            var userList = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userVM = new UserViewModel
                {
                    Id = Guid.Parse(user.Id),
                    UserName = user.UserName,
                    Email = user.Email,
                };
                userList.Add(userVM);
            }

            return View(userList);
        }        

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userRepository.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
