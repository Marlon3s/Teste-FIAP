using Backend_FIAP.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_FIAP.Data
{
    public class FiapDbContext : DbContext
    {
        public FiapDbContext(DbContextOptions<FiapDbContext> options) : base(options)
        {

        }

        public DbSet<AlunoModel> aluno { get; set; }
        public DbSet<TurmaModel> turma { get; set; }
        public DbSet<Aluno_TurmaModel> aluno_turma { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno_TurmaModel>().HasKey(x => new {x.aluno_id, x.turma_id});
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true).Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DataBase"));
        }
    }
}
