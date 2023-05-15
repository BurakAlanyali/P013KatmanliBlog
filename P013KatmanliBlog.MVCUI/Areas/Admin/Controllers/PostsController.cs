using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly IService<Category> _categoryService;
        private readonly IService<User> _userService;
        private readonly IPostService _service;

        public PostsController(IService<Category> categoryService, IService<User> userService, IPostService service)
        {
            _categoryService = categoryService;
            _userService = userService;
            _service = service;
        }

        // GET: PostsController
        public async Task<ActionResult> Index()
        {
            return View(await _service.GetAllByIncludeCategoryAndUserAsync());
        }

        // GET: PostsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _service.GetByIdByIncludeCategoryAndUserAsync(id));
        }

        // GET: PostsController/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id","Name");
            ViewBag.UserId = new SelectList(_userService.GetAll(), "Id","Name");
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Post collection)
        {
            try
            {
                await _service.AddAsync(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id", "Name");
                ViewBag.UserId = new SelectList(_userService.GetAll(), "Id", "Name");
                return View();
            }
        }

        // GET: PostsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id", "Name");
            ViewBag.UserId = new SelectList(_userService.GetAll(), "Id", "Name");
            return View(await _service.GetByIdByIncludeCategoryAndUserAsync(id));
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Post collection)
        {
            try
            {
                _service.Update(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id", "Name");
                ViewBag.UserId = new SelectList(_userService.GetAll(), "Id", "Name");
                return View();
            }
        }

        // GET: PostsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _service.GetByIdByIncludeCategoryAndUserAsync(id));
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Post collection)
        {
            try
            {
                _service.Delete(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
