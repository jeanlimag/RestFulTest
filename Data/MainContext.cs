using Microsoft.EntityFrameworkCore;
using RestFul.Models;

namespace RestFul.Data
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<JogoViewModel> Jogo { get; set; }
    }
}
