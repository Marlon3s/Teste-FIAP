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

        public bool DeleteRelacao(int idAluno, int idTurma)
        {
            throw new NotImplementedException();
        }

        public bool InsertRelacao(int idAluno, int idTurma)
        {
            try
            {
                _connection.Execute(@"INSERT INTO aluno_turma(aluno_id, turma_id, status) VALUES (@idAluno, @idTurma, @status)", new { aluno_id = idAluno, turma_id = idTurma, status = 1 });
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

        public Aluno_TurmaModel SearchRelacao(int idAluno, int idTurma)
        {
            return _connection.QuerySingle<Aluno_TurmaModel>("SELECT * FROM aluno_turma WHERE aluno_id = @idAluno AND turma_id = @idTurma ", new { aluno_id = idAluno, turma_id = idTurma});
        }
    }
}
