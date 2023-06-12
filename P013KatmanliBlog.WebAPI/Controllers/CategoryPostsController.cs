using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P013KatmanliBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryPostsController : ControllerBase
    {
        private readonly IPostService _service;

        public CategoryPostsController(IPostService service)
        {
            _service = service;
        }

        // GET: api/<CategoryPostsController>
        [HttpGet("{id}")]
        public async Task<IEnumerable<Post>> GetAsync(int id)
        {
            return await _service.GetSomeByIncludeCategoryAndUserAsync(x=>x.CategoryId==id);
        }

        
    }
}
