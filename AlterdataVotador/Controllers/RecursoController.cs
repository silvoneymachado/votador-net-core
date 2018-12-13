using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlterdataVotador.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlterdataVotador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RecursoController(ApplicationDbContext db)
        {
            _db = db;
        }

        // POST: api/Recurso
        [HttpPost]
        public async Task<ActionResult<Recurso>> Salvar(Recurso recurso)
        {
            _db.Recursos.Add(recurso);
            await _db.SaveChangesAsync();
            return CreatedAtAction("BuscarUm", new { id = recurso.Id }, recurso);
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
                return NotFound();
            }
            else
            {
                return item;
            }
        }

        // PUT: api/Recurso/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(long id, Recurso recurso)
        {
            if (id != recurso.Id)
            {
                return BadRequest();
            }
            else
            {
                _db.Entry(recurso).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return CreatedAtAction("BuscarUm", new { id = recurso.Id }, recurso);
            }
        }

        // Delete: api/Recurso/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recurso>> ExcluirUm(long id)
        {
            var item = await _db.Recursos.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _db.Recursos.Remove(item);
                await _db.SaveChangesAsync();

                return item;
            }

        }
    }
}
