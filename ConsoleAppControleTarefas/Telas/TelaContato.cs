using ConsoleAppControleTarefas.Controladores;
using ConsoleAppControleTarefas.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas.Telas
{
    public class TelaContato : TelaCadastro<Contato>, ICadastravel
    {
        ControladorContato controladorContato = new ControladorContato();
        public TelaContato(ControladorContato controlador) : base("Cadastro de Contatos", controlador)
        {
            this.controladorContato = controlador;
        }

        public override void ApresentarMensagem(List<Contato> contatos)
        {
            string configuracaColunasTabela = "{0,-8} | {1,-15} | {2,-22} | {3,-30} | {4,-15} | {5,-20}";

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Nome", "Email", "Telefone", "Empresa", "Cargo");

            foreach (Contato contato in contatos)
            {
                Console.WriteLine(configuracaColunasTabela, contato.id, contato.nome, contato.email, contato.telefone, contato.empresa, contato.cargo);
            }
        }

        public override Contato ObterObjeto(TipoAcao tipoAcao)
        {
            Console.Write("Digite qual o nome do contato: \n");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o email do contato: ");
            string email = Console.ReadLine();

            Console.WriteLine("Digite o telfone do contato: ");
            int telefone = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite a empresa do contato: ");
            string empresa = Console.ReadLine();

            Console.WriteLine("Digite o cargo do contato: ");
            string cargo = Console.ReadLine();


            return new Contato(nome, email, telefone, empresa, cargo);
        }
    }
}
