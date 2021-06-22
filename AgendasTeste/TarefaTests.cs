using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ConsoleAppControleTarefas.Dominio;
using ConsoleAppControleTarefas.Controladores;
using System.Collections.Generic;
using ConsoleAppControleTarefas;

namespace AgendasTeste
{
    [TestClass]
    class TarefaTests
    {
        private ControladorTarefa controladorTarefa;
        private Tarefa tarefaTeste;

        public TarefaTests()
        {
            controladorTarefa = new ControladorTarefa();
            tarefaTeste = new Tarefa(2, "Lavar casa", new DateTime(22/06/2021), new DateTime(23/06/2021), 35);
        }

        [TestMethod]
        public void DeveInserirTarefa()
        {
            Assert.AreEqual(controladorTarefa.InserirNovo(tarefaTeste), "ESTA_VALIDO");
        }

        [TestMethod]
        public void DeveAtualizarTarefa()
        {
            Assert.AreEqual(controladorTarefa.EditarRegistro(1, tarefaTeste), "ESTA_VALIDO");
        }

        [TestMethod]
        public void DeveExcluirTarefa()
        {
            Assert.IsTrue(controladorTarefa.ExcluirRegistro(tarefaTeste));
        }

        [TestMethod]
        public void DeveSelecionarTodosTarefas()
        {
            List<Tarefa> tarefas = controladorTarefa.SelecionarTodosRegistros();

            Assert.IsTrue(tarefas.Count > 0);
        }

    }
}
