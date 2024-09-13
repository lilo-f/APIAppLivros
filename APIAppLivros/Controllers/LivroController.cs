
using APIAppLivros.Models;
using Livros.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AlunosAPI.Controllers
{
    [Route("api/pessoas")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly LivroRepository _livroRepository;

        public PessoaController(LivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        // GET: api/<PessoaController>
        [HttpGet]
        [Route("listar")]
        [SwaggerOperation(Summary = "Listar todos os livros", Description = "Este endpoint retorna um listagem de livros cadastrados.")]
        public async Task<IEnumerable<Livro>> Listartodos([FromQuery] bool? ativo = null)
        {
            return await _livroRepository.ListarTodos(ativo);
        }

        // GET api/<PessoaController>/5
        [HttpGet("detalhes/{id}")]
        [SwaggerOperation(
            Summary = "Obtém dados de um livro pelo ID",
            Description = "Este endpoint retorna todos os dados de um livro cadastrado filtrando pelo ID.")]
        public async Task<Livro> BuscarPorId(int id)
        {
            return await _livroRepository.BuscarPorId(id);
        }

        // POST api/<PessoaController>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastrar um novo livro",
            Description = "Este endpoint é responsavel por cadastrar um novo livro no banco")]
        public async void Criar([FromBody] Livro livro)
        {
            await _livroRepository.Criar(livro);
        }

        // PUT api/<PessoaController>/5
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualizar os dados de um livro filtrando pelo ID.",
            Description = "Este endpoint é responsavel por atualizar os dados de um livro no banco")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Livro livro)
        {
            livro.Id = id;
            await _livroRepository.Atualizar(livro);
            return Ok();
        }

        // DELETE api/<PessoaController>/5
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover um Livro filtrando pelo ID.",
            Description = "Este endpoint é responsavel por remover os dados de um livro no banco")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _livroRepository.Deletar(id);
            return Ok();
        }
    }

}
