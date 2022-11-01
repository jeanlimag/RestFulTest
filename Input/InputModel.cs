using System.ComponentModel.DataAnnotations;

namespace RestFul.Input
{
    public class InputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 2 e 100 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da produtora deve conter entre 1 e 100 caracteres")]
        public string Produtora { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve ser de nominimo R$ 1 (Real) e maximo de R$ 1000 (Reais)")]
        public double Preco { get; set; }
    }
}
