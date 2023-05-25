using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.MVCUI.Controllers
{
	public class PostsController : Controller
	{
		private readonly IPostService _servicePost;

		public PostsController(IPostService servicePost)
		{
			_servicePost = servicePost;
		}

		public async Task<IActionResult> Index(int id)
		{
			Post model = await _servicePost.GetByIdByIncludeCategoryAndUserAsync(id);
			return View(model);
		}
		public async Task<IActionResult> Search(string q)
		{
			var model = await _servicePost.GetSomeByIncludeCategoryAndUserAsync(p => p.Title.Contains(q) || p.Body.Contains(q) || p.Category.Name.Contains(q) || p.User.Name.Contains(q));
			return View(model);
		}
	}
}
