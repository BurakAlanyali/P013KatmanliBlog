using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.WebAPIUsing.Models;
using System.Net.Http.Json;

namespace P013KatmanliBlog.WebAPIUsing.Controllers
{
	public class AccountController : Controller
	{
		private readonly HttpClient _httpClient;

		public AccountController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		private readonly string _apiAdres = "https://localhost:7277/api/";
		public async Task<IActionResult> Index()
		{
			var kullaniciID = HttpContext.Session.GetInt32("userId");
			if (kullaniciID is null)
			{
				TempData["Message"] = "<div class='alert alert-danger'>Lütfen Giriş Yapınız!!!</div>";
				return RedirectToAction(nameof(Login));
			}
			else
			{
				var kullanici = await _httpClient.GetFromJsonAsync<User>(_apiAdres+"Users/"+kullaniciID);
				return View(kullanici);
			}
		}
		[HttpPost]
		public async Task<IActionResult> UpdateUser(User appUser)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var kullaniciID = HttpContext.Session.GetInt32("userId");
					var kullanici = await _httpClient.GetFromJsonAsync<User>(_apiAdres + "Users/" + kullaniciID);
					if (kullanici is not null)
					{
						kullanici.Name = appUser.Name;
						kullanici.Email = appUser.Email;
						kullanici.Password = appUser.Password;
						kullanici.Surname = appUser.Surname;
						kullanici.Phone = appUser.Phone;
						await _httpClient.PutAsJsonAsync(_apiAdres + "Users/" + kullaniciID, kullanici);
						return RedirectToAction(nameof(Index));
					}
				}
				else
				{
					throw new Exception();
				}

			}
			catch (Exception)
			{
				ModelState.AddModelError("", "Hata Oluştu!");

			}
			return View("Index", appUser);
		}
		public IActionResult SignIn()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(User user)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var userList = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres+"Users");
					var kullanici= userList.Where(x=>x.Email==user.Email).FirstOrDefault();
					if (kullanici is not null)
					{
						ModelState.AddModelError("", "Bu Email kullanılmaktadır... Başka E-Mail deneyiniz!");
						return View();
					}
					user.IsAdmin = false;
					await _httpClient.PostAsJsonAsync(_apiAdres + "Users",user);

					TempData["Message"] = "<div class='alert alert-success'>Yeni Kayıt Başarıyla Oluşturuldu! Teşekkürler..</div>";
				}
				catch (Exception)
				{
					ModelState.AddModelError("", "Hata Oluştu!");
					return RedirectToAction("SignIn");
				}
			}
			return View();
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(AdminLoginModel user)
		{
			var userList = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres + "Users");
			User kullanici = userList.Where(x => x.Email == user.Email && x.Password==user.Password).FirstOrDefault();
			
			if (kullanici is null)
			{
				ModelState.AddModelError("", "Giriş Başarısız!");
			}
			else
			{
				HttpContext.Session.SetInt32("userId", kullanici.Id);
				return RedirectToAction(nameof(Index));
			}
			return View();
		}
		public IActionResult Logout()
		{
			try
			{
				HttpContext.Session.Remove("userId");
			}
			catch (Exception)
			{

				HttpContext.Session.Clear();
			}

			return RedirectToAction("Index", "Home");
		}
	}
}
