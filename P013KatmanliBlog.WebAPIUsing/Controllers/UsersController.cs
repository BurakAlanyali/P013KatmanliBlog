using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.WebAPIUsing.Models;

namespace P013KatmanliBlog.WebAPIUsing.Controllers
{
	public class UsersController : Controller
	{
		private readonly HttpClient _httpClient;

		public UsersController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		private readonly string _apiAdres = "https://localhost:7277/api/";
		public async Task<IActionResult> Index(int id)
		{
			var deneme = await _httpClient.GetAsync(_apiAdres + "GetNewestbyUser/" + id);
			if (deneme.IsSuccessStatusCode)
			{
				var post = await _httpClient.GetFromJsonAsync<Post>(_apiAdres + "GetNewestbyUser/" + id);
				var postList = await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres + "UserPosts/" + id);
				var posts = postList.Where(x => x.Id != post.Id).ToList();
				var model = new UsersViewModel()
				{
					FeaturedPost = post,
					Posts = posts
				};
				return View(model);
			}
			return NotFound();
		}
	}
}
