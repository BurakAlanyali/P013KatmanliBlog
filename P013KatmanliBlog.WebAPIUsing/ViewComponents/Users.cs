using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;


namespace P013KatmanliBlog.WebAPIUsing.ViewComponents
{
	public class Users : ViewComponent
	{
		private readonly HttpClient _httpClient;

		public Users(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		private readonly string _apiAdres = "https://localhost:7277/api/Users";


		public async Task<IViewComponentResult> InvokeAsync()
		{
			var model = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres);
			var userList = model.Where(x=>x.IsAdmin).ToList();
			return View(userList);
		}
	}
}
