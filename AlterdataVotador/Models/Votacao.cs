using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlterdataVotador.Models
{
    [Table("votacao")]
    public class Votacao
    {
        private readonly ApplicationDbContext _db;

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("idusuario")]
        public long IdUsuario { get; set; }
        [Column("idrecurso")]
        public long IdRecurso { get; set; }
        [Column("datahora")]
        public DateTime DataHora { get; set; }
        [Column("comentario")]
        public string Comentario { get; set; }

        public Votacao() { }

        public Votacao(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<string> ValidaVoto(Votacao voto)
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
