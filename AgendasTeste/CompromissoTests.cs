using ConsoleAppControleTarefas.Controladores;
using ConsoleAppControleTarefas.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendasTeste
{
    [TestClass]
    class CompromissoTests
    {
        private ControladorCompromisso controladorCompromisso;
        private Compromisso compromissoTeste;
       

        public CompromissoTests()
        {
            controladorCompromisso = new ControladorCompromisso();
            compromissoTeste = new Compromisso("Fazer Polenta", "Lages", new DateTime(22 / 03 / 2009), "12:30", "17:30", 2);
        }

        [TestMethod]
        public void DeveInserirCompromissos()
        {
            Assert.AreEqual(controladorCompromisso.InserirNovo(compromissoTeste), "ESTA_VALIDO");
        }

        [TestMethod]
        public void DeveAtualizarCompromissos()
        {
            Assert.AreEqual(controladorCompromisso.EditarRegistro(1, compromissoTeste), "ESTA_VALIDO");
        }

        [TestMethod]
        public void DeveExcluirCompromissos()
        {
            Assert.IsTrue(controladorCompromisso.ExcluirRegistro(compromissoTeste));
        }

        [TestMethod]
        public void DeveSelecionarTodosCompromissos()
        {
            List<Compromisso> compromissos = controladorCompromisso.SelecionarTodosRegistros();

            Assert.IsTrue(compromissos.Count > 0);
        }


    }
}
