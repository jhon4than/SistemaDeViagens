using System.ComponentModel.DataAnnotations;

namespace Sis_Controle_Viagens.Models
{
    public class LogAuditoria
    {
        [Display(Name = "Bloco#")]
        public int Id { get; set; }

        [Display(Name = "Detalhes Do Bloco")]
        public string DetalhesAuditoria { get; set; }

        [Display(Name = "Email Usuário")]
        public string EmailUsuario { get; set; }
    }
}
