﻿using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P013KatmanliBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IService<Category> _service;

        public CategoriesController(IService<Category> service)
        {
            _service = service;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _service.GetAll();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _service.Find(id);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public void Post([FromBody] Category value)
        {
            _service.Add(value);
            _service.Save();
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Category value)
        {
            _service.Update(value);
            int sonuc = await _service.SaveAsync();
            if (sonuc > 0)
            {
                return Ok(value);
            }
            return StatusCode(StatusCodes.Status304NotModified);
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var kayit = await _service.FindAsync(id);
            if (kayit == null)
            {
                return NoContent();
            }
            else
            {
                _service.Delete(kayit);
                await _service.SaveAsync();
                return Ok(kayit);
            }
        }
    }
}
