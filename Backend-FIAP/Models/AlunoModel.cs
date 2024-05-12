﻿namespace Backend_FIAP.Models
{
    public class AlunoModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
        //STATUS:
        //0 - Inativo
        //1 - Ativo
        public int status { get; set; }
    }
}
