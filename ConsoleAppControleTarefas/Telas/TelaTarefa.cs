using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas
{
    public class TelaTarefa : TelaCadastro<Tarefa>, ICadastravel 
    { 
        ControladorTarefa controladorTarefa = new ControladorTarefa();

        public TelaTarefa(ControladorTarefa controlador) : base("Cadastro de Tarefas", controlador)
        {
            this.controladorTarefa = controlador;
        }

        public bool MostrarTarefasConcluidas() {
            
            ConfigurarTela("Visualizando tarefas fechadas...");

            List<Tarefa> tarefas = controladorTarefa.SelecionarTarefasFechadas();

            if (tarefas.Count == 0)
            {
                ApresentarMensagem("Nenhuma registro cadastrado!", TipoMensagem.Atencao);
                return false;
            }

            ApresentarMensagem(tarefas);

            return true;

        }

        public bool MostrarTarefasAbertas()
        {

            ConfigurarTela("Visualizando tarefas Abertas...");

            List<Tarefa> tarefas = controladorTarefa.SelecionarTarefasAbertas();

            if (tarefas.Count == 0)
            {
                ApresentarMensagem("Nenhuma tarefa cadastrada!", TipoMensagem.Atencao);
                return false;
            }

            ApresentarMensagem(tarefas);

            return true;

        }

        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir uma nova tarefa");
            Console.WriteLine("Digite 2 para visualizar tarefas");
            Console.WriteLine("Digite 3 para editar uma tarefa");
            Console.WriteLine("Digite 4 para excluir uma tarefa");
            Console.WriteLine("Digite 5 para visualizar tarefas que ja foram concluidas.");
            Console.WriteLine("Digite 6 para visualizar tarefas que ainda não foram concluidas.");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public override void ApresentarMensagem(List<Tarefa> tarefas)
        {

            string configuracaColunasTabela = "{0,-10} | {1,-20} | {2,-18} | {3,-20} | {4,-20} | {5,-5}";

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Prioridade", "Titulo", "Data Criação", "Data Conclusão", "Percentual");

            foreach (Tarefa tarefa in tarefas)
            {
                Console.WriteLine(configuracaColunasTabela, tarefa.id, tarefa.prioridade, tarefa.titulo, tarefa.dataCriacao.ToString("dd/MM/yy"), tarefa.dataConclusao.ToString("dd/MM/yy"), tarefa.percentual);
            }

        }

        public override Tarefa ObterObjeto(TipoAcao tipoAcao)
        {
            


            Console.Write("Digite qual o nível de prioridade da tarefa: \n");
            Console.Write("1 - Baixa\n");
            Console.Write("2 - Normal\n");
            Console.Write("3 - Alta\n");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            DateTime dataCriacao = new DateTime();
            if (tipoAcao == TipoAcao.Inserindo) {
                Console.WriteLine("Digite a data de criação da tarefa: ");
                 dataCriacao = Convert.ToDateTime(Console.ReadLine());
            }
            

            Console.WriteLine("Digite a data de conclusão da tarefa: ");
            DateTime dataConclusao = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Digite qual quantos % de está feita sua  tarefa: ");
            decimal percentual = Convert.ToDecimal(Console.ReadLine());


            return new Tarefa(prioridade, titulo, dataCriacao, dataConclusao, percentual);
        }

    }
}
