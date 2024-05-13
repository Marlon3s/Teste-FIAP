using Backend_FIAP.Models;

namespace Backend_FIAP.Repository.Relacao
{
    public interface IRelacao
    {
        Aluno_TurmaModel SearchRelacao(int aluno_id, int turma_id);
        bool InsertRelacao(int aluno_id, int turma_id);
        List<Aluno_TurmaModel> ListRelacoes();
    }
}
