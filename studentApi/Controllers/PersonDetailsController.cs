using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using studentApi.Models;

namespace studentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonDetailsController : ControllerBase
    {
        private readonly StudentContext _context;

        public PersonDetailsController(StudentContext context)
        {
            _context = context;
        }

        // GET: api/PersonDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDetail>>> GetPersonDetails()
        {
          if (_context.PersonDetail == null)
          {
              return NotFound();
          }
            return await _context.PersonDetail.ToListAsync();
        }

        // GET: api/PersonDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDetail>> GetPersonDetail(int id)
        {
          if (_context.PersonDetail == null)
          {
              return NotFound();
          }
            var personDetail = await _context.PersonDetail.FindAsync(id);

            if (personDetail == null)
            {
                return NotFound();
            }

            return personDetail;
        }

        // PUT: api/PersonDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonDetail(int id, PersonDetail personDetail)
        {
            if (id != personDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(personDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonDetailExists(id))
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

        // POST: api/PersonDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonDetail>> PostPersonDetail(PersonDetail personDetail)
        {
          if (_context.PersonDetail == null)
          {
              return Problem("Entity set 'StudentContext.PersonDetails'  is null.");
          }
          
            _context.PersonDetail.Add(personDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonDetail", new { id = personDetail.Id }, personDetail);
        }

        // DELETE: api/PersonDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonDetail(int id)
        {
            if (_context.PersonDetail == null)
            {
                return NotFound();
            }
            var personDetail = await _context.PersonDetail.FindAsync(id);
            if (personDetail == null)
            {
                return NotFound();
            }

            _context.PersonDetail.Remove(personDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonDetailExists(int id)
        {
            return (_context.PersonDetail?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
