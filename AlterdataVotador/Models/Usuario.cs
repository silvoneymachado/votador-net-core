using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlterdataVotador.Models
{
    /// <summary>
    /// Usuário do sistema
    /// Não é permitido o cadastro de dois usuários com o mesmo email.
    /// email e senha são usados no login obtendo o token de acesso.
    /// </summary>
    [Table("usuario")]
    public class Usuario
    {

        private readonly ApplicationDbContext _db;

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("senha")]
        public string Senha { get; set; }
        [Column("roles")]
        public string Roles { get; set; }
        [Column("token")]
        public string Token { get; set; }

        public Usuario(ApplicationDbContext db)
        {
            _db = db;
        }

        public string Encrypt(Usuario usuario)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(usuario.Email + usuario.Senha));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public string ValidaEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                
                List<Usuario> _users = _db.Usuarios.ToList();
                var user = _users.SingleOrDefault(x => x.Email == email);
                if (user == null)
                {
                    return email;
                }
                else
                {
                    return "O email informado já está em uso";
                }
            }
            catch (FormatException)
            {
                return "Formado de email inválido";
            }
            
        }
    }
}
