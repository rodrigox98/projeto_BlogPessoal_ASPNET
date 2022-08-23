using BlogAPI.Src.Contextos;
using BlogAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BlogTeste.Contextos
{
    [TestClass]
        public class UsuarioContextoTeste
        {
                       
        #region atributos
        private BlogPessoalContexto _contexto;

        #endregion 

        #region Métodos

        [TestMethod]
        public void InserirNovoUsuarioRetornaUsuarioInserido()
        {
            //Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_UCT1")
            .Options;

            _contexto = new BlogPessoalContexto(opt);

            // GIVEN - Dado que adiciono um usuario ao sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Jhony Boaz",
                Email = "johnny@email.com",
                Foto = "URLDAFOTO",
                Senha = "123456",
            });
            _contexto.SaveChanges();

            //WHEN - Quando eu pesquiso pelo e-mail 
            var resultado = _contexto.Usuarios.FirstOrDefault(u => u.Email == "johnny@email.com");

            //THEN - Ai depende do teu teste kkkk.
            //Assert.IsNotNull(resultado);
            //Assert.AreEqual("Jhony Boaz", resultado.Nome);
            Assert.AreNotEqual("Jonny Boaz", resultado.Nome);
        }
        #endregion 
    }
}
