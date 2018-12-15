using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
    }
}
