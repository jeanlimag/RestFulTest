using RestFul.Entidades;
using RestFul.Input;
using RestFul.Models;
using RestFul.Repository;
using System.Runtime;

namespace RestFul.Services
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);
        Task<JogoViewModel> Obter(Guid id);
        Task<JogoViewModel> Inserir(InputModel jogo);
        Task Atualizar(Guid id, InputModel jogo);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }

    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRespository;
        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRespository = jogoRepository;
        }
        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRespository.Obter(pagina, quantidade);
            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco,
            })
                .ToList();
        }
        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _jogoRespository.Obter(id);
            if (jogo == null)
                return null;
            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }
        public async Task<JogoViewModel> Inserir(InputModel jogo)
        {
            var entidadeJogo = await _jogoRespository.Obter(jogo.Nome, jogo.Produtora);
            if (entidadeJogo.Count > 0)
                throw new Exception();
            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
            await _jogoRespository.Inserir(jogoInsert);
            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }
        public async Task Atualizar(Guid id, InputModel jogo)
        {
            var entidadeJogo = await _jogoRespository.Obter(id);
            if (entidadeJogo == null)
                throw new Exception();
            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;
            await _jogoRespository.Atualizar(entidadeJogo);
        }
        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRespository.Obter(id);
            if (entidadeJogo == null)
                throw new Exception();
            entidadeJogo.Preco = preco;
            await _jogoRespository.Atualizar(entidadeJogo);
        }
        public async Task Remover(Guid id)
        {
            var jogo = await _jogoRespository.Obter(id);
            if (jogo == null)
                throw new Exception();
            await _jogoRespository.Remover(id);
        }
        public void Dispose() => _jogoRespository?.Dispose();
    }
}
