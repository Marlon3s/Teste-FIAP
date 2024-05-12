using Backend_FIAP.Data;
using Backend_FIAP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Backend_FIAP.Repository.Turmas
{
    public class TurmaRepository : ITurmas
    {

        private readonly FiapDbContext _context;
        private IConfiguration _configuration;
        private IDbConnection _connection;

        public TurmaRepository(FiapDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DataBase"));
        }

        public bool DeleteTurma(int id)
        {
            try
            {
                _connection.Execute("UPDATE turma SET status = 0 WHERE id = @id", new { id = id });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertTurma(TurmaModel turmaModel)
        {
            try
            {
                _connection.Execute(@"INSERT INTO turma(curso, turma, ano, status) VALUES (@curso, @turma, @ano, @status)", new { curso = turmaModel.curso, turma = turmaModel.turma, ano = turmaModel.ano, status = turmaModel.status });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<TurmaModel> ListTurmas()
        {
            return _connection.Query<TurmaModel>("SELECT * FROM turma WHERE status != 0").ToList();
        }
        public TurmaModel SearchTurmaId(int id)
        {         
            return _connection.QuerySingle<TurmaModel>("SELECT * FROM turma WHERE id = @id", new { id = id });
        }

        public TurmaModel SearchTurmaNome(string turma)
        {         
            return _connection.QuerySingle<TurmaModel>("SELECT * FROM turma WHERE turma = @turma", new { turma = turma });        
        }

        public bool UpdateTurma(TurmaModel turmaModel)
        {
            try
            {
                _connection.Execute("UPDATE turma SET curso = @curso, turma = @turma, ano = @ano WHERE id = @id", new { curso = turmaModel.curso, turma = turmaModel.turma, ano = turmaModel.ano, id = turmaModel.id });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
