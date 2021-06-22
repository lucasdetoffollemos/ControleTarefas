using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas
{
    public abstract class ControladorBase<T> where T : EntidadeBase
    {

        protected List<T> registros = new List<T>();

        public SqlConnection AbrindoConexaoDB() {
            string enderecoDBTarefa =
                @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefa;Integrated Security=True;Pooling=False";

            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefa;
            conexaoComBanco.Open();
            return conexaoComBanco;0
        }

        public abstract string InserirNovo(T item);

        public abstract string EditarRegistro(int id, T item);

        public abstract bool ExcluirRegistro(T item);

        public abstract List<T> SelecionarTodosRegistros();

        public abstract T SelecionarRegistroPorId(int id);

    }

}
