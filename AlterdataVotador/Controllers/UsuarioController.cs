using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlterdataVotador.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AlterdataVotador.Services;
using System.Net.Http;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlterdataVotador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize()]
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUsuarioService _usuarioService;
        private HttpRequestMessage req;

        public UsuarioController(ApplicationDbContext db, IUsuarioService usuarioService)
        {
            _db = db;
            _usuarioService = usuarioService;

        }

        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult Authenticate([FromBody]Usuario usuario)
        {
            usuario.Senha = _usuarioService.Encrypt(usuario);
            var user = _usuarioService.Authenticate(usuario.Email, usuario.Senha);
            if (user == null)
                return BadRequest(new { message = "Usuario ou senha estão incorretos." });

            return Ok(user);
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> Salvar(Usuario usuario)
        {
            var res = _usuarioService.ValidaEmail(usuario.Email);
            if(res == usuario.Email)
            {
                usuario.Senha = _usuarioService.Encrypt(usuario);
                _db.Usuarios.Add(usuario);
                await _db.SaveChangesAsync();
                return CreatedAtAction("BuscarUm", new { id = usuario.Id }, usuario);
            }
            else
            {
                return BadRequest(new { message = res });
            }
            
        }

        // GET: api/Usuario
        [HttpGet]
        public ActionResult<List<Usuario>> BuscarTodos()
        {
            return _db.Usuarios.ToList();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "rh")]
        public ActionResult<Usuario> BuscarUm(long id)
        {
            var item = _db.Usuarios.Find(id);
            if(item == null)
            {
                return NotFound(new { message = "Usuário não encontrado!"});
            }
            else
            {
                return item;
            } 
        }

        // PUT: api/Usuario
        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(long id, Usuario usuario)
        {
            if(id != usuario.Id)
            {
                return BadRequest(new { message = "ID de usuário não informado!" });
            }
            else
            {
                usuario.Senha = _usuarioService.Encrypt(usuario);
                _db.Entry(usuario).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return CreatedAtAction("BuscarUm", new { id = usuario.Id }, usuario);
            }
        }

        // Delete: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> ExcluirUm(long id)
        {
            var item = await _db.Usuarios.FindAsync(id);
            if(item == null)
            {
                return NotFound(new { message = "Usuário não encontrado!" });
            }
            else
            {
                _db.Usuarios.Remove(item);
                await _db.SaveChangesAsync();

                return item;
            }

        }
    }
}
