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

        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Pacote> Pacotes { get; set; }
    }
}