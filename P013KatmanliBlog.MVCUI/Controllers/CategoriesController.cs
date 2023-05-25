using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Models;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.MVCUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IService<Category> _serviceCategory;
        private readonly IPostService _servicePost;

        public CategoriesController(IService<Category> serviceCategory, IPostService servicePost)
        {
            _serviceCategory = serviceCategory;
            _servicePost = servicePost;
        }

        public async Task<IActionResult> Index(int id)
        {
            Post deneme = await _servicePost.GetNewestByCategory(id);
            var model = new CategoriesViewModel()
            {
                FeaturedPost = deneme,
                Posts = await _servicePost.GetSomeByIncludeCategoryAndUserAsync(x => x.CategoryId == id && x.Id != deneme.Id)
            };
            return View(model);
        }
    }
}
