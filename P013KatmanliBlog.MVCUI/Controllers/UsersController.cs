using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Models;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.MVCUI.Controllers
{
	public class UsersController : Controller
	{
		private readonly IPostService _servicePost;

		public UsersController(IPostService servicePost)
		{
			_servicePost = servicePost;
		}

		public async Task<IActionResult> Index(int id)
		{
			Post deneme = await _servicePost.GetNewestByUser(id);
			var model = new UsersViewModel()
			{
				FeaturedPost = deneme,
				Posts = await _servicePost.GetSomeByIncludeCategoryAndUserAsync(x => x.UserId == id && x.Id != deneme.Id)
			};
			return View(model);
		}
	}
}
