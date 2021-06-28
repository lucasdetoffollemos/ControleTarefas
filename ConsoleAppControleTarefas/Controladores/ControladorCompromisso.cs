using ConsoleAppControleTarefas.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas.Controladores
{
    public class ControladorCompromisso : ControladorBase<Compromisso>
    {
        public override string InserirNovo(Compromisso compromisso)
        {
            string resultado = compromisso.Validar();

            if (resultado == "ESTA_VALIDO")
            {

                SqlConnection conexaoComBanco = AbrindoConexaoDB();

                SqlCommand comandoInsercao = new SqlCommand();
                comandoInsercao.Connection = conexaoComBanco;

                string sqlInsercao =
                    @"INSERT INTO TBCOMPROMISSO
                    (
                        [ASSUNTO],
                        [LOCAL],
                        [DATACOMPROMISSO],
                        [HORAINICIO],
                        [HORATERMINO],
                        [ID_CONTATO]
                    ) 
                    VALUES
                    (
                        @ASSUNTO,
                        @LOCAL,
                        @DATACOMPROMISSO,
                        @HORAINICIO,
                        @HORATERMINO,
                        @ID_CONTATO
                        
                    );";

                sqlInsercao +=
                    @"SELECT SCOPE_IDENTITY();";

                comandoInsercao.CommandText = sqlInsercao;

                comandoInsercao.Parameters.AddWithValue("ASSUNTO", compromisso.assunto);
                comandoInsercao.Parameters.AddWithValue("LOCAL", compromisso.local);
                comandoInsercao.Parameters.AddWithValue("DATACOMPROMISSO", compromisso.dataCompromisso);
                comandoInsercao.Parameters.AddWithValue("HORAINICIO", compromisso.horaInicio);
                comandoInsercao.Parameters.AddWithValue("HORATERMINO", compromisso.horaTermino);
                comandoInsercao.Parameters.AddWithValue("ID_CONTATO", compromisso.idContato);

                object id = comandoInsercao.ExecuteScalar();

                compromisso.id = Convert.ToInt32(id);

                conexaoComBanco.Close();
            }

            return resultado;
        }

        public override List<Compromisso> SelecionarTodosRegistros()
        {
            SqlConnection conexaoComBanco = AbrindoConexaoDB();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        COM.ID,
                        COM.ASSUNTO,
                        COM.LOCAL,
                        COM.DATACOMPROMISSO,
                        COM.HORAINICIO,
                        COM.HORATERMINO,
                        COM.ID_CONTATO,
                        C.NOME
                    FROM 
                        TBCOMPROMISSO COM LEFT JOIN 
                        TBCONTATO C
                    ON 
                        COM.ID_CONTATO = C.ID";


            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorCompromissos = comandoSelecao.ExecuteReader();

            List<Compromisso> compromissos = new List<Compromisso>();

            while (leitorCompromissos.Read())
            {
                int id = Convert.ToInt32(leitorCompromissos["ID"]);
                string assunto = Convert.ToString(leitorCompromissos["ASSUNTO"]);
                string local = Convert.ToString(leitorCompromissos["LOCAL"]);
                DateTime dataCompromisso = Convert.ToDateTime(leitorCompromissos["DATACOMPROMISSO"]);
                string horaInicio = Convert.ToString(leitorCompromissos["HORAINICIO"]);
                string horaTermino = Convert.ToString(leitorCompromissos["HORATERMINO"]);
                string nomeContato = Convert.ToString(leitorCompromissos["NOME"]);

                Compromisso c = new Compromisso(assunto, local, dataCompromisso, horaInicio, horaTermino, nomeContato);
                c.id = id;

                compromissos.Add(c);
            }

            conexaoComBanco.Close();

            return compromissos;
        }

        public override string EditarRegistro(int idEncontrado, Compromisso compromisso)
        {
            string resultado = compromisso.Validar();

            if (resultado == "ESTA_VALIDO")
            {
                SqlConnection conexaoComBanco = AbrindoConexaoDB();

                SqlCommand comandoAtualizacao = new SqlCommand();
                comandoAtualizacao.Connection = conexaoComBanco;

                string sqlAtualizacao =
                    @"UPDATE TBCOMPROMISSO 
	                SET	
		                [ASSUNTO] = @ASSUNTO,
                        [LOCAL] = @LOCAL,
                        [DATACOMPROMISSO] = @DATACOMPROMISSO,
                        [HORAINICIO] = @HORAINICIO,
                        [HORATERMINO] = @HORATERMINO,
                        [ID_CONTATO] = @ID_CONTATO
	                WHERE 
		                [ID] = @ID";

                comandoAtualizacao.CommandText = sqlAtualizacao;

                comandoAtualizacao.Parameters.AddWithValue("ID", idEncontrado);
                comandoAtualizacao.Parameters.AddWithValue("ASSUNTO", compromisso.assunto);
                comandoAtualizacao.Parameters.AddWithValue("LOCAL", compromisso.local);
                comandoAtualizacao.Parameters.AddWithValue("DATACOMPROMISSO", compromisso.dataCompromisso);
                comandoAtualizacao.Parameters.AddWithValue("HORAINICIO", compromisso.horaInicio);
                comandoAtualizacao.Parameters.AddWithValue("HORATERMINO", compromisso.horaTermino);
                comandoAtualizacao.Parameters.AddWithValue("ID_CONTATO", compromisso.idContato);

                comandoAtualizacao.ExecuteNonQuery();

                conexaoComBanco.Close();
            }

            return resultado;
        }

        public override bool ExcluirRegistro(Compromisso compromisso)
        {
            SqlConnection conexaoComBanco = AbrindoConexaoDB();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                @"DELETE FROM TBCOMPROMISSO                 
	                WHERE 
		                [ID] = @ID";

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", compromisso.id);

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return true;

        }

        public override Compromisso SelecionarRegistroPorId(int idPesquisado)
        {

            SqlConnection conexaoComBanco = AbrindoConexaoDB();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID], 
                        [ASSUNTO],
                        [LOCAL],
                        [DATACOMPROMISSO],
                        [HORAINICIO],
                        [HORATERMINO],
                        [ID_CONTATO]
                    FROM 
                        TBCOMPROMISSO
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", idPesquisado);

            SqlDataReader leitorCompromissos = comandoSelecao.ExecuteReader();

            if  (leitorCompromissos.Read() == false)
                return null;

            int id = Convert.ToInt32(leitorCompromissos["ID"]);
            string assunto = Convert.ToString(leitorCompromissos["ASSUNTO"]);
            string local = Convert.ToString(leitorCompromissos["LOCAL"]);
            DateTime dataCompromisso = Convert.ToDateTime(leitorCompromissos["DATACOMPROMISSO"]);
            string horaInicio = Convert.ToString(leitorCompromissos["HORAINICIO"]);
            string horaTermino = Convert.ToString(leitorCompromissos["HORATERMINO"]);
            int  idContato = Convert.ToInt32(leitorCompromissos["ID_CONTATO"]);

            Compromisso c = new Compromisso(assunto, local, dataCompromisso, horaInicio, horaTermino, idContato);
            c.id = id;

            conexaoComBanco.Close();

            return c;

        }

        public List<Compromisso> SelecionarTodosCompromissosPassados()
        {
            SqlConnection conexaoComBanco = AbrindoConexaoDB();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        COM.ID,
                        COM.ASSUNTO,
                        COM.LOCAL,
                        COM.DATACOMPROMISSO,
                        COM.HORAINICIO,
                        COM.HORATERMINO,
                        COM.ID_CONTATO,
                        C.NOME
                    FROM 
                        TBCOMPROMISSO COM LEFT JOIN 
                        TBCONTATO C
                    ON 
                        COM.ID_CONTATO = C.ID
                    WHERE 
                        COM.DATACOMPROMISSO < GETUTCDATE()";


            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorCompromissos = comandoSelecao.ExecuteReader();

            List<Compromisso> compromissos = new List<Compromisso>();

            while (leitorCompromissos.Read())
            {
                int id = Convert.ToInt32(leitorCompromissos["ID"]);
                string assunto = Convert.ToString(leitorCompromissos["ASSUNTO"]);
                string local = Convert.ToString(leitorCompromissos["LOCAL"]);
                DateTime dataCompromisso = Convert.ToDateTime(leitorCompromissos["DATACOMPROMISSO"]);
                string horaInicio = Convert.ToString(leitorCompromissos["HORAINICIO"]);
                string horaTermino = Convert.ToString(leitorCompromissos["HORATERMINO"]);
                string nomeContato = Convert.ToString(leitorCompromissos["NOME"]);

                Compromisso c = new Compromisso(assunto, local, dataCompromisso, horaInicio, horaTermino, nomeContato);
                c.id = id;

                compromissos.Add(c);
            }

            conexaoComBanco.Close();

            return compromissos;
        }

        public List<Compromisso> SelecionarTodosCompromissosFuturos(DateTime dataValida)
        {
            SqlConnection conexaoComBanco = AbrindoConexaoDB();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        COM.ID,
                        COM.ASSUNTO,
                        COM.LOCAL,
                        COM.DATACOMPROMISSO,
                        COM.HORAINICIO,
                        COM.HORATERMINO,
                        COM.ID_CONTATO,
                        C.NOME
                    FROM 
                        TBCOMPROMISSO COM LEFT JOIN 
                        TBCONTATO C
                    ON 
                        COM.ID_CONTATO = C.ID
                    WHERE 
                        COM.DATACOMPROMISSO

                    BETWEEN GETUTCDATE() AND @DATAVALIDA";


            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("DATAVALIDA", dataValida);

            SqlDataReader leitorCompromissos = comandoSelecao.ExecuteReader();

            List<Compromisso> compromissos = new List<Compromisso>();

            while (leitorCompromissos.Read())
            {
                int id = Convert.ToInt32(leitorCompromissos["ID"]);
                string assunto = Convert.ToString(leitorCompromissos["ASSUNTO"]);
                string local = Convert.ToString(leitorCompromissos["LOCAL"]);
                DateTime dataCompromisso = Convert.ToDateTime(leitorCompromissos["DATACOMPROMISSO"]);
                string horaInicio = Convert.ToString(leitorCompromissos["HORAINICIO"]);
                string horaTermino = Convert.ToString(leitorCompromissos["HORATERMINO"]);
                string nomeContato = Convert.ToString(leitorCompromissos["NOME"]);

                Compromisso c = new Compromisso(assunto, local, dataCompromisso, horaInicio, horaTermino, nomeContato);
                c.id = id;

                compromissos.Add(c);
            }

            conexaoComBanco.Close();

            return compromissos;
        }

    }
}
