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
    public class RecursoController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const string SQL_BUSCAR_RANKING = "select r.id, r.nome, r.descricao, r.habilitado, count(v.id) as qtd from votacao v join recurso r on v.idrecurso = r.id group by r.id order by qtd desc";

        public RecursoController(ApplicationDbContext db)
        {
            _db = db;
        }

        // POST: api/Recurso
        [HttpPost]
        public async Task<ActionResult<Recurso>> Salvar(Recurso recurso)
        {
            var res = ValidaRecurso(recurso);
            if(res == "ok")
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

        // GET: api/Recurso
        [HttpGet]
        public ActionResult<List<Recurso>> BuscarTodos()
        {
            return _db.Recursos.ToList();
        }

        // GET: api/Recurso/5
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

        // GET: api/Recurso/Ranking
        [HttpGet("ranking/")]
        public ActionResult<List<Recurso>> RankingDescrescente()
        {
            return _db.Recursos.FromSql(SQL_BUSCAR_RANKING).ToList();
        }

        // PUT: api/Recurso/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(long id, Recurso recurso)
        {
            if (id != recurso.Id)
            {
                return BadRequest(new { message = "ID de recurso não informado!" });
            }
            else
            {
                var res = ValidaRecurso(recurso);
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

        // Delete: api/Recurso/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recurso>> ExcluirUm(long id)
        {
            var item = await _db.Recursos.FindAsync(id);
            if (item == null)
            {
                return NotFound(new { message = "Recurso não encontrado"});
            }
            else
            {
                _db.Recursos.Remove(item);
                await _db.SaveChangesAsync();

                return item;
            }

        }


        private string ValidaRecurso(Recurso recurso)
        {

            if (recurso.Descricao.Length <= 0 || recurso.Nome.Length <= 0)
            {
                return "É necessário preencher todos os campos!";
            }
            else
            {
                return "ok";
            }
        }
    }
}
