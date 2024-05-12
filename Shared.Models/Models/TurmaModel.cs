namespace Backend_FIAP.Models
{
    public class TurmaModel
    {
        public int id { get; set; }
        //NOTA: No diagrama estava pedindo uma FOREIGN KEY para CURSOS.
        //Entretando não existe tabela curso no diagrama, portanto substitui por um VARCHAR com o nome do curso.
        public string curso { get; set; }
        public string turma { get; set; }
        public int ano { get; set; }
    }
}
