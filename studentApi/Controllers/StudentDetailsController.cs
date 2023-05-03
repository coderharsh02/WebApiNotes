using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using studentApi.Models;

namespace studentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentDetailsController : ControllerBase
    {
        private readonly StudentContext _context;
        private readonly IMapper _autoMapper;


        public StudentDetailsController(StudentContext context, IMapper autoMapper)
        {
            _context = context;
            _autoMapper = autoMapper;
        }

        // GET: api/StudentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDetail>>> GetStudentDetails()
        {
          if (_context.StudentDetail == null)
          {
              return NotFound();
          }
          return _autoMapper.Map<List<StudentDetail>>(await new StudentContextProcedures(_context).GetStudentDetailAsync());
        }

        // GET: api/StudentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDetail>> GetStudentDetail(int id)
        {
          if (_context.StudentDetail == null)
          {
              return NotFound();
          }
            var studentDetail = await _context.StudentDetail.FindAsync(id);

            if (studentDetail == null)
            {
                return NotFound();
            }

            return studentDetail;
        }

        // PUT: api/StudentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentDetail(int id, StudentDetail studentDetail)
        {
            if (id != studentDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentDetailExists(id))
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

        // POST: api/StudentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentDetail>> PostStudentDetail(StudentDetail studentDetail)
        {
          if (_context.StudentDetail == null)
          {
              return Problem("Entity set 'StudentContext.StudentDetails'  is null.");
          }
            _context.StudentDetail.Add(studentDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentDetail", new { id = studentDetail.Id }, studentDetail);
        }

        // DELETE: api/StudentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentDetail(int id)
        {
            if (_context.StudentDetail == null)
            {
                return NotFound();
            }
            var studentDetail = await _context.StudentDetail.FindAsync(id);
            if (studentDetail == null)
            {
                return NotFound();
            }

            _context.StudentDetail.Remove(studentDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentDetailExists(int id)
        {
            return (_context.StudentDetail?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
