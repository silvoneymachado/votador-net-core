using AlterdataVotador.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlterdataVotador.Tests
{
    [TestFixture]
    public class RecursoTest
    {
        private Recurso recurso;

        [SetUp]
        public void SetUp()
        {
            recurso = new Recurso
            {
                Nome = "Projeto 001",
                Descricao = "Descricao do projeto",
                Habilitado = true
            };
        }

        [Test]
        [Description("Deve receber OK como resposta à validação do recurso")]
        public void ValidaRecursoTestOk()
        {
            var resposta = recurso.ValidaRecurso(recurso);
            var mensagemEsperada = "ok";
            Assert.AreEqual(mensagemEsperada, resposta);
        }

        [Test]
        [Description("Deve receber uma mensagem de erro ao enviar um objeto com campo em branco")]
        public void ValidaRecursoTestCampoVazio()
        {
            var mensagemEsperada = "É necessário preencher todos os campos!";

            recurso.Nome = "";
            var resposta = recurso.ValidaRecurso(recurso);
            
            Assert.AreEqual(mensagemEsperada, resposta);

            recurso.Descricao = "";
            resposta = recurso.ValidaRecurso(recurso);
            Assert.AreEqual(mensagemEsperada, resposta);

            recurso.Nome = "NovoNome";
            resposta = recurso.ValidaRecurso(recurso);
            Assert.AreEqual(mensagemEsperada, resposta);
        }
    }
}
