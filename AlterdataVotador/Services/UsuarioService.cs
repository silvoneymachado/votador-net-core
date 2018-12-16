using AlterdataVotador.Controllers;
using AlterdataVotador.Helpers;
using AlterdataVotador.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
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
        private List<Usuario> _users;
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _db;

        public UsuarioService(IOptions<AppSettings> appSettings, ApplicationDbContext db)
        {
            _appSettings = appSettings.Value;
            _db = db;
        }

        public Usuario Authenticate(string email, string password)
        {
            _users = _db.Usuarios.ToList();
            var user = _users.SingleOrDefault(x => x.Email == email && x.Senha == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            // update token on database
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChangesAsync();
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
