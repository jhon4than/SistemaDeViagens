using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sis_Controle_Viagens.Models
{
    public class Pacote
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Origem Obrigatório!")]
        public string Origem { get; set; }

        [Required(ErrorMessage = "Campo Destino Obrigatório!")]
        public string Destino { get; set; }

        [Required(ErrorMessage = "Data Saída Obrigatório!")]
        public DateTime Saida { get; set; }

        [Required(ErrorMessage = "Data Retorno Obrigatório!")]
        public DateTime Retorno { get; set; }

        [Required(ErrorMessage = "Preço Obrigatório!")]
        public double Preco { get; set; }

        public string User { get; set; }
    }
}
