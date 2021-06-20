using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas
{
    public class ControladorTarefa : ControladorBase<Tarefa>
    {
        private const string enderecoDBTarefa =
                 @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefa;Integrated Security=True;Pooling=False";

        public override string InserirNovo(Tarefa tarefa)
        {

            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefa;
            conexaoComBanco.Open();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            string sqlInsercao =
                @"INSERT INTO TBTAREFA
                    (
                        [PRIORIDADE],
                        [TITULO],
                        [DATACRIACAO],
                        [DATACONCLUSAO],
                        [PERCENTUAL]
                    ) 
                    VALUES
                    (
                        @PRIORIDADE,
                        @TITULO,
                        @DATACRIACAO,
                        @DATACONCLUSAO,
                        @PERCENTUAL
                    );";

            sqlInsercao +=
                @"SELECT SCOPE_IDENTITY();";

            comandoInsercao.CommandText = sqlInsercao;

            comandoInsercao.Parameters.AddWithValue("PRIORIDADE", tarefa.prioridade);
            comandoInsercao.Parameters.AddWithValue("TITULO", tarefa.titulo);
            comandoInsercao.Parameters.AddWithValue("DATACRIACAO", tarefa.dataCriacao);
            comandoInsercao.Parameters.AddWithValue("DATACONCLUSAO", tarefa.dataConclusao);
            comandoInsercao.Parameters.AddWithValue("PERCENTUAL", tarefa.percentual);

            object id = comandoInsercao.ExecuteScalar();

            tarefa.id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return "ESTA_VALIDO";
        }

        public override List<Tarefa> SelecionarTodosRegistros()
        {


            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefa;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID], 
                        [PRIORIDADE],
                        [TITULO],
                        [DATACRIACAO],
                        [DATACONCLUSAO],
                        [PERCENTUAL]
                    FROM 
                        TBTAREFA
                    ORDER BY 
                       CASE [PRIORIDADE]
                       WHEN 'Alta' THEN 1
                       WHEN 'Normal' THEN 2
                       WHEN 'Baixa' THEN 3
                    END";


            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorTarefas.Read())
            {
                int id = Convert.ToInt32(leitorTarefas["ID"]);
                string prioridade = Convert.ToString(leitorTarefas["PRIORIDADE"]);
                string titulo = Convert.ToString(leitorTarefas["TITULO"]);
                DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
                DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
                decimal percentual = Convert.ToDecimal(leitorTarefas["PERCENTUAL"]);

                Tarefa t = new Tarefa(prioridade, titulo, dataCriacao, dataConclusao, percentual);
                t.id = id;

                tarefas.Add(t);
            }

            conexaoComBanco.Close();

            return tarefas;
        }

        public override string EditarRegistro(int idEncontrado, Tarefa tarefa)
        {
            //string resultado = item.Validar();
            string resultado = "ESTA_VALIDO";

            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefa;
            conexaoComBanco.Open();

            SqlCommand comandoAtualizacao = new SqlCommand();
            comandoAtualizacao.Connection = conexaoComBanco;

            string sqlAtualizacao =
                @"UPDATE TBTAREFA 
	                SET	
		                [PRIORIDADE] = @PRIORIDADE,
                        [TITULO] = @TITULO,
                        [DATACONCLUSAO] = @DATACONCLUSAO,
                        [PERCENTUAL] = @PERCENTUAL
	                WHERE 
		                [ID] = @ID";

            comandoAtualizacao.CommandText = sqlAtualizacao;

            comandoAtualizacao.Parameters.AddWithValue("ID", idEncontrado);
            comandoAtualizacao.Parameters.AddWithValue("PRIORIDADE", tarefa.prioridade);
            comandoAtualizacao.Parameters.AddWithValue("TITULO", tarefa.titulo);
            comandoAtualizacao.Parameters.AddWithValue("DATACONCLUSAO", tarefa.dataConclusao);
            comandoAtualizacao.Parameters.AddWithValue("PERCENTUAL", tarefa.percentual);

            comandoAtualizacao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return resultado;
        }

        public override bool ExcluirRegistro(Tarefa tarefa)
        {
          
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefa;
            conexaoComBanco.Open();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                @"DELETE FROM TBTAREFA	                
	                WHERE 
		                [ID] = @ID";

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", tarefa.id);

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return true;
        }

        public override Tarefa SelecionarRegistroPorId(int idPesquisado)
        {

            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefa;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID], 
                        [PRIORIDADE],
                        [TITULO],
                        [DATACRIACAO],
                        [DATACONCLUSAO],
                        [PERCENTUAL]
                    FROM 
                        TBTAREFA
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", idPesquisado);

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            if (leitorTarefas.Read() == false)
                return null;

            int id = Convert.ToInt32(leitorTarefas["ID"]);
            string prioridade = Convert.ToString(leitorTarefas["PRIORIDADE"]);
            string titulo = Convert.ToString(leitorTarefas["TITULO"]);
            DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
            DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
            decimal percentual = Convert.ToDecimal(leitorTarefas["PERCENTUAL"]);

            Tarefa t = new Tarefa(prioridade, titulo, dataCriacao, dataConclusao, percentual);
            t.id = id;

            conexaoComBanco.Close();

            return t;
        }

        public  List<Tarefa> SelecionarTarefasFechadas()
        {
            

            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefa;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID], 
                        [PRIORIDADE],
                        [TITULO],
                        [DATACRIACAO],
                        [DATACONCLUSAO],
                        [PERCENTUAL]
                    FROM 
                        TBTAREFA
                    WHERE
                        [PERCENTUAL] = 100
                    ORDER BY 
                       CASE [PRIORIDADE]
                       WHEN 'Alta' THEN 1
                       WHEN 'Normal' THEN 2
                       WHEN 'Baixa' THEN 3
                    END";


            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorTarefas.Read())
            {
                int id = Convert.ToInt32(leitorTarefas["ID"]);
                string prioridade = Convert.ToString(leitorTarefas["PRIORIDADE"]);
                string titulo = Convert.ToString(leitorTarefas["TITULO"]);
                DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
                DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
                decimal percentual = Convert.ToDecimal(leitorTarefas["PERCENTUAL"]);

                Tarefa t = new Tarefa(prioridade, titulo, dataCriacao, dataConclusao, percentual);
                t.id = id;

                tarefas.Add(t);
            }

            conexaoComBanco.Close();

            return tarefas;
        }

        public List<Tarefa> SelecionarTarefasAbertas()
        {
           

            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefa;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID], 
                        [PRIORIDADE],
                        [TITULO],
                        [DATACRIACAO],
                        [DATACONCLUSAO],
                        [PERCENTUAL]
                    FROM 
                        TBTAREFA
                    WHERE
                        [PERCENTUAL] < 100
                    ORDER BY 
                       CASE [PRIORIDADE]
                       WHEN 'Alta' THEN 1
                       WHEN 'Normal' THEN 2
                       WHEN 'Baixa' THEN 3
                    END";


            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorTarefas.Read())
            {
                int id = Convert.ToInt32(leitorTarefas["ID"]);
                string prioridade = Convert.ToString(leitorTarefas["PRIORIDADE"]);
                string titulo = Convert.ToString(leitorTarefas["TITULO"]);
                DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
                DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
                decimal percentual = Convert.ToDecimal(leitorTarefas["PERCENTUAL"]);

                Tarefa t = new Tarefa(prioridade, titulo, dataCriacao, dataConclusao, percentual);
                t.id = id;

                tarefas.Add(t);
            }

            conexaoComBanco.Close();

            return tarefas;
        }

    }
}
