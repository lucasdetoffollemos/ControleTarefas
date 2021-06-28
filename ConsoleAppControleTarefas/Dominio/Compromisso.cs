using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas.Dominio
{
    public class Compromisso : EntidadeBase, IValidavel
    {
        public string assunto;
        public string local;
        public DateTime dataCompromisso;
        public string horaInicio;
        public string horaTermino;
        public int idContato;
        public string nomeContato;

        public Compromisso(string assunto, string local, DateTime dataCompromisso, string horaInicio, string horaTermino, int idContato)
        {
            this.assunto = assunto;
            this.local = local;
            this.dataCompromisso = dataCompromisso;
            this.horaInicio = horaInicio;
            this.horaTermino = horaTermino;
            this.idContato = idContato;
        }

        public Compromisso(string assunto, string local, DateTime dataCompromisso, string horaInicio, string horaTermino, string nomeContato)
        {
            this.assunto = assunto;
            this.local = local;
            this.dataCompromisso = dataCompromisso;
            this.horaInicio = horaInicio;
            this.horaTermino = horaTermino;
            this.nomeContato = gerarNomeContato( nomeContato);
        }

        private string gerarNomeContato(string nome)
        {
            if (nome != "")
                return nome;
            else
                return "Contato não está na lista";
                
        }

        public string Validar()
        {
            string resultadoValidacao = "";

            if (this.dataCompromisso.DayOfWeek.ToString() == "Saturday" || this.dataCompromisso.DayOfWeek.ToString() == "Sunday")
                resultadoValidacao += "O compromisso não pode ser agendado em finais de semana\n";

            if (this.horaInicio == this.horaTermino)
                resultadoValidacao += "O horário de início não pode ser o mesmo que o de término\n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
            
        }
    }
}
