using Backend_FIAP.Data;
using Backend_FIAP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Backend_FIAP.Repository.Alunos
{
    public class AlunosRepository : IAlunos
    {
        private readonly FiapDbContext _context;
        private IConfiguration _configuration;
        private IDbConnection _connection;

        public AlunosRepository(FiapDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DataBase"));
        }

        public AlunoModel SearchAluno(int id)
        {        
            return _connection.QuerySingleOrDefault<AlunoModel>("SELECT * FROM aluno WHERE id = @id", new { id = id});
        }

        public bool InsertAluno(AlunoModel alunoModel)
        {
            try
            {
                _connection.Execute(@"INSERT INTO aluno(nome, usuario, senha, status) VALUES (@nome, @usuario, HASHBYTES('SHA2_256', @senha), @status)", new { nome = alunoModel.nome, usuario = alunoModel.usuario, senha = alunoModel.senha, status = alunoModel.status});
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAluno(int id)
        {
            try
            {
                _connection.Execute("UPDATE aluno SET status = 0 WHERE id = @id", new { id = id });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<AlunoModel> ListAlunos()
        {
            return _connection.Query<AlunoModel>("SELECT * FROM aluno WHERE status != 0").ToList();
        }

        public bool UpdateAluno(AlunoModel alunoModel)
        {
            try
            {
                _connection.Execute("UPDATE aluno SET nome = @nome, usuario = @usuario, senha = HASHBYTES('SHA2_256', @senha) WHERE id = @id", new {nome = alunoModel.nome, usuario = alunoModel.usuario, senha = alunoModel.senha, id = alunoModel.id});
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
