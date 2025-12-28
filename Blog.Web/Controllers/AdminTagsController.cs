using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Blog.Web.Repositories.IRepository;
using Blog.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository = null;
        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tagList = await _tagRepository.GetAllAsync();
            return View(tagList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TagRequest tagVM)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag
                {
                    Name = tagVM.Name,
                    DisplayName = tagVM.DisplayName,
                };
                await _tagRepository.AddAsync(tag);
                TempData[SD.Success] = SD.TagAdded;
                return RedirectToAction(nameof(Index));
            }
            return View(tagVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tagFromDb = await _tagRepository.GetAsync(id);
            if (tagFromDb == null)
            {
                return NotFound();
            }
            var tagForView = new TagRequest
            {
                Id = id,
                Name = tagFromDb.Name,
                DisplayName = tagFromDb.DisplayName,
            };
            return View(tagForView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TagRequest tagVM)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag
                {
                    Id = tagVM.Id,
                    Name = tagVM.Name,
                    DisplayName = tagVM.DisplayName,
                };
                await _tagRepository.UpdateAsync(tag);
                TempData[SD.Success] = SD.TagUpdated;
                return RedirectToAction(nameof(Index));
            }
            return View(tagVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _tagRepository.DeleteAsync(id);
            TempData[SD.Success] = SD.TagDeleted;
            return RedirectToAction(nameof(Index));
        }

    }
}
