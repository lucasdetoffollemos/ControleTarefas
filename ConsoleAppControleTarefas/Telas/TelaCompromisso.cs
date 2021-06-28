using ConsoleAppControleTarefas.Controladores;
using ConsoleAppControleTarefas.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas.Telas
{
    public class TelaCompromisso : TelaCadastro<Compromisso>, ICadastravel
    {

        ControladorCompromisso controladorCompromisso = new ControladorCompromisso();

        private readonly ControladorContato controladorContato;
        private readonly TelaContato telaContato;
        public TelaCompromisso(ControladorCompromisso controlador) : base("Cadastro de Compromissos", controlador)
        {

            this.controladorCompromisso = controlador;
            controladorContato = new ControladorContato();
            telaContato = new TelaContato(controladorContato);

        }

        public override void ApresentarMensagem(List<Compromisso> compromissos)
        {
            string configuracaColunasTabela = "{0,-5} | {1,-18} | {2,-15} | {3,-10} | {4,-12} | {5,-12} | {6, -5}";

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Assunto", "Local/Link", "Data", "Hora do inicio", "Hora do fim", "Contatos");

            foreach (Compromisso c in compromissos)
            {
                Console.WriteLine(configuracaColunasTabela, c.id, c.assunto, c.local, c.dataCompromisso.ToString("dd/MM/yyyy"), c.horaInicio, c.horaTermino, c.nomeContato);
            }
        }

        public override Compromisso ObterObjeto(TipoAcao tipoAcao)
        {
            Console.WriteLine("Digite 1  se o compromisso for presencial, digite 2 se o compromisso for remoto: ");
            int opcaoLocal = Convert.ToInt32(Console.ReadLine());

            telaContato.VisualizarRegistros(TipoVisualizacao.VisualizandoTela);
           

            Console.WriteLine("Se este compromisso estiver relacionado a um contato da sua lista, digite o id do respectivo contato, se não digite 0: ");
            int idContato = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            Console.Write("Digite qual o assunto do compromisso: \n");
            string assunto = Console.ReadLine();

            string local = null;

            if(opcaoLocal == 1)
            {
                Console.WriteLine("Digite o local do compromisso: ");
                local = Console.ReadLine();
            }
            else if(opcaoLocal == 2)
            {
                Console.WriteLine("Digite o link do compromisso: ");
                local = Console.ReadLine();
            }
            
            Console.WriteLine("Digite a data do compromisso: ");
            DateTime dataCompromisso = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Digite a hora de inicio do compromisso: ");
            DateTime horaInicioDt = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Digite a hora de termino do compromisso: ");
            DateTime horaTerminoDt = Convert.ToDateTime(Console.ReadLine());

            string horaInicio = horaInicioDt.ToString("HH:mm");
            string horaTermino = horaTerminoDt.ToString("HH:mm");

            return new Compromisso(assunto, local, dataCompromisso, horaInicio, horaTermino, idContato);
        }

        public bool VisualizarCompromissosPassados(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("Visualizando compromissos passados...");

            List<Compromisso> compromissos = controladorCompromisso.SelecionarTodosCompromissosPassados();

            if (compromissos.Count == 0)
            {
                ApresentarMensagem("Nenhum compromisso cadastrado!", TipoMensagem.Atencao);
                return false;
            }

            ApresentarMensagem(compromissos);

            return true;
        }

        public bool VisualizarCompromissosFuturos(TipoVisualizacao tipo)
        {

            Console.WriteLine("Digite até que data você deseja ver os compromissos futuros: ");
            DateTime dataValidaFutura = Convert.ToDateTime(Console.ReadLine());
            Console.Clear();
            if(dataValidaFutura > DateTime.Now)
            {
                if (tipo == TipoVisualizacao.VisualizandoTela)
                    ConfigurarTela("Visualizando compromissos futuros...");

                List<Compromisso> compromissos = controladorCompromisso.SelecionarTodosCompromissosFuturos(dataValidaFutura);

                if (compromissos.Count == 0)
                {
                    ApresentarMensagem("Nenhum compromisso cadastrado!", TipoMensagem.Atencao);
                    return false;
                }

                ApresentarMensagem(compromissos);
            }
            else
            {
                ApresentarMensagem("A data tem que estar no futuro!", TipoMensagem.Atencao);
                return false;
            }
           

            return true;
        }

        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir um compromisso");
            Console.WriteLine("Digite 2 para visualizar todos os compromissos");
            Console.WriteLine("Digite 3 para editar um compromisso");
            Console.WriteLine("Digite 4 para excluir uma tarefa");
            Console.WriteLine("Digite 5 para visualizar compromissos passados.");
            Console.WriteLine("Digite 6 para visualizar compromissos futuros.");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }
    }
}
