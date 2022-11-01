using System.ComponentModel.DataAnnotations;

namespace RestFul.Entidades
{
    public class Jogo
    {
        [Key]
        public Guid Id { get; set; }   
        public string? Nome { get; set; }
        public string?  Produtora { get; set; }
        public double Preco { get; set; }   

    }
}
