using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.WebAPIUsing.Models;
using System.Diagnostics;
using System.Linq;

namespace P013KatmanliBlog.WebAPIUsing.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7277/api/";

        public async Task<IActionResult> Index()
        {
            Post deneme = await _httpClient.GetFromJsonAsync<Post>(_apiAdres+"GetNewest");
            var postList = await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres+"Posts");
			var posts = postList.Where(x=>x.Id != deneme.Id).ToList();
			var model = new HomePageViewModel()
			{
				FeaturedPost = deneme,
				Posts = posts
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