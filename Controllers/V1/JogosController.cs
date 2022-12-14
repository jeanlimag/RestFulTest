using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RestFul.Input;
using RestFul.Models;
using RestFul.Services;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace RestFul.Controllers.V1
{

    [Route("RestFul/v1/[controller]")]
    [ApiController]

    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)

        {
            _jogoService = jogoService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogoService.Obter(pagina, quantidade);
            if (jogos.Count() == 0)
                return NoContent();
            return Ok(jogos);
        }
        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var jogo = await _jogoService.Obter(idJogo);
            if (jogo == null)
                return NoContent();
            return Ok(jogo);
        }
        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] InputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoService.Inserir(jogoInputModel);
                return Ok(jogo);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }
        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] InputModel jogoInputModel)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, jogoInputModel);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Esse jogo não existe");
            }
        }
        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarPreco([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, preco);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Esse jogo não existe");
            }
        }
        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoService.Remover(idJogo);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Esse jogo não existe");
            }
        }

    }
}
