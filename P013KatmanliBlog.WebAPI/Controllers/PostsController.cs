using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P013KatmanliBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _service;

        public PostsController(IPostService service)
        {
            _service = service;
        }

        // GET: api/<PostsController>
        [HttpGet]
        public async Task<IEnumerable<Post>> Get()
        {
            return await _service.GetAllByIncludeCategoryAndUserAsync();
        }

        [HttpGet("/api/GetNewest")]
		public async Task<Post> GetNewest()
		{
			return await _service.GetNewest();
		}
		[HttpGet("/api/GetNewestbyCategory/"+"{id}")]
		public async Task<IActionResult> GetNewestbyCategory(int id)
		{
            var model = await _service.GetNewestByCategory(id);
            if (model == null)
            {
                return NotFound();
            }
			return Ok(model);
		}
		[HttpGet("/api/GetNewestbyUser/" + "{id}")]
		public async Task<IActionResult> GetNewestbyUser(int id)
		{
			var model= await _service.GetNewestByUser(id);
			if (model == null)
			{
				return NotFound();
			}
			return Ok(model);
		}
		// GET api/<PostsController>/5
		[HttpGet("{id}")]
        public async Task<Post> Get(int id)
        {
            return await _service.GetByIdByIncludeCategoryAndUserAsync(id);
        }
		[HttpGet("/api/Search/"+"{q}")]
		public async Task<List<Post>> Search(string q)
		{
            var model = await _service.GetAllByIncludeCategoryAndUserAsync();
            var postList = model.Where(p => p.Title.Contains(q) || p.Body.Contains(q) || p.Category.Name.Contains(q) || p.User.Name.Contains(q)).ToList();
			return postList;
		}
		// POST api/<PostsController>
		[HttpPost]
        public async Task Post([FromBody] Post value)
        {
            await _service.AddAsync(value);
            await _service.SaveAsync();
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Post value)
        {
            _service.Update(value);
            int sonuc = await _service.SaveAsync();
            if (sonuc > 0)
            {
                return Ok(value);
            }
            return StatusCode(StatusCodes.Status304NotModified);
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var kayit = await _service.GetByIdByIncludeCategoryAndUserAsync(id);
            if (kayit is null)
            {
                return NoContent();
            }
            else
            {
                _service.Delete(kayit);
                await _service.SaveAsync();
                return Ok();
            }
        }
    }
}
