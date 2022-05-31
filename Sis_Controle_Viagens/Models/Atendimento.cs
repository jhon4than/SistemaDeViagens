using System.ComponentModel.DataAnnotations;

namespace Sis_Controle_Viagens.Models
{
    public class Atendimento
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Campo Nome Obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Dúvida Obrigatório!")]
        public string Duvida { get; set; }

        public string User { get; set; }
    }
}
