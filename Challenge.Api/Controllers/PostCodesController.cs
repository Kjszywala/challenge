using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Challenge.Api.Models;
using Challenge.Api.Models.Database;
using Challenge.BusinessLogic.Interfaces;

namespace Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCodesController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private IPostCodeLogic PostCodeLogic;

        public PostCodesController(DatabaseContext context, IPostCodeLogic _PostCodeLogic)
        {
            _context = context;
            PostCodeLogic = _PostCodeLogic;
        }

        // GET: api/PostCodes/dt4
        [HttpGet("{partialString}")]
        public async Task<ActionResult<List<string?>>> SearchPostCodes(string partialString)
        {
            try
            {
                var postCodes = await _context.PostCodes
                    .Where(postcode => postcode.Postcode.Contains(partialString))
                    .Select(postcode => postcode.Postcode)
                    .ToListAsync();

                if (postCodes == null)
                {
                    return NotFound();
                }

                return postCodes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("near")]
        public async Task<ActionResult<List<string>>> GetPostcodesNearLocation(double latitude, double longitude, double maxDistanceInKilometers)
        {
            try
            {
                // Calculate the distance between the provided location and stored locations
                var nearbyPostcodes = _context.PostCodes
                    .AsEnumerable()
                    .Where(postcode => PostCodeLogic.CalculateDistance(latitude, longitude, postcode.Latitude, postcode.Longitude) <= maxDistanceInKilometers)
                    .Select(postcode => postcode.Postcode);

                return nearbyPostcodes.ToList();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/PostCodes
        [HttpPost]
        public async Task<ActionResult<PostCodes>> PostPostCodes(PostCodes postCodes)
        {
            try
            {
                if (_context.PostCodes == null)
                {
                    return Problem("Entity set 'DatabaseContext.PostCodes'  is null.");
                }
                _context.PostCodes.Add(postCodes);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPostCodes", new { id = postCodes.Id }, postCodes);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
