using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ConsoleAppControleTarefas.Dominio;
using ConsoleAppControleTarefas.Controladores;
using System.Collections.Generic;

namespace AgendasTeste
{
    [TestClass]
    public class ContatoTests
    {
        private ControladorContato controladorContato;
        private Contato contatoTeste;

        public ContatoTests()
        {
            controladorContato = new ControladorContato();
            contatoTeste = new Contato("Lucas", "lucas@lucas.com", 123456789, "ndd", "dev");
        }

        [TestMethod]
        public void DeveInserirContato()
        {
            Assert.AreEqual(controladorContato.InserirNovo(contatoTeste), "ESTA_VALIDO");
        }

        [TestMethod]
        public void DeveAtualizarContato()
        {
            Assert.AreEqual(controladorContato.EditarRegistro(1, contatoTeste), "ESTA_VALIDO");
        }

        [TestMethod]
        public void DeveExcluirContato()
        {
            Assert.IsTrue(controladorContato.ExcluirRegistro(contatoTeste));
        }

        [TestMethod]
        public void DeveSelecionarTodosContatos()
        {
            List<Contato> contatos = controladorContato.SelecionarTodosRegistros();

            Assert.IsTrue(contatos.Count > 0);
        }




    }
}
