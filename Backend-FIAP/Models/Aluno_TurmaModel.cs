﻿namespace Backend_FIAP.Models
{
    public class Aluno_TurmaModel
    {
        public int aluno_id { get; set; }
        public AlunoModel aluno { get; set; }
        public int turma_id { get; set; }
        public TurmaModel turma { get; set; }
        //STATUS:
        //0 - Inativo
        //1 - Ativo
        public int status { get; set; }
    }
}
