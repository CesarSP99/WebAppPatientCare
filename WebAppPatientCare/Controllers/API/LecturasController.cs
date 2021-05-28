using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppPatientCare.Models;

namespace WebAppPatientCare.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturasController : ControllerBase
    {
        private readonly dbPatientCareContext _context;

        public LecturasController(dbPatientCareContext context)
        {
            _context = context;
        }

        // GET: api/Lecturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lectura>>> GetLecturas()
        {
            return await _context.Lecturas.ToListAsync();
        }

        // GET: api/Lecturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Lectura>>> GetLectura(int id)
        {
            return await _context.Lecturas.Where(l => l.IdPaciente == id).ToListAsync();
            /*
            if (lectura == null)
            {
                return NotFound();
            }

            return lectura;
            */
        }

        // PUT: api/Lecturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLectura(int id, Lectura lectura)
        {
            if (id != lectura.IdLectura)
            {
                return BadRequest();
            }

            _context.Entry(lectura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturaExists(id))
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

        // POST: api/Lecturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lectura>> PostLectura(Lectura lectura)
        {
            _context.Lecturas.Add(lectura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLectura", new { id = lectura.IdLectura }, lectura);
        }

        // DELETE: api/Lecturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLectura(int id)
        {
            var lectura = await _context.Lecturas.FindAsync(id);
            if (lectura == null)
            {
                return NotFound();
            }

            _context.Lecturas.Remove(lectura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LecturaExists(int id)
        {
            return _context.Lecturas.Any(e => e.IdLectura == id);
        }
    }
}
