using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlterdataVotador.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AlterdataVotador.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlterdataVotador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class VotacaoController : Controller
    {
        private readonly ApplicationDbContext _db;
        private Votacao vot;
        

        public VotacaoController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Insere um novo voto no banco de dados (um usuário não pode votar duas vezes no mesmo recurso)
        /// </summary>
        /// <param>voto</param>
        /// <returns>O voto informado com id</returns>
        /// <response code="200">O voto informado com id</response>
        /// <response code="400">Caso algum campo esteja vazio</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        [HttpPost]
        public async Task<ActionResult<Votacao>> Salvar(Votacao votacao)
        {
            vot = new Votacao(_db);
            var res = await vot.ValidaVoto(votacao);
            if (res == "ok")
            {
                votacao.DataHora = DateTime.Now;
                _db.Votacoes.Add(votacao);
                await _db.SaveChangesAsync();
                return CreatedAtAction("BuscarUm", new { id = votacao.Id }, votacao);
            }
            else
            {
                return BadRequest(new { message = res});
            }
           
        }

        /// <summary>
        /// Obtém uma lista de todos os votos
        /// </summary>
        /// <returns>Uma lista com todos os votos cadastrados</returns>
        /// <response cod="200">Uma lista com todos os votos cadastrados</response>
        /// <response code="400">Caso algum campo esteja vazio</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        [HttpGet]
        public ActionResult<List<Votacao>> BuscarTodos()
        {
            return _db.Votacoes.ToList();
        }

        /// <summary>
        /// Obtém um voto com base em seu ID
        /// </summary>
        /// <returns>Um voto com base em seu ID</returns>
        /// <param name="id"></param>
        /// <response code="200">Um voto com base em seu ID</response> 
        /// <response code="400">Caso o id não seja informado</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        [HttpGet("{id}")]
        public ActionResult<Votacao> BuscarUm(long id)
        {
            var item = _db.Votacoes.Find(id);
            if (item == null)
            {
                return NotFound(new { message = "Voto não encontrado!" } );
            }
            else
            {
                return item;
            }
        }

        /// <summary>
        /// Edita um voto com base em seu ID
        /// </summary>
        /// <returns></returns>
        /// <param name="votacao"></param>
        /// <param name="id"></param>
        /// <response code="200">O Recurso editado</response> 
        /// <response code="400">Caso o id ou voto não sejam informados</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        [HttpPut("{id}")]
        private async Task<ActionResult> Atualizar(long id, Votacao votacao)
        {
            if (id != votacao.Id)
            {
                return BadRequest(new { message = "ID de votacao não informado!" });
            }
            else
            {
                vot = new Votacao(_db);
                var res = await vot.ValidaVoto(votacao);
                if (res == "ok")
                {
                    votacao.DataHora = DateTime.Now;
                    _db.Entry(votacao).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return CreatedAtAction("BuscarUm", new { id = votacao.Id }, votacao);
                }
                else
                {
                    return BadRequest(new { message = res });
                }
                
            }
        }

        /// <summary>
        /// Deleta um voto com base em seu ID
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        /// <response code="200">Mensagem: "Voto excluído com sucesso!"</response> 
        /// <response code="400">Caso o id não seja informado</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        /// <response code="404">Caso não exista voto com ID informado</response>
        [HttpDelete("{id}")]
        private async Task<ActionResult<Votacao>> ExcluirUm(long id)
        {
            var item = await _db.Votacoes.FindAsync(id);
            if (item == null)
            {
                return NotFound(new { message = "Voto não encontrado!" });
            }
            else
            {
                _db.Votacoes.Remove(item);
                await _db.SaveChangesAsync();

                return item;
            }

        }

    }
}
