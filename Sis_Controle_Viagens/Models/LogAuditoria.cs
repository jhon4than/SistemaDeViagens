using System.ComponentModel.DataAnnotations;

namespace Sis_Controle_Viagens.Models
{
    public class LogAuditoria
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Detalhes Auditoria")]
        public string DetalhesAuditoria { get; set; }

        [Display(Name = "Email Usuário")]
        public string EmailUsuario { get; set; }
    }
}
