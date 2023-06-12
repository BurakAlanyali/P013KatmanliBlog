using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;

namespace P013KatmanliBlog.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class CategoriesController : Controller
    {
        public readonly HttpClient _httpClient;
        public readonly string _apiAdres = "https://localhost:7277/api/";
        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: CategoriesController
        public async Task<ActionResult> Index()
        {
            return View(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres+"Categories"));
        }

        // GET: CategoriesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres + "CategoryPosts/"+id));
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _httpClient.PostAsJsonAsync(_apiAdres + "Categories", collection);
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

        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _httpClient.GetFromJsonAsync<Category>(_apiAdres+"Categories/"+id));
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response= await _httpClient.PutAsJsonAsync(_apiAdres+"Categories/"+id, collection);
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

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _httpClient.GetFromJsonAsync<Category>(_apiAdres + "Categories/" + id));
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Category collection)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiAdres + "Categories/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
