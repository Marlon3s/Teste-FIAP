using Backend_FIAP.Models;

namespace Backend_FIAP.Repository.Relacao
{
    public interface IRelacao
    {
        Aluno_TurmaModel SearchRelacao(int idAluno, int idTurma);
        bool InsertRelacao(int idAluno, int idTurma);
        bool DeleteRelacao(int idAluno, int idTurma);
        List<Aluno_TurmaModel> ListRelacoes();
    }
}
