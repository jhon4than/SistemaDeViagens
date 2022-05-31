using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sis_Controle_Viagens.Data;
using Sis_Controle_Viagens.Models;

namespace Sis_Controle_Viagens.Controllers
{
    public class SuporteUsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuporteUsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuporteUsuarios
        public async Task<IActionResult> Index()
        {
              return _context.SuporteUsuarios != null ? 
                          View(await _context.SuporteUsuarios
                          .AsNoTracking()
                          .Where(x => x.User == User.Identity.Name)
                          .ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SuporteUsuarios'  is null.");
        }

        // GET: SuporteUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SuporteUsuarios == null)
            {
                return NotFound();
            }

            var suporteUsuario = await _context.SuporteUsuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suporteUsuario == null)
            {
                return NotFound();
            }

            if (suporteUsuario.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(suporteUsuario);
        }

        // GET: SuporteUsuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuporteUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Duvida,User")] SuporteUsuario suporteUsuario)
        {
            if (ModelState.IsValid)
            {
                suporteUsuario.User = User.Identity.Name;
                _context.Add(suporteUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suporteUsuario);
        }

        // GET: SuporteUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SuporteUsuarios == null)
            {
                return NotFound();
            }

            var suporteUsuario = await _context.SuporteUsuarios.FindAsync(id);
            if (suporteUsuario == null)
            {
                return NotFound();
            }

            if (suporteUsuario.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(suporteUsuario);
        }

        // POST: SuporteUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Duvida,User")] SuporteUsuario suporteUsuario)
        {
            if (id != suporteUsuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suporteUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuporteUsuarioExists(suporteUsuario.Id))
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
            return View(suporteUsuario);
        }

        // GET: SuporteUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SuporteUsuarios == null)
            {
                return NotFound();
            }

            var suporteUsuario = await _context.SuporteUsuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suporteUsuario == null)
            {
                return NotFound();
            }

            if (suporteUsuario.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(suporteUsuario);
        }

        // POST: SuporteUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SuporteUsuarios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SuporteUsuarios'  is null.");
            }
            var suporteUsuario = await _context.SuporteUsuarios.FindAsync(id);
            if (suporteUsuario != null)
            {
                _context.SuporteUsuarios.Remove(suporteUsuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuporteUsuarioExists(int id)
        {
          return (_context.SuporteUsuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
