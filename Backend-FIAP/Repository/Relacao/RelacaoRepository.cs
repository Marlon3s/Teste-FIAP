using Backend_FIAP.Data;
using Backend_FIAP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Backend_FIAP.Repository.Relacao
{
    public class RelacaoRepository : IRelacao
    {
        private readonly FiapDbContext _context;
        private IConfiguration _configuration;
        private IDbConnection _connection;

        public RelacaoRepository(FiapDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DataBase"));
        }

        public bool InsertRelacao(int aluno_id, int turma_id)
        {
            try
            {
                _connection.Execute(@"INSERT INTO aluno_turma(aluno_id, turma_id, status) VALUES (@aluno_id, @turma_id, @status)", new { aluno_id = aluno_id, turma_id = turma_id, status = 1 });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Aluno_TurmaModel> ListRelacoes()
        {
            return _connection.Query<Aluno_TurmaModel>("SELECT * FROM aluno_turma WHERE status != 0").ToList();
        }

        public Aluno_TurmaModel SearchRelacao(int aluno_id, int turma_id)
        {
            return _connection.QuerySingleOrDefault<Aluno_TurmaModel>("SELECT * FROM aluno_turma WHERE aluno_id = @aluno_id AND turma_id = @turma_id", new { aluno_id = aluno_id, turma_id = turma_id });
        }
    }
}
