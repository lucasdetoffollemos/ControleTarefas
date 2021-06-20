using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas
{
    public class Tarefa : EntidadeBase
    {
        public string prioridade;
        public string titulo;
        public DateTime dataCriacao;
        public DateTime dataConclusao;
        public decimal percentual;

        public Tarefa(int prioridadeChave, string titulo, DateTime dataCriacao, DateTime dataConclusao, decimal percentual)
        {
            prioridade = gerarPrioridade(prioridadeChave);
            this.titulo = titulo;
            this.dataCriacao = dataCriacao;
            this.dataConclusao = dataConclusao;
            this.percentual = percentual;
        }

        public Tarefa(string prioridade, string titulo, DateTime dataCriacao, DateTime dataConclusao, decimal percentual)
        {
            this.prioridade = prioridade;
            this.titulo = titulo;
            this.dataCriacao = dataCriacao;
            this.dataConclusao = dataConclusao;
            this.percentual = percentual;
        }

        private string gerarPrioridade(int prioridadeChave)
        {
            Dictionary<int, string> prioridades = new Dictionary<int, string>();
            prioridades.Add(1, "Baixa");
            prioridades.Add(2, "Normal");
            prioridades.Add(3, "Alta");

            for(int i= 0; i< prioridades.Count; i++)
            {
                if (prioridades.ContainsKey(prioridadeChave))
                {
                    return prioridades[prioridadeChave];
                }
            }

            return "ERRADO";

        }
    }
}
