using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sis_Controle_Viagens.Data;
using Sis_Controle_Viagens.Models;

namespace Sis_Controle_Viagens.Controllers
{
    [Authorize]
    public class PacotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PacotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pacotes
        public async Task<IActionResult> Index()
        {
            _context.LogAuditoria.Add(
            new LogAuditoria
            {
                EmailUsuario = User.Identity.Name,
                DetalhesAuditoria = String.Concat("Entrou na Tela de Listagem de Pacotes. - ", DateTime.Now.ToString())
            });
            _context.SaveChanges();
            return _context.Pacotes != null ?
                        View(await _context.Pacotes
                        .AsNoTracking()
                        .Where(x => x.User == User.Identity.Name)
                        .ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Pacotes'  is null.");
        }

        // GET: Pacotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pacotes == null)
            {
                return NotFound();
            }

            var pacote = await _context.Pacotes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacote == null)
            {
                return NotFound();
            }

            if (pacote.User != User.Identity.Name)
            {
                return NotFound();
            }

            _context.LogAuditoria.Add(
            new LogAuditoria
            {
                EmailUsuario = User.Identity.Name,
                DetalhesAuditoria = string.Concat("Entrou na tela de Detalhes do Pacote: ",
                pacote.Id, " - ", pacote.Nome, " - " , DateTime.Now.ToString())
            });
            _context.SaveChanges();

            return View(pacote);
        }

        // GET: Pacotes/Create
        public IActionResult Create()
        {
            _context.LogAuditoria.Add(
            new LogAuditoria
            {
                EmailUsuario = User.Identity.Name,
                DetalhesAuditoria = String.Concat("Entrou na tela de Cadastro de Pacote. - ", DateTime.Now.ToString())
            });

            _context.SaveChanges();

            return View();
        }

        // POST: Pacotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Origem,Destino,Saida,Retorno,Preco")] Pacote pacote)
        {
            if (ModelState.IsValid)
            {
                pacote.User = User.Identity.Name;
                _context.Add(pacote);
                await _context.SaveChangesAsync();

                _context.LogAuditoria.Add(
                new LogAuditoria
                {
                    EmailUsuario = User.Identity.Name,
                    DetalhesAuditoria = String.Concat("Cadastrou o Pacote: ",
                    pacote.Nome, " Data de cadastro: - ", DateTime.Now.ToString())
                });

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(pacote);
        }

        // GET: Pacotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pacotes == null)
            {
                return NotFound();
            }

            var pacote = await _context.Pacotes.FindAsync(id);
            if (pacote == null)
            {
                return NotFound();
            }

            _context.LogAuditoria.Add(
            new LogAuditoria
            {
                EmailUsuario = User.Identity.Name,
                DetalhesAuditoria = string.Concat("Entrou na tela de Edição do Pacote: ",
                pacote.Id, " - ", pacote.Nome, " - ", DateTime.Now.ToString())
            });
            _context.SaveChanges();

            if (pacote.User != User.Identity.Name)
            {
                return NotFound();
            }
            return View(pacote);
        }

        // POST: Pacotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Origem,Destino,Saida,Retorno,Preco")] Pacote pacote)
        {
            if (id != pacote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pacote.User = User.Identity.Name;
                    _context.Update(pacote);
                    await _context.SaveChangesAsync();

                    _context.LogAuditoria.Add(
                new LogAuditoria
                {
                    EmailUsuario = User.Identity.Name,
                    DetalhesAuditoria = String.Concat("Atualizou o Pacote: ",
                    pacote.Nome, " Data de Atualização: - ", DateTime.Now.ToString())
                });
                    _context.SaveChanges();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacoteExists(pacote.Id))
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
            return View(pacote);
        }

        // GET: Pacotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pacotes == null)
            {
                return NotFound();
            }

            var pacote = await _context.Pacotes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacote == null)
            {
                return NotFound();
            }

            if (pacote.User != User.Identity.Name)
            {
                return NotFound();
            }

            _context.LogAuditoria.Add(
            new LogAuditoria
            {
                EmailUsuario = User.Identity.Name,
                DetalhesAuditoria = string.Concat("Entrou na tela de Exclusão do Pacote: ",
                pacote.Id, " - ", pacote.Nome, " - ", DateTime.Now.ToString())
            });
            _context.SaveChanges();

            return View(pacote);
        }

        // POST: Pacotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pacotes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pacotes'  is null.");
            }
            var pacote = await _context.Pacotes.FindAsync(id);
            if (pacote != null)
            {
                _context.Pacotes.Remove(pacote);
            }

            await _context.SaveChangesAsync();

            _context.LogAuditoria.Add(
                new LogAuditoria
                {
                    EmailUsuario = User.Identity.Name,
                    DetalhesAuditoria = String.Concat("Deletou o Pacote: ",
                    pacote.Nome," Data de exclusão: - ", DateTime.Now.ToString())
                });
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool PacoteExists(int id)
        {
            return (_context.Pacotes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
