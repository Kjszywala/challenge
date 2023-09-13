using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Challenge.Api.Models;
using Challenge.Api.Models.Database;

namespace Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCodesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PostCodesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/PostCodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostCodes>>> GetPostCodes()
        {
            if (_context.PostCodes == null)
            {
                return NotFound();
            }
            return await _context.PostCodes.ToListAsync();
        }

        // GET: api/PostCodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostCodes>> GetPostCodes(int id)
        {
            if (_context.PostCodes == null)
            {
                return NotFound();
            }
            var postCodes = await _context.PostCodes.FindAsync(id);

            if (postCodes == null)
            {
                return NotFound();
            }

            return postCodes;
        }

        // POST: api/PostCodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostCodes>> PostPostCodes(PostCodes postCodes)
        {
            if (_context.PostCodes == null)
            {
                return Problem("Entity set 'DatabaseContext.PostCodes'  is null.");
            }
            _context.PostCodes.Add(postCodes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostCodes", new { id = postCodes.Id }, postCodes);
        }

        private bool PostCodesExists(int id)
        {
            return (_context.PostCodes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
