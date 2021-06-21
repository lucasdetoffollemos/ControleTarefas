using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppControleTarefas
{
    public abstract class TelaCadastro<T> : TelaBase where T : EntidadeBase
    {
        private readonly ControladorBase<T> controlador;

        public TelaCadastro(string tit, ControladorBase<T> c) : base(tit)
        {
            controlador = c;
        }

        public abstract T ObterObjeto(TipoAcao tipoAcao);

        public abstract void ApresentarMensagem(List<T> registros);

        public void InserirNovoRegistro()
        {
  
            ConfigurarTela("Inserindo uma novo registro...");

            TipoAcao tipo = new TipoAcao();
            T registro = ObterObjeto(tipo);

            string resultadoValidacao = controlador.InserirNovo(registro);

            if (resultadoValidacao == "ESTA_VALIDO")
                ApresentarMensagem("Registro inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("Visualizando registros...");

            List<T> registros = controlador.SelecionarTodosRegistros();

            if (registros.Count == 0)
            {
                ApresentarMensagem("Nenhuma registro cadastrado!", TipoMensagem.Atencao);
                return false;
            }

            ApresentarMensagem(registros);

            return true;
        }

        public  void EditarRegistro() {

            ConfigurarTela("Editando um Registro...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.WriteLine("\nDigite o número do registro que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            T registroEncontrado = controlador.SelecionarRegistroPorId(id);

            if (registroEncontrado == null)
            {
                ApresentarMensagem("Nenhum registro foi encontrado com este número: " + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }

            T registro = ObterObjeto(TipoAcao.Editando);

            string resultadoValidacao = controlador.EditarRegistro(registroEncontrado.id, registro);

            if (resultadoValidacao == "ESTA_VALIDO")
                ApresentarMensagem("Registro editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro() {
            ConfigurarTela("Excluindo um resgitro...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.WriteLine("\nDigite o número do registro que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            T  registro = controlador.SelecionarRegistroPorId(id);
            if (registro == null)
            {
                ApresentarMensagem("Nenhum registro foi encontrado com este número: " + id, TipoMensagem.Erro);
                ExcluirRegistro();
                return;
            }

            bool conseguiuExcluir = controlador.ExcluirRegistro(registro);

            if (conseguiuExcluir)
                ApresentarMensagem("Registro excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluira tarefa", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

    }
}
