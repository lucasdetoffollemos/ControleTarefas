using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas
{
    public abstract class ControladorBase<T> where T : EntidadeBase
    {

        protected List<T> registros = new List<T>();

       
        public abstract string InserirNovo(T item);

        public abstract string EditarRegistro(int id, T item);

        public abstract bool ExcluirRegistro(T item);

        public abstract List<T> SelecionarTodosRegistros();

        public abstract T SelecionarRegistroPorId(int id);

    }

}
