namespace Frontend.FIAP.Objects.Receive
{
    public class ListAlunos
    {
        public bool success { get; set; }
        public int code { get; set; }
        public class Aluno
        {
            public int id { get; set; }
            public string nome { get; set; }
            public string usuario { get; set; }
            public string senha { get; set; }
            public int status { get; set; }
        }
    }
}
