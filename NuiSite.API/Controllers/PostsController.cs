using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuiSite.API.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using NuiSite.API.Models;

namespace NuiSite.API.Controllers
{

    [Produces("application/json")]
    [Route("api/Posts")]
    public class PostsController : Controller
    {
        private readonly NuiSiteContext _context;
        private IConfiguration _config;

        public PostsController(NuiSiteContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        [HttpGet("test")]
        public IActionResult Test()
        {
            var zz = HttpContext.User.Claims.Select(x => new { x.Type, x.Value }).ToList();
            var xx = _config.GetConnectionString("NuiSiteDb");
            var yy = _config.GetConnectionString("DefaultConnection");
            return Ok(new { NuiSiteDb = xx, DefaultConnection = yy, user = zz });
        }

        // GET: api/Posts
        [HttpGet]
        public IActionResult GetPost()
        {
            var xx = DateTime.Now;
            var post = _context.Post;
            if (!post.Any())
                return NotFound();

            return Ok(post.ToList());
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Post.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPost([FromRoute] int id, [FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                post.UpdateOn = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostPost([FromBody] Post post)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var date = DateTime.Now;
            post.CreatedOn = date;
            post.UpdateOn = date;
            post.IsActive = true;

            _context.Post.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Post.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}