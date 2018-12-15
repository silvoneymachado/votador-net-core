using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlterdataVotador.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AlterdataVotador.Services;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlterdataVotador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class RecursoController : Controller
    {
        private readonly ApplicationDbContext _db;
        Recurso rec;

        public RecursoController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Insere um novo recurso no banco de dados
        /// </summary>
        /// <remarks>
        /// Necessário informar o token no header: 
        /// 
        ///     header: 
        ///     {
        ///         "Authorization": "Bearer " + token,
        ///         "Content-Type": "application/json"
        ///     }
        ///     
        /// </remarks>
        /// <param name="recurso"></param>
        /// <returns>O recurso informado com id</returns>
        /// <response code="200">O recurso informado com id</response>
        /// <response code="400">Caso algum campo esteja vazio</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        [HttpPost]
        public async Task<ActionResult<Recurso>> Salvar(Recurso recurso)
        {
            rec = new Recurso();
            var res = rec.ValidaRecurso(recurso);
            if (res == "ok")
            {
                _db.Recursos.Add(recurso);
                await _db.SaveChangesAsync();
                return CreatedAtAction("BuscarUm", new { id = recurso.Id }, recurso);
            }
            else
            {
                return BadRequest(new { message = res });
            }

        }

        /// <summary>
        /// Obtém uma lista de todos os recursos
        /// </summary>
        /// <remarks>
        /// Necessário informar o token no header: 
        /// 
        ///     header: 
        ///     {
        ///         "Authorization": "Bearer " + token,
        ///         "Content-Type": "application/json"
        ///     }
        ///     
        /// </remarks>
        /// <returns>Uma lista com todos os recursos cadastrados</returns>
        /// <response cod="200">Uma lista com todos os recursos cadastrados</response>
        /// <response code="400">Caso algum campo esteja vazio</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        [HttpGet]
        public ActionResult<List<Recurso>> BuscarTodos()
        {
            return _db.Recursos.ToList();
        }

        /// <summary>
        /// Obtém um recurso com base em seu ID
        /// </summary>
        /// <remarks>
        /// Necessário informar o token no header: 
        /// 
        ///     header: 
        ///     {
        ///         "Authorization": "Bearer " + token,
        ///         "Content-Type": "application/json"
        ///     }
        ///     
        /// </remarks>
        /// <returns>Um recurso com base em seu ID</returns>
        /// <param name="id"></param>
        /// <response code="200">Um recurso com base em seu ID</response> 
        /// <response code="400">Caso o id não seja informado</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        [HttpGet("{id}")]
        public ActionResult<Recurso> BuscarUm(long id)
        {
            var item = _db.Recursos.Find(id);
            if (item == null)
            {
                return NotFound(new { message = "ID de recurso não informado!" });
            }
            else
            {
                return item;
            }
        }

        /// <summary>
        /// Obtém um lista de recursos ordenada pelo numero de votos recebidos
        /// </summary>
        /// <remarks>
        /// Necessário informar o token no header: 
        /// 
        ///     header: 
        ///     {
        ///         "Authorization": "Bearer " + token,
        ///         "Content-Type": "application/json"
        ///     }
        ///     
        /// </remarks>
        /// <returns>Uma lista de recursos em ordem decrescente pelos votos</returns> 
        /// <response code="401">Caso não possua token de acesso</response> 
        [HttpGet("ranking/")]
        public ActionResult<List<RecursoRanking>> RankingDescrescente()
        {
            List<RecursoRanking> rrLst = new List<RecursoRanking> { };
            var recursoLst = _db.Recursos.ToList();
            var votacoes = _db.Votacoes.ToList();

            foreach (Recurso r in recursoLst)
            {
                RecursoRanking rr = new RecursoRanking(r);
                rr.Qtd = _db.Votacoes.Count(v => v.IdRecurso == r.Id);
                rrLst.Add(rr);
            };

            if (rrLst.Count() > 0)
            {
                return rrLst.OrderByDescending(m => m.Qtd).ToList();
            }
            else
            {
                return NotFound(new { messagem = "Não há items a ser exibidos" });
            }

        }

        /// <summary>
        /// Edita um recurso com base em seu ID
        /// </summary>
        /// <remarks>
        /// Necessário informar o token no header: 
        /// 
        ///     header: 
        ///     {
        ///         "Authorization": "Bearer " + token,
        ///         "Content-Type": "application/json"
        ///     }
        ///     
        /// </remarks>
        /// <returns></returns>
        /// <param name="recurso"></param>
        /// <param name="id"></param>
        /// <response code="200">O Recurso editado</response> 
        /// <response code="400">Caso o id ou recurso não sejam informados</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(long id, Recurso recurso)
        {
            if (id != recurso.Id)
            {
                return BadRequest(new { message = "ID de recurso não informado!" });
            }
            else
            {
                rec = new Recurso();
                var res = rec.ValidaRecurso(recurso);
                if (res == "ok")
                {
                    _db.Entry(recurso).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return CreatedAtAction("BuscarUm", new { id = recurso.Id }, recurso);
                }
                else
                {
                    return BadRequest(new { message = res });
                }

            }
        }

        /// <summary>
        /// Deleta um recurso com base em seu ID
        /// </summary>
        /// <remarks>
        /// Necessário informar o token no header: 
        /// 
        ///     header: 
        ///     {
        ///         "Authorization": "Bearer " + token,
        ///         "Content-Type": "application/json"
        ///     }
        ///     
        /// </remarks>
        /// <returns></returns>
        /// <param name="id"></param>
        /// <response code="200">Mensagem: "Recurso excluído com sucesso!"</response> 
        /// <response code="400">Caso o id não seja informado</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        /// <response code="404">Caso não exista recurso com ID informado</response> 
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recurso>> ExcluirUm(long id)
        {
            var item = await _db.Recursos.FindAsync(id);
            if (item == null)
            {
                return NotFound(new { message = "Recurso não encontrado" });
            }
            else
            {
                _db.Recursos.Remove(item);
                await _db.SaveChangesAsync();

                return Ok(new { message = "Recurso excluído com sucesso!" });
            }

        }

    }
}
