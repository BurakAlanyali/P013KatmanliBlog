using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.WebAPIUsing.Models;
using System.Security.Claims;

namespace P013KatmanliBlog.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        public readonly HttpClient _httpClient;
        public readonly string _apiAdres = "https://localhost:7277/api/";
        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AdminLoginModel admin)
        {

            var kullaniciListe = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres + "Users");
            var kullanici = kullaniciListe.FirstOrDefault(x=>x.Email==admin.Email && x.Password==admin.Password);
            if (kullanici != null)
            {
                var kullaniciYetkileri = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email,kullanici.Email),
                        new Claim("Role",kullanici.IsAdmin ? "Admin" : "User")
                    };
                var kullanicikimligi = new ClaimsIdentity(kullaniciYetkileri, "Login");
                ClaimsPrincipal claimsPrincipal = new(kullanicikimligi);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect("/Admin/Main");
            }
            try
            {
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Hata Oluştu!");

            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin/Login");
        }
    }
}
