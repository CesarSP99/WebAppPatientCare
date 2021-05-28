using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppPatientCare.Models;

namespace WebAppPatientCare.Controllers
{
    public class LecturasController : Controller
    {
        private readonly dbPatientCareContext _context;

        public LecturasController(dbPatientCareContext context)
        {
            _context = context;
        }

        // GET: Lecturas
        public async Task<IActionResult> Index()
        {
            var dbPatientCareContext = _context.Lecturas.Include(l => l.IdPacienteNavigation);
            return View(await dbPatientCareContext.ToListAsync());
        }

        // GET: Lecturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectura = await _context.Lecturas
                .Include(l => l.IdPacienteNavigation)
                .FirstOrDefaultAsync(m => m.IdLectura == id);
            if (lectura == null)
            {
                return NotFound();
            }

            return View(lectura);
        }

        // GET: Lecturas/Create
        public IActionResult Create()
        {
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente");
            return View();
        }

        // POST: Lecturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLectura,IdPaciente,RitmoCardiaco,SaturacionOxigeno,FechaMedicion")] Lectura lectura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lectura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", lectura.IdPaciente);
            return View(lectura);
        }

        // GET: Lecturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectura = await _context.Lecturas.FindAsync(id);
            if (lectura == null)
            {
                return NotFound();
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", lectura.IdPaciente);
            return View(lectura);
        }

        // POST: Lecturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLectura,IdPaciente,RitmoCardiaco,SaturacionOxigeno,FechaMedicion")] Lectura lectura)
        {
            if (id != lectura.IdLectura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lectura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LecturaExists(lectura.IdLectura))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", lectura.IdPaciente);
            return View(lectura);
        }

        // GET: Lecturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectura = await _context.Lecturas
                .Include(l => l.IdPacienteNavigation)
                .FirstOrDefaultAsync(m => m.IdLectura == id);
            if (lectura == null)
            {
                return NotFound();
            }

            return View(lectura);
        }

        // POST: Lecturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lectura = await _context.Lecturas.FindAsync(id);
            _context.Lecturas.Remove(lectura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LecturaExists(int id)
        {
            return _context.Lecturas.Any(e => e.IdLectura == id);
        }
    }
}
