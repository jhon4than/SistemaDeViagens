﻿using System;
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
    public class LogAuditoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LogAuditoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LogAuditorias
        public async Task<IActionResult> Index()
        {
              return _context.LogAuditoria != null ? 
                          View(await _context.LogAuditoria.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.LogAuditoria'  is null.");
        }

        // GET: LogAuditorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LogAuditoria == null)
            {
                return NotFound();
            }

            var logAuditoria = await _context.LogAuditoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logAuditoria == null)
            {
                return NotFound();
            }

            return View(logAuditoria);
        }

        // GET: LogAuditorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LogAuditorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DetalhesAuditoria,EmailUsuario")] LogAuditoria logAuditoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logAuditoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logAuditoria);
        }

        // GET: LogAuditorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LogAuditoria == null)
            {
                return NotFound();
            }

            var logAuditoria = await _context.LogAuditoria.FindAsync(id);
            if (logAuditoria == null)
            {
                return NotFound();
            }
            return View(logAuditoria);
        }

        // POST: LogAuditorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DetalhesAuditoria,EmailUsuario")] LogAuditoria logAuditoria)
        {
            if (id != logAuditoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logAuditoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogAuditoriaExists(logAuditoria.Id))
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
            return View(logAuditoria);
        }

        // GET: LogAuditorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LogAuditoria == null)
            {
                return NotFound();
            }

            var logAuditoria = await _context.LogAuditoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logAuditoria == null)
            {
                return NotFound();
            }

            return View(logAuditoria);
        }

        // POST: LogAuditorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LogAuditoria == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LogAuditoria'  is null.");
            }
            var logAuditoria = await _context.LogAuditoria.FindAsync(id);
            if (logAuditoria != null)
            {
                _context.LogAuditoria.Remove(logAuditoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogAuditoriaExists(int id)
        {
          return (_context.LogAuditoria?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
