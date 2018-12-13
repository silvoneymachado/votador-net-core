using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlterdataVotador.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlterdataVotador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotacaoController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const string SQL_BUSCAR_RANKING = "Select "; // Buscar todos os recursos e exibir a quantidade de votos de cada, sem repeti-los

        public VotacaoController(ApplicationDbContext db)
        {
            _db = db;
        }

        // POST: api/Votacao
        [HttpPost]
        public async Task<ActionResult<Votacao>> Salvar(Votacao votacao)
        {
            if (ValidaVoto(votacao.IdRecurso, votacao.IdUsuario))
            {
                _db.Votacoes.Add(votacao);
                await _db.SaveChangesAsync();
                return CreatedAtAction("BuscarUm", new { id = votacao.Id }, votacao);
            }
            else
            {
                return BadRequest();
            }
           
        }

        // GET: api/Votacao
        [HttpGet]
        public ActionResult<List<Votacao>> BuscarTodos()
        {
            return _db.Votacoes.ToList();
        }

        // GET: api/Votacao/Ranking
        [HttpGet("/ranking")]
        public ActionResult<List<Votacao>> RankingDescrescente()
        {
            return _db.Votacoes.FromSql(SQL_BUSCAR_RANKING).ToList();
        }

        // GET: api/Votacao/5
        [HttpGet("{id}")]
        public ActionResult<Votacao> BuscarUm(long id)
        {
            var item = _db.Votacoes.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return item;
            }
        }

        // PUT: api/Votacao/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(long id, Votacao votacao)
        {
            if (id != votacao.Id)
            {
                return BadRequest();
            }
            else
            {
                _db.Entry(votacao).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return CreatedAtAction("BuscarUm", new { id = votacao.Id }, votacao);
            }
        }

        // Delete: api/Votacao/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Votacao>> ExcluirUm(long id)
        {
            var item = await _db.Votacoes.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _db.Votacoes.Remove(item);
                await _db.SaveChangesAsync();

                return item;
            }

        }

        public Boolean ValidaVoto(long IdRecurso, long IdUsuario)
        {
            var item = _db.Votacoes.SingleOrDefaultAsync(v => v.IdRecurso == IdRecurso && v.IdUsuario == IdUsuario);
            if(item == null)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
