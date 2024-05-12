using Backend_FIAP.Models;
using Backend_FIAP.Repository.Alunos;
using Backend_FIAP.Repository.Relacao;
using Backend_FIAP.Repository.Turmas;
using Microsoft.AspNetCore.Mvc;

namespace Backend_FIAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelacaoController : ControllerBase
    {
        private readonly IRelacao _relacao;

        public RelacaoController(IRelacao relacao)
        {
            _relacao = relacao;
        }

        [HttpGet("GetAllRelacoes")]
        public async Task<IActionResult> GetAllRelacoes()
        {
            try
            {
                List<Aluno_TurmaModel> relacoesList = _relacao.ListRelacoes();
                if (relacoesList != null && relacoesList.Any())
                {
                    return Ok(new
                    {
                        success = true,
                        code = 200,
                        aluno = relacoesList.ToList()
                    });
                }
                else
                {
                    return Ok(new
                    {
                        code = 204,
                        message = "Não existem relações ativas!"
                    });
                }
            }
            catch
            {
                return Ok(new
                {
                    code = 409,
                    message = "Um erro ocorreu na geração da resposta, tente novamente!"
                });
            }
        }

        [HttpPost("PostInsertRelacao")]
        public async Task<IActionResult> PostInsertRelacao(int idAluno, int idTurma)
        {
            try
            {
                Aluno_TurmaModel relacaoModel = _relacao.SearchRelacao(idAluno, idTurma);
                if (relacaoModel != null)
                {
                    return Ok(new
                    {
                        code = 400,
                        message = "Esse aluno já está cadastrado nessa turma!"
                    });
                }

                bool resultado = _relacao.InsertRelacao(idAluno, idTurma);
                if (resultado)
                {
                    return Ok(new
                    {
                        code = 200,
                        message = "Aluno cadastrado na turma com sucesso"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        code = 410,
                        message = "Um erro ocorreu, tente novamente!"
                    });
                }
            }
            catch
            {
                return Ok(new
                {
                    code = 409,
                    message = "Um erro ocorreu na geração da resposta, tente novamente!"
                });
            }
        }
    }
}
