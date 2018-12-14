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
        

        public VotacaoController(ApplicationDbContext db)
        {
            _db = db;
        }

        // POST: api/Votacao
        [HttpPost]
        public async Task<ActionResult<Votacao>> Salvar(Votacao votacao)
        {
            var res = await ValidaVoto(votacao);
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

        // GET: api/Votacao
        [HttpGet]
        public ActionResult<List<Votacao>> BuscarTodos()
        {
            return _db.Votacoes.ToList();
        }

        // GET: api/Votacao/5
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

        // PUT: api/Votacao/5
        [HttpPut("{id}")]
        private async Task<ActionResult> Atualizar(long id, Votacao votacao)
        {
            if (id != votacao.Id)
            {
                return BadRequest(new { message = "ID de votacao não informado!" });
            }
            else
            {
                var res = await ValidaVoto(votacao);
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

        // Delete: api/Votacao/5
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

        private async Task<string>ValidaVoto(Votacao voto)
        {
            var item = await _db.Votacoes.SingleOrDefaultAsync(v => v.IdRecurso == voto.IdRecurso && v.IdUsuario == voto.IdUsuario);
            if (item == null)
            {
                if (voto.Comentario.Length > 0)
                {
                    return "ok";
                }
                else
                {
                    return "É necessário preencher o campo de comentário!";
                }
            }
            else
            {
                return "Não é possível votar mais de uma vez em um recurso";
            }
        }
    }
}
