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
    public class BlockchainController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlockchainController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Blockchain
        public async Task<IActionResult> Index()
        {
              return _context.blocks != null ? 
                          View(await _context.blocks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.blocks'  is null.");
        }

        // GET: Blockchain/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.blocks == null)
            {
                return NotFound();
            }

            var block = await _context.blocks
                .FirstOrDefaultAsync(m => m.Height == id);
            if (block == null)
            {
                return NotFound();
            }

            return View(block);
        }

        // GET: Blockchain/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blockchain/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Contract,Creator")] Block block)
        {
            if (ModelState.IsValid)
            {
                _context.Add(block);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(block);
        }

        // GET: Blockchain/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.blocks == null)
            {
                return NotFound();
            }

            var block = await _context.blocks.FindAsync(id);
            if (block == null)
            {
                return NotFound();
            }
            return View(block);
        }

        // POST: Blockchain/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Height,TimeStamp,PrevHash,Hash,Contract,Creator")] Block block)
        {
            if (id != block.Height)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(block);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlockExists(block.Height))
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
            return View(block);
        }

        // GET: Blockchain/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.blocks == null)
            {
                return NotFound();
            }

            var block = await _context.blocks
                .FirstOrDefaultAsync(m => m.Height == id);
            if (block == null)
            {
                return NotFound();
            }

            return View(block);
        }

        // POST: Blockchain/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.blocks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.blocks'  is null.");
            }
            var block = await _context.blocks.FindAsync(id);
            if (block != null)
            {
                _context.blocks.Remove(block);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlockExists(int id)
        {
          return (_context.blocks?.Any(e => e.Height == id)).GetValueOrDefault();
        }
    }
}
