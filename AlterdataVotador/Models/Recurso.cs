using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlterdataVotador.Models
{
    /// <summary>
    /// Recurso a ser votado no sistema
    /// </summary>
    [Table("recurso")]
    public class Recurso
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("descricao")]
        public string Descricao { get; set; }
        [Column("habilitado")]
        public bool Habilitado { get; set; }

        public string ValidaRecurso(Recurso recurso)
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
