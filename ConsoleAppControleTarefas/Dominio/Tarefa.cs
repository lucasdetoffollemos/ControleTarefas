using ConsoleAppControleTarefas.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas
{
    public class Tarefa : EntidadeBase, IValidavel
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


        public string Validar()
        {
            string resultadoValidacao = "";

            if (percentual > 100)
            {
                resultadoValidacao = "Percentual não pode ser maior que cem, tente novamente";
            }

            if (percentual < 0)
            {
                resultadoValidacao = "Percentual não pode ser menor que 0, tente novamente";
            }

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }
    }
}
