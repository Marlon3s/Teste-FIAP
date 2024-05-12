namespace Backend_FIAP.Models
{
    public class TurmaModel
    {
        public int id { get; set; }
        public string curso { get; set; }
        public string turma { get; set; }
        public int ano { get; set; }
        //STATUS:
        //0 - Inativo
        //1 - Ativo
        public int status { get; set; }
    }
}
