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
    }
}
