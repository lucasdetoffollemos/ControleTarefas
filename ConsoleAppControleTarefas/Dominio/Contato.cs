using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas.Dominio
{
    public class Contato : EntidadeBase, IValidavel
    {
        public string nome;
        public string email;
        public int telefone;
        public string empresa;
        public string cargo;

        public Contato(string nome, string email, int telefone, string empresa, string cargo) {
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.empresa = empresa;
            this.cargo = cargo;
        }

        public string Validar() {

            string resultadoValidacao = "";

            string telefoneStr = Convert.ToString(telefone);

            if (telefoneStr.Length != 9)
            {
                resultadoValidacao = "Número inválido, tente novamente \n";
            }

            if (!email.Contains("@") && !email.Contains(".com"))
            {
                resultadoValidacao += "Email inválido, tente novamente";
            }

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }

    }
}
