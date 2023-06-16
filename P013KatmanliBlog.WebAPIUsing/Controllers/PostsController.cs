using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;

namespace P013KatmanliBlog.WebAPIUsing.Controllers
{
	public class PostsController : Controller
	{
		private readonly HttpClient _httpClient;

		public PostsController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		private readonly string _apiAdres = "https://localhost:7277/api/";
		public async Task<IActionResult> Index(int id)
		{
			Post model = await _httpClient.GetFromJsonAsync<Post>(_apiAdres+"Posts/"+id);
			return View(model);
		}
		public async Task<IActionResult> Search(string? q)
		{
			if (q == null)
			{
				return RedirectToAction("Index","Home");
			}
			var model = await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres+"Search/"+q);
			if (model == null)
			{
				return NotFound();
			}
			return View(model);
		}
	}
}
