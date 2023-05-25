using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.MVCUI.ViewComponents
{
	public class Users : ViewComponent
	{
		private readonly IService<User> _service;

		public Users(IService<User> service)
		{
			_service = service;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View(await _service.GetAllAsync());
		}
	}
}
