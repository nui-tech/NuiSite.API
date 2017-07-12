using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NuiSite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        // GET: api/Blog
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Blog/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            return NotFound();
        }
        
        // POST: api/Blog
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Blog/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
