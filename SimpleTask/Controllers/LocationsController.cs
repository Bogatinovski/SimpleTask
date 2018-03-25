using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleTask.Models;

namespace SimpleTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Locations")]
    public class LocationsController : Controller
    {
        private readonly C3Context _context;

        public LocationsController(C3Context context)
        {
            _context = context;
        }

        // GET: api/Locations
        /// <summary>
        /// Get a list of all locations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLocations()
        {
            IEnumerable<Location> locations = await _context.Locations.Include(l => l.Classrooms).ToListAsync();
            return Ok(locations);
        }

        // GET: api/Locations/5
        /// <summary>
        /// Get a single location by id
        /// </summary>
        /// <param name="id">The id of the location</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = await _context.Locations.SingleOrDefaultAsync(m => m.Id == id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // GET: api/Locations/5/Classrooms
        /// <summary>
        /// Get a list of Classroom objects for a given Location
        /// </summary>
        /// <param name="id">The id of the location</param>
        /// <returns></returns>
        [HttpGet("{id}/Classrooms")]
        public async Task<IActionResult> GetLocationClassrooms([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<Classroom> classrooms = await _context.Classrooms.Where(c => c.LocationId == id).ToListAsync();

            return Ok(classrooms);
        }

        // POST: api/Locations/5/Classrooms
        /// <summary>
        /// Add Classroom to a Location
        /// </summary>
        /// <param name="id">The id of the location for which a classroom will be added</param>
        /// <param name="classroom">The Classroom entity</param>
        /// <returns></returns>
        [HttpPost("{id}/Classrooms")]
        public async Task<IActionResult> PostLocationClassroom([FromRoute] int id, [FromBody] Classroom classroom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Classrooms.Add(classroom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = classroom.LocationId }, classroom);
        }

        // PUT: api/Locations/5
        /// <summary>
        /// Update a Location
        /// </summary>
        /// <param name="id">The id of the Location</param>
        /// <param name="location">The Location entity</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation([FromRoute] int id, [FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.Id)
            {
                return BadRequest();
            }

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locations
        /// <summary>
        /// Create a Location
        /// </summary>
        /// <param name="location">The Location entity</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostLocation([FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = location.Id }, location);
        }

        // DELETE: api/Locations/5
        /// <summary>
        /// Delete a Location by id
        /// </summary>
        /// <param name="id">The id of the location</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = await _context.Locations.SingleOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return Ok(location);
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
    }
}