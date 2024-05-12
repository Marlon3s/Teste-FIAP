using Backend_FIAP.Models;

namespace Backend_FIAP.Repository.Turmas
{
    public interface ITurmas
    {
        TurmaModel SearchTurmaId(int id);
        TurmaModel SearchTurmaNome(string turma);

        bool InsertTurma(TurmaModel turmaModel);
        bool UpdateTurma(TurmaModel turmaModel);
        bool DeleteTurma(int id);
        List<TurmaModel> ListTurmas();
    }
}
