using AlterdataVotador.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlterdataVotador.Tests.Unit
{
    [TestFixture]
    public class UsuarioTest
    {
        private Usuario usuario;
        private readonly ApplicationDbContext _db;

        public UsuarioTest(ApplicationDbContext db)
        {
            _db = db;
        }

        [SetUp]
        public void SetUp()
        {
            usuario = new Usuario(_db)
            {
                Nome = "teste",
                Email = "teste@teste1234.com",
                Senha = "123mudar",
                Roles = "Comum"
            };
        }


        //[Test]
        //[Description("Deve verificar se o email informado está disponível")]
        //public void ValidaEmailOk()
        //{
        //    var resposta = usuario.ValidaEmail(usuario.Email);
        //    var mensagemEsperada = usuario.Email;
        //    Assert.AreEqual(mensagemEsperada, resposta);
        //}

        //[Test]
        //[Description("Deve receber uma mensagem de erro informando o formato de email inválido")]
        //public void ValidaEmailInvalido()
        //{
        //    usuario.Email = "teste1234.com";
        //    var mensagemEsperada = "Formado de email inválido";
        //    var resposta = usuario.ValidaEmail(usuario.Email);
        //    Assert.AreEqual(mensagemEsperada, resposta);
        //}

        //[Test]
        //[Description("Deve receber uma mensagem de erro caso o email informado não esteja disponivel")]
        //public void ValidaEmailIndidponivel()
        //{
        //    usuario.Email = "adminvotacao@alterdata.com.br";
        //    var mensagemEsperada = "O email informado já está em uso";
        //    var resposta = usuario.ValidaEmail(usuario.Email);
        //    Assert.AreEqual(mensagemEsperada, resposta);
        //}

    }
}
