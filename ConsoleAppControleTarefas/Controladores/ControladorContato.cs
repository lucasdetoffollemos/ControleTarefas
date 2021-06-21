using ConsoleAppControleTarefas.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas.Controladores
{
    public class ControladorContato : ControladorBase<Contato>
    {

        private const string enderecoDBTarefa =
                 @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefa;Integrated Security=True;Pooling=False";

        public override string InserirNovo(Contato contato)
        {
            string resultado = contato.Validar();

            if (resultado == "ESTA_VALIDO")
            {
                SqlConnection conexaoComBanco = AbrindoConexaoDB();

                SqlCommand comandoInsercao = new SqlCommand();
                comandoInsercao.Connection = conexaoComBanco;

                string sqlInsercao =
                    @"INSERT INTO TBCONTATO
                    (
                        [NOME],
                        [EMAIL],
                        [TELEFONE],
                        [EMPRESA],
                        [CARGO]
                    ) 
                    VALUES
                    (
                        @NOME,
                        @EMAIL,
                        @TELEFONE,
                        @EMPRESA,
                        @CARGO
                    );";

                sqlInsercao +=
                    @"SELECT SCOPE_IDENTITY();";

                comandoInsercao.CommandText = sqlInsercao;

                comandoInsercao.Parameters.AddWithValue("NOME", contato.nome);
                comandoInsercao.Parameters.AddWithValue("EMAIL", contato.email);
                comandoInsercao.Parameters.AddWithValue("TELEFONE", contato.telefone);
                comandoInsercao.Parameters.AddWithValue("EMPRESA", contato.empresa);
                comandoInsercao.Parameters.AddWithValue("CARGO", contato.cargo);

                object id = comandoInsercao.ExecuteScalar();

                contato.id = Convert.ToInt32(id);

                conexaoComBanco.Close();
            }

            return resultado;
        }

        public override List<Contato> SelecionarTodosRegistros()
        {
            SqlConnection conexaoComBanco = AbrindoConexaoDB();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID], 
                        [NOME],
                        [EMAIL],
                        [TELEFONE],
                        [EMPRESA],
                        [CARGO]
                    FROM 
                        TBCONTATO 
                    ORDER BY [CARGO]";


            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorContatos = comandoSelecao.ExecuteReader();

            List<Contato> contatos = new List<Contato>();

            while (leitorContatos.Read())
            {
                int id = Convert.ToInt32(leitorContatos["ID"]);
                string nome = Convert.ToString(leitorContatos["NOME"]);
                string email = Convert.ToString(leitorContatos["EMAIL"]);
                int telefone = Convert.ToInt32(leitorContatos["TELEFONE"]);
                string empresa = Convert.ToString(leitorContatos["EMPRESA"]);
                string cargo = Convert.ToString(leitorContatos["CARGO"]);

                Contato c = new Contato(nome, email, telefone, empresa, cargo);
                c.id = id;

                contatos.Add(c);
            }

            conexaoComBanco.Close();

            return contatos;
        }

        public override string EditarRegistro(int idEncontrado, Contato contato)
        {
            string resultado = contato.Validar();
            //string resultado = "ESTA_VALIDO";

            if(resultado == "ESTA_VALIDO")
            {
                SqlConnection conexaoComBanco = AbrindoConexaoDB();

                SqlCommand comandoAtualizacao = new SqlCommand();
                comandoAtualizacao.Connection = conexaoComBanco;

                string sqlAtualizacao =
                    @"UPDATE TBCONTATO 
	                SET	
		                [NOME] = @NOME,
                        [EMAIL] = @EMAIL,
                        [TELEFONE] = @TELEFONE,
                        [EMPRESA] = @EMPRESA,
                        [CARGO] = @CARGO
	                WHERE 
		                [ID] = @ID";

                comandoAtualizacao.CommandText = sqlAtualizacao;

                comandoAtualizacao.Parameters.AddWithValue("ID", idEncontrado);
                comandoAtualizacao.Parameters.AddWithValue("NOME", contato.nome);
                comandoAtualizacao.Parameters.AddWithValue("EMAIL", contato.email);
                comandoAtualizacao.Parameters.AddWithValue("TELEFONE", contato.telefone);
                comandoAtualizacao.Parameters.AddWithValue("EMPRESA", contato.empresa);
                comandoAtualizacao.Parameters.AddWithValue("CARGO", contato.cargo);

                comandoAtualizacao.ExecuteNonQuery();

                conexaoComBanco.Close();
            }
            return resultado;
        }

        public override bool ExcluirRegistro(Contato contato)
        {
            SqlConnection conexaoComBanco = AbrindoConexaoDB();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                @"DELETE FROM TBCONTATO                 
	                WHERE 
		                [ID] = @ID";

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", contato.id);

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return true;
        }

        public override Contato SelecionarRegistroPorId(int idPesquisado)
        {
            SqlConnection conexaoComBanco = AbrindoConexaoDB();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID], 
                        [NOME],
                        [EMAIL],
                        [TELEFONE],
                        [EMPRESA],
                        [CARGO]
                    FROM 
                        TBCONTATO
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", idPesquisado);

            SqlDataReader leitorContatos = comandoSelecao.ExecuteReader();

            if (leitorContatos.Read() == false)
                return null;

            int id = Convert.ToInt32(leitorContatos["ID"]);
            string nome = Convert.ToString(leitorContatos["NOME"]);
            string email = Convert.ToString(leitorContatos["EMAIL"]);
            int telefone = Convert.ToInt32(leitorContatos["TELEFONE"]);
            string empresa = Convert.ToString(leitorContatos["EMPRESA"]);
            string cargo = Convert.ToString(leitorContatos["CARGO"]);

            Contato c = new Contato(nome, email, telefone, empresa, cargo);
            c.id = id;

            conexaoComBanco.Close();

            return c;
        }

       
    }
}
