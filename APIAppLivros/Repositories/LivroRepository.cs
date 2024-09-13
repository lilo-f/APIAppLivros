using APIAppLivros.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Livros.Repositories
{
    public class LivroRepository
    {
        private readonly string _connectionString;

        public LivroRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection Connection =>
            new MySqlConnection(_connectionString);

        public async Task<IEnumerable<Livro>> ListarTodos(bool? ativo = null)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM tb_livros";

                if (ativo.HasValue)
                {
                    sql += " WHERE Ativo = @Ativo";
                    return await conn.QueryAsync<Livro>(sql, new { Ativo = ativo });
                }
                else
                {
                    return await conn.QueryAsync<Livro>(sql);
                }
            }
        }
        public async Task<Livro> BuscarPorId(int id)
        {
            var sql = "SELECT * FROM tb_livros WHERE Id = @Id";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Livro>(sql, new { Id = id });
            }
        }

        public async Task<int> Deletar(int id)
        {
            var sql = "DELETE FROM tb_livros WHERE Id = @Id";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<int> Criar(Livro livro)
        {
            var sql = "INSERT INTO tb_livros (Titulo,Autor,AnoPublicacao,Genero,NumeroPaginas) " +
                "values (@Titulo,@Autor,@AnoPublicacao,@Genero,@NumeroPaginas)";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql,
                    new
                    {
                        Titulo = livro.Titulo,
                        Autor = livro.Autor,
                        AnoPublicacao = livro.AnoPublicacao,
                        Genero = livro.Genero,
                        NumeroPaginas = livro.NumeroPaginas
                    });
            }
        }

        public async Task<int> Atualizar(Livro livro)
        {
            var sql = "UPDATE tb_livros set Titulo = @Titulo, Autor = @Autor, AnoPublicacao = @AnoPublicacao, Genero = @Genero, NumeroPaginas = @NumeroPaginas WHERE Id = @id";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, livro);
            }
        }

    }
}
