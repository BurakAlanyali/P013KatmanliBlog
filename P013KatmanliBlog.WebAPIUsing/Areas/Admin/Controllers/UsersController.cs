using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;

namespace P013KatmanliBlog.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class UsersController : Controller
    {
        public readonly HttpClient _httpClient;
        public readonly string _apiAdres = "https://localhost:7277/api/";
        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: UsersController
        public async Task<ActionResult> Index()
        {
            return View(await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres + "Users"));
        }

        // GET: UsersController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres + "UserPosts/" + id));
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(User collection)
        {
            try
            {
                collection.IsAdmin = true;
                if (ModelState.IsValid)
                {
                    var response = await _httpClient.PostAsJsonAsync(_apiAdres + "Users", collection);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }

            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View();
        }

        // GET: UsersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _httpClient.GetFromJsonAsync<User>(_apiAdres + "Users/" + id));
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, User collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _httpClient.PutAsJsonAsync(_apiAdres + "Users/" + id, collection);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(collection);
        }

        // GET: UsersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _httpClient.GetFromJsonAsync<User>(_apiAdres + "Users/" + id));
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, User collection)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiAdres + "Users/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
