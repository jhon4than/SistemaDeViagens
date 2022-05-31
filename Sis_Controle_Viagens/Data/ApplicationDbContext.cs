using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sis_Controle_Viagens.Models;

namespace Sis_Controle_Viagens.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pacote> Pacotes { get; set; }
        public DbSet<SuporteUsuario> SuporteUsuarios { get; set; }
        public DbSet<Sis_Controle_Viagens.Models.LogAuditoria>? LogAuditoria { get; set; }
    }
}