using AlterdataVotador.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AlterdataVotador.Tests.Tests
{
    [TestFixture()]
    class VotacaoTest
    {
        private Votacao votacao;

        [SetUp]
        public void SetUp()
        {
            
            votacao = new Votacao()
            {
                IdRecurso = 1,
                IdUsuario = 10,
                Comentario = "Comentario teste"
            };
        }

        //[Test]
        //[Description("Deve receber OK como resposta à validação do voto")]
        //public async Task ValidaVotacaoTestOkAsync()
        //{
        //    var resposta = await votacao.ValidaVoto(votacao);
        //    var mensagemEsperada = "ok";
        //    Assert.AreEqual(mensagemEsperada, resposta);
        //}

        [Test]
        [Description("Deve validar o comentario do voto")]
        public void ValidaComentarioOk()
        {
            var resposta = votacao.ValidaComentario(votacao.Comentario);
            var mensagemEsperada = "ok";
            Assert.AreEqual(mensagemEsperada, resposta);
        }
    }
}
