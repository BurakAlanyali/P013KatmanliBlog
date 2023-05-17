using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.MVCUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoriesController : Controller
	{
		private readonly IService<Category> _service;
		private readonly IPostService _servicePost;

		public CategoriesController(IService<Category> service, IPostService servicePost)
		{
			_service = service;
			_servicePost = servicePost;
		}

		// GET: CategoriesController
		public async Task<ActionResult> Index()
		{
			return View(await _service.GetAllAsync());
		}

		// GET: CategoriesController/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			var model = await _servicePost.GetSomeByIncludeCategoryAndUserAsync(x => x.CategoryId == id);
			ViewBag.Kategori = _service.Find(id.Value).Name;
			if (model == null)
			{
				return NotFound();
			}
			return View(model);
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
				await _service.AddAsync(collection);
				await _service.SaveAsync();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: CategoriesController/Edit/5
		public async Task<ActionResult> Edit(int id)
		{
			return View(await _service.FindAsync(id));
		}

		// POST: CategoriesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int id, Category collection)
		{
			try
			{
				_service.Update(collection);
				await _service.SaveAsync();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: CategoriesController/Delete/5
		public async Task<ActionResult> Delete(int id)
		{
			return View(await _service.FindAsync(id));
		}

		// POST: CategoriesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id, Category collection)
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
