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

       
    }
}
