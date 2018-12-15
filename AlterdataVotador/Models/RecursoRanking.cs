using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlterdataVotador.Models
{
    /// <summary>
    /// Utilizada para obter quantidade de votos e exibir.
    /// Herda de Recurso.
    /// </summary>
    public class RecursoRanking: Recurso
    {
        public int Qtd { get; set; }

        public RecursoRanking(Recurso recurso)
        {
            Id = recurso.Id;
            Nome = recurso.Nome;
            Descricao = recurso.Descricao;
        }
    }
}
