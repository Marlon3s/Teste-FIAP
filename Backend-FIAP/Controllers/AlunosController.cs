using Backend_FIAP.Models;
using Backend_FIAP.Repository.Alunos;
using Backend_FIAP.Repository.Relacao;
using Backend_FIAP.Repository.Turmas;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Backend_FIAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunos _alunos;

        public AlunosController(IAlunos alunos)
        {
            _alunos = alunos;
        }

        #region ALUNOS ENDPOINTS

        [HttpGet("GetAllAlunos")]
        public async Task<IActionResult> GetAllAlunos()
        {
            try
            {
                List<AlunoModel> alunoList = _alunos.ListAlunos();
                if (alunoList != null && alunoList.Any())
                {
                    return Ok(new
                    {
                        success = true,
                        code = 200,
                        aluno = alunoList.ToList()
                    });
                }
                else
                {
                    return Ok(new
                    {
                        code = 204,
                        message = "Não existem alunos ativos!"
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

        [HttpPost("PostInsertAluno")]
        public async Task<IActionResult> PostInsertAluno(string nome, string usuario, string senha)
        {
            try
            {
                AlunoModel alunoModel = new AlunoModel();

                bool hasStrongPassword = senha.Length >= 8 &&
                          senha.Any(char.IsUpper) &&
                          senha.Any(char.IsLower) &&
                          senha.Any(char.IsDigit) &&
                          senha.Any(x => !char.IsLetterOrDigit(x));

                if (!hasStrongPassword)
                {
                    return Ok(new
                    {
                        code = 400,
                        message = "Senha fraca, por favor certifique-se de colocar mais de 8 caracteres contendo letras, números e caracteres especiais!"
                    });
                }
                //Verificação de string, para caso exista algum caractere especial ou número em sua composição.                            
                bool hasSpecialChar = Regex.IsMatch(nome, @"[^a-zA-Z\s]");
                if (hasSpecialChar)
                {
                    return Ok(new
                    {
                        code = 400,
                        message = "O parametro 'nome', não pode contar números ou caracteres especiais"
                    });
                }
                else
                {               
                    alunoModel.usuario = usuario;
                    alunoModel.nome = nome;
                    alunoModel.senha = senha;
                    alunoModel.status = 1;
                    bool resultado = _alunos.InsertAluno(alunoModel);
                    if (resultado)
                    {
                        return Ok(new
                        {
                            code = 200,
                            message = "Aluno cadastrado com sucesso"
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

        [HttpPost("PostUpdateAluno")]
        public async Task<IActionResult> PostUpdateAluno(int id, string nome, string usuario, string senha)
        {
            try
            {
                AlunoModel alunoModel = _alunos.SearchAluno(id);
                if (alunoModel != null)
                {
                    bool hasStrongPassword = senha.Length >= 8 &&
                          senha.Any(char.IsUpper) &&
                          senha.Any(char.IsLower) &&
                          senha.Any(char.IsDigit) &&
                          senha.Any(x => !char.IsLetterOrDigit(x));

                    if (!hasStrongPassword)
                    {
                        return Ok(new
                        {
                            code = 400,
                            message = "Senha fraca, por favor certifique-se de colocar mais de 8 caracteres contendo letras ,números e caracteres especiais!"
                        });
                    }
                    //Verificação de string, para caso exista algum caractere especial ou número em sua composição.                            
                    bool hasSpecialChar = Regex.IsMatch(nome, @"[^a-zA-Z\s]");
                    if (hasSpecialChar)
                    {
                        return Ok(new
                        {
                            code = 400,
                            message = "O parametro 'nome', não pode contar números ou caracteres especiais"
                        });
                    }
                    else 
                    {
                        alunoModel.nome = nome;
                        alunoModel.usuario = usuario;
                        alunoModel.senha = senha;

                        bool resultado = _alunos.UpdateAluno(alunoModel);
                        if (resultado)
                        {
                            return Ok(new
                            {
                                code = 200,
                                message = "Aluno atualizado com sucesso"
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
                }
                else
                {
                    return Ok(new
                    {
                        code = 204,
                        message = "Nenhum aluno encontrado com esse 'id'"
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

        [HttpPost("PostDeleteAluno")]
        public async Task<IActionResult> PostDeleteAluno(int id)
        {
            try
            {              
                bool resultado = _alunos.DeleteAluno(id);
                if (resultado)
                {
                    return Ok(new
                    {
                        code = 200,
                        message = "Aluno desligado com sucesso"
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
        #endregion
        
    }
}
