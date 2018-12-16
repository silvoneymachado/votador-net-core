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
        private Usuario usr;

        public UsuarioController(ApplicationDbContext db, IUsuarioService usuarioService)
        {
            _db = db;
            _usuarioService = usuarioService;
        }


        /// <summary>
        /// Solicita um token de acesso.
        /// </summary>
        /// <remarks>
        /// Exemplo de request aceito:
        ///
        ///     POST /api/token
        ///     {
        ///        "email": "emailCadastrado@email.com",
        ///        "senha": senhaCadastrada no sistema
        ///     }
        ///
        /// </remarks>
        /// <param name="usuario"></param>
        /// <returns>token de acesso a ser armazenado localmente para futuras requisiçoes</returns>
        /// <response code="201">token de acesso</response>
        /// <response code="400">caso o item seja nulo</response> 
        /// <response code="401">caso usuario ou senha estejam incorretos</response> 
        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult Authenticate([FromBody]Usuario usuario)
        {
            usr = new Usuario(_db);
            usuario.Senha = usr.Encrypt(usuario);
            var user = _usuarioService.Authenticate(usuario.Email, usuario.Senha);
            if (user == null)
                return BadRequest(new { message = "Usuario ou senha estão incorretos." });

            return Ok(user);
        }

        /// <summary>
        /// Insere um novo usuario no banco de dados
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
        /// <param name="usuario"></param>
        /// <returns>O usuario informado com id</returns>
        /// <response code="200">O usuario informado com id</response>
        /// <response code="400">Caso algum campo esteja vazio</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        [HttpPost]
        public async Task<ActionResult<Usuario>> Salvar(Usuario usuario)
        {
            usr = new Usuario(_db);
            var res = usr.ValidaEmail(usuario.Email);
            if(res == usuario.Email)
            {
                usuario.Senha = usr.Encrypt(usuario);
                _db.Usuarios.Add(usuario);
                await _db.SaveChangesAsync();
                return CreatedAtAction("BuscarUm", new { id = usuario.Id }, usuario);
            }
            else
            {
                return BadRequest(new { message = res });
            }
            
        }

        /// <summary>
        /// Obtém uma lista de todos os usuários
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
        /// <returns>Uma lista com todos os usuários cadastrados</returns>
        /// <response cod="200">Uma lista com todos os usuários cadastrados</response>
        /// <response code="400">Caso algum campo esteja vazio</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        [HttpGet]
        public ActionResult<List<Usuario>> BuscarTodos()
        {
            List <Usuario> usrLst = _db.Usuarios.ToList();
            foreach(Usuario usr in usrLst)
            {
                usr.Senha = null;
            }

            return usrLst;
        }

        /// <summary>
        /// Obtém um usuário com base em seu ID
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
        /// <returns>Um usuário com base em seu ID</returns>
        /// <param name="id"></param>
        /// <response code="200">Um usuário com base em seu ID</response> 
        /// <response code="400">Caso o id não seja informado</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
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
                item.Senha = null;
                return item;
            } 
        }

        /// <summary>
        /// Edita um usuário com base em seu ID
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
        /// <param name="usuario"></param>
        /// <param name="id"></param>
        /// <response code="200">O Usuário editado</response> 
        /// <response code="400">Caso o id ou usuário não sejam informados</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(long id, Usuario usuario)
        {
            if(id != usuario.Id)
            {
                return BadRequest(new { message = "ID de usuário não informado!" });
            }
            else
            {
                usr = new Usuario(_db);
                usuario.Senha = usr.Encrypt(usuario);
                _db.Entry(usuario).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return Ok(new { message = "Usuário editado com sucesso!" });
            }
        }

        /// <summary>
        /// Deleta um usuário com base em seu ID
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
        /// <response code="200">Mensagem: "Usuário excluído com sucesso!"</response> 
        /// <response code="400">Caso o id não seja informado</response> 
        /// <response code="401">Caso não possua token de acesso</response> 
        /// <response code="404">Caso não exista usuário com ID informado</response> 
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

                return Ok(new { message = "Usuário excluído com sucesso!" });
            }

        }
    }
}
