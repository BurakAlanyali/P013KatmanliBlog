using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using P013KatmanliBlog.Core.Entities;
using System.Net.Http.Json;

namespace P013KatmanliBlog.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class PostsController : Controller
    {
        public readonly HttpClient _httpClient;
        public readonly string _apiAdres = "https://localhost:7277/api/";
        public PostsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: PostsController
        public async Task<ActionResult> Index()
        {
            return View(await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres + "Posts"));
        }

        // GET: PostsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _httpClient.GetFromJsonAsync<Post>(_apiAdres + "Posts/"+id));
        }

        // GET: PostsController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres + "Categories"), "Id", "Name");
            ViewBag.UserId = new SelectList(await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres + "Users"), "Id", "Name");
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Post collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _httpClient.PostAsJsonAsync(_apiAdres + "Posts", collection);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu");
            }
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres + "Categories"), "Id", "Name");
            ViewBag.UserId = new SelectList(await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres + "Users"), "Id", "Name");
            return View();
        }

        // GET: PostsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres + "Categories"), "Id", "Name");
            ViewBag.UserId = new SelectList(await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres + "Users"), "Id", "Name");
            return View(await _httpClient.GetFromJsonAsync<Post>(_apiAdres + "Posts/" + id));
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Post collection)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var response = await _httpClient.PutAsJsonAsync(_apiAdres + "Posts/" + id, collection);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu");
            }
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres + "Categories"), "Id", "Name");
            ViewBag.UserId = new SelectList(await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres + "Users"), "Id", "Name");
            return View(collection);
        }

        // GET: PostsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _httpClient.GetFromJsonAsync<Post>(_apiAdres + "Posts/" + id));
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Post collection)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiAdres + "Posts/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
