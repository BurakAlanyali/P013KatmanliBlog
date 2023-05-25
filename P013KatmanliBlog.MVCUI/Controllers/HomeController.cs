using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Areas.Admin.Controllers;
using P013KatmanliBlog.MVCUI.Models;
using P013KatmanliBlog.Service.Abstract;
using System.Diagnostics;

namespace P013KatmanliBlog.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _servicePost;
        private readonly IService<Category> _serviceCategory;
        private readonly IService<User> _serviceUser;

		public HomeController(IPostService servicePost, IService<Category> serviceCategory, IService<User> serviceUser)
		{
			_servicePost = servicePost;
			_serviceCategory = serviceCategory;
			_serviceUser = serviceUser;
		}

		public async Task<IActionResult> Index()
        {
            Post deneme = await _servicePost.GetNewest();
            var model = new HomePageViewModel()
            {
                FeaturedPost = deneme,
                Posts = await _servicePost.GetSomeByIncludeCategoryAndUserAsync(x=>x.Id !=deneme.Id)
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}