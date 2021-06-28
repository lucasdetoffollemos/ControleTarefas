using ConsoleAppControleTarefas.Controladores;
using ConsoleAppControleTarefas.Telas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas
{
    public class TelaPrincipal : TelaBase
    {
        private readonly ControladorTarefa controladorTarefa;
        private readonly ControladorContato controladorContato;
        private readonly ControladorCompromisso controladorCompromisso;

        private readonly TelaTarefa telaTarefa;
        private readonly TelaContato telaContato;
        private readonly TelaCompromisso telaCompromisso;
        
        public TelaPrincipal() : base("Tela Principal")
        {
            controladorTarefa = new ControladorTarefa();
            controladorContato = new ControladorContato();
            controladorCompromisso = new ControladorCompromisso();

            telaTarefa = new TelaTarefa(controladorTarefa);
            telaContato = new TelaContato(controladorContato);
            telaCompromisso = new TelaCompromisso(controladorCompromisso);


        }

        public TelaBase ObterTela()
        {
            ConfigurarTela("Escolha uma opção: ");

            TelaBase telaSelecionada = null;
            string opcao;
            do
            {
                Console.WriteLine("Digite 1 para o Cadastro de Tarefa");
                Console.WriteLine("Digite 2 para o Cadastro de Contato");
                Console.WriteLine("Digite 3 para o Cadastro de Compromisso");

                Console.WriteLine("Digite S para Sair");
                Console.WriteLine();
                Console.Write("Opção: ");
                opcao = Console.ReadLine();

                if (opcao == "1")
                    telaSelecionada = telaTarefa;

                else if (opcao == "2")
                    telaSelecionada = telaContato;

                else if (opcao == "3")
                    telaSelecionada = telaCompromisso;


                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    telaSelecionada = null;

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "5" && opcao != "6"  && opcao != "S" && opcao != "s")
            {
                ApresentarMensagem("Opção inválida", TipoMensagem.Erro);
                return true;
            }
            else
                return false;
        }
    }
}

