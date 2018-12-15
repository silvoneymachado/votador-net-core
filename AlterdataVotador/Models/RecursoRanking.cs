using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlterdataVotador.Models
{
    public class RecursoRanking
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Habilitado { get; set; }
        public int Qtd { get; set; }

        public RecursoRanking(Recurso recurso)
        {
            Id = recurso.Id;
            Nome = recurso.Nome;
            Descricao = recurso.Descricao;
        }
    }
}
