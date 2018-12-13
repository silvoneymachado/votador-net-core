using AlterdataVotador.Helpers;
using AlterdataVotador.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlterdataVotador.Services
{
    public interface IUsuarioService
    {
        Usuario Authenticate(string nome, string senha);
        IEnumerable<Usuario> BuscarTodos();
    }

    public class UsuarioService : IUsuarioService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<Usuario> _users = new List<Usuario>
        {
            new Usuario { Id = 1, Nome = "admin", Email = "adminvotacao@alterdata.com", Senha = "@lterd@t@admin2o19" }
        };

        private readonly AppSettings _appSettings;

        public UsuarioService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Usuario Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Nome == username && x.Senha == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Senha = null;

            return user;
        }

        public IEnumerable<Usuario> BuscarTodos()
        {
            // return users without passwords
            return _users.Select(x => {
                x.Senha = null;
                return x;
            });
        }
    }
}
