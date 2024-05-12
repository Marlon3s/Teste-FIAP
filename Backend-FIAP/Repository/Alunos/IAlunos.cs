using Backend_FIAP.Models;

namespace Backend_FIAP.Repository.Alunos
{
    public interface IAlunos
    {
        AlunoModel SearchAluno(int id);
        bool InsertAluno(AlunoModel alunoModel);
        bool UpdateAluno(AlunoModel alunoModel);
        bool DeleteAluno(int id);
        List<AlunoModel> ListAlunos();
    }
}
