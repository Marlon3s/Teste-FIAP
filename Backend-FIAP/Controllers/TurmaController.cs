using Backend_FIAP.Models;
using Backend_FIAP.Repository.Turmas;
using Microsoft.AspNetCore.Mvc;

namespace Backend_FIAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmas _turmas;

        public TurmaController(ITurmas turmas)
        {
            _turmas = turmas;
        }

        #region TURMA ENDPOINTS

        [HttpGet("GetAllTurmas")]
        public async Task<IActionResult> GetAllTurmas()
        {
            try
            {
                List<TurmaModel> turmaList = _turmas.ListTurmas();
                if (turmaList != null && turmaList.Any())
                {
                    return Ok(new
                    {
                        success = true,
                        code = 200,
                        aluno = turmaList.ToList()
                    });
                }
                else
                {
                    return Ok(new
                    {
                        code = 204,
                        message = "Não existem turmas ativas!"
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

        [HttpPost("PostInsertTurma")]
        public async Task<IActionResult> PostInsertTurma(string curso, string turma, int ano)
        {
            try
            {

                TurmaModel turmaModel = _turmas.SearchTurmaNome(turma);
                if(turmaModel != null)
                {
                    return Ok(new
                    {
                        code = 400,
                        message = "Já existe uma turma com esse nome!"
                    });
                }
                int anoAtual = DateTime.Now.Year;
                //Verificando se o ano informado é válido
                if (ano.ToString().Length > 4 || ano < DateTime.Now.Year)
                {
                    return Ok(new
                    {
                        code = 400,
                        message = "Informe um ano válido!"
                    });
                }

                turmaModel.curso = curso;
                turmaModel.turma = turma;
                turmaModel.ano = ano;
                turmaModel.status = 1;
                bool resultado = _turmas.InsertTurma(turmaModel);
                if (resultado)
                {
                    return Ok(new
                    {
                        code = 200,
                        message = "Turma cadastrada com sucesso"
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

        [HttpPost("PostUpdateTurma")]
        public async Task<IActionResult> PostUpdateTurma(int id, string curso, string turma, int ano)
        {
            try
            {
                TurmaModel turmaModel = _turmas.SearchTurmaId(id);
                if (turmaModel != null)
                {

                    //Verificando se já existe uma turma com o nome atualizado.
                    //Primeiro é verificado se o nome da turma mudou, para assim ser possivel verificar se o novo nome já foi cadastrado.
                    TurmaModel verificao = _turmas.SearchTurmaNome(turma);
                    if(turmaModel.turma != turma)
                    {
                        if (verificao != null)
                        {
                            return Ok(new
                            {
                                code = 400,
                                message = "Já existe uma turma com esse nome!"
                            });
                        }
                    }
                    
                    //Verificando se o ano fornecida é válido 
                    int anoAtual = DateTime.Now.Year;
                    if (ano.ToString().Length > 4 || ano < DateTime.Now.Year)
                    {
                        return Ok(new
                        {
                            code = 400,
                            message = "Informe um ano válido!"
                        });
                    }
                 
                    turmaModel.curso = curso;
                    turmaModel.turma = turma;
                    turmaModel.ano = ano;
                    turmaModel.status = 1;

                    bool resultado = _turmas.UpdateTurma(turmaModel);
                    if (resultado)
                    {
                        return Ok(new
                        {
                            code = 200,
                            message = "Turma atualizada com sucesso"
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
                else
                {
                    return Ok(new
                    {
                        code = 204,
                        message = "Nenhuma turma encontrada com esse 'id'"
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

        [HttpPost("PostDeleteTurma")]
        public async Task<IActionResult> PostDeleteTurma(int id)
        {
            try
            {
                bool resultado = _turmas.DeleteTurma(id);
                if (resultado)
                {
                    return Ok(new
                    {
                        code = 200,
                        message = "Turma desativada com sucesso"
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
        #endregion TURMA ENDPOINTS
    }
}
