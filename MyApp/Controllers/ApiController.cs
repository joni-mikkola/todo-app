using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyDB;
using SQLitePCL;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly MyDBContext _db;

        public ApiController(ILogger<ApiController> logger, MyDBContext db)
        {
            _db = db;
            _logger = logger;
        }

        [HttpDelete("todo/{id}")]
        public IActionResult DeleteTodo(int id)
        {
            var match = _db.Todos.SingleOrDefault(x => x.Id == id);
            if (match != null)
            {
                _db.Todos.Remove(match);
                _db.SaveChanges();
            }
            return Ok();
        }

        [HttpPatch("todo/{id}")]
        public IActionResult PutTodo(int id)
        {
            var match = _db.Todos.SingleOrDefault(x => x.Id == id);
            if (match != null)
            {
                if (match.Done == null)
                {
                    match.Done = DateTime.UtcNow;
                }
                else
                {
                    match.Done = null;
                }
                _db.SaveChanges();
            }

            return Ok(match);
        }

        [HttpGet("todo")]
        public IActionResult GetTodo()
        {
            return Ok(_db.Todos.OrderByDescending(x => x.Id));
        }

        [HttpPost("todo")]
        public IActionResult PostTodo([FromBody] TodoModel model)
        {
            model.Created = DateTime.UtcNow;
            _db.Add(model);
            _db.SaveChanges();
            return Ok(model);
        }
    }
}