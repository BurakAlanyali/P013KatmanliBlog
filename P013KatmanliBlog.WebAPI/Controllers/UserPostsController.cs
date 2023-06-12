using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPostsController : ControllerBase
    {
        private readonly IPostService _service;

        public UserPostsController(IPostService service)
        {
            _service = service;
        }

        // GET: api/<CategoryPostsController>
        [HttpGet("{id}")]
        public async Task<IEnumerable<Post>> GetAsync(int id)
        {
            return await _service.GetSomeByIncludeCategoryAndUserAsync(x => x.UserId == id);
        }
    }
}
