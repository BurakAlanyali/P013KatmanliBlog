using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Models;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.MVCUI.Controllers
{
	public class AccountController : Controller
	{
		private readonly IService<User> _service;

		public AccountController(IService<User> service)
		{
			_service = service;
		}

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
                var kullanici = await _service.GetAsync(u => u.Id == kullaniciID);
                return View(kullanici);
            }
        }
		[HttpPost]
        public async Task<IActionResult> UpdateUser(User appUser)
        {
            try
            {
                var kullaniciID = HttpContext.Session.GetInt32("userId");
                var kullanici = await _service.GetAsync(u => u.Id == kullaniciID);
                if (kullanici is not null)
                {
                    kullanici.Name = appUser.Name;
                    kullanici.Email = appUser.Email;
                    kullanici.Password = appUser.Password;
                    kullanici.Surname = appUser.Surname;
                    kullanici.Phone = appUser.Phone;
                    _service.Update(kullanici);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
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
					if (_service.Get(x => x.Email == user.Email) is not null)
					{
						ModelState.AddModelError("", "Bu Email kullanılmaktadır... Başka E-Mail deneyiniz!");
						return View();
					}
					user.IsAdmin = false;
					await _service.AddAsync(user);
					await _service.SaveAsync();
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
			User kullanici = await _service.GetAsync(x => x.Email == user.Email && x.Password == user.Password);
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
