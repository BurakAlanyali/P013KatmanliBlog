using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Models;
using P013KatmanliBlog.Service.Abstract;
using System.Security.Claims;

namespace P013KatmanliBlog.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IService<User> _service;

        public LoginController(IService<User> service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AdminLoginModel admin)
        {
            
                
                    var kullanici = await _service.GetAsync(k => k.Email == admin.Email && k.Password == admin.Password);
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
