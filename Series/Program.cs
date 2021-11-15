using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Series
{
    public class SerieRepositorio : IRepositorio<seriesGo>
    {
        private List<seriesGo> listaSerie = new List<seriesGo>();
        public void Atualiza(int id, seriesGo objeto)
        {
            listaSerie[id] = objeto;
        }

        public void Exclui(int id)
        {
            listaSerie[id].Excluir();
        }

        public void Insere(seriesGo objeto)
        {
            listaSerie.Add(objeto);
        }

        public List<seriesGo> Lista()
        {
            return listaSerie;
        }

        public int ProximoId()
        {
            return listaSerie.Count;
        }

        public seriesGo RetornaPorId(int id)
        {
            return listaSerie[id];
        }
    }
    public interface IRepositorio<T>
    {
        List<T> Lista();
        T RetornaPorId(int id);
        void Insere(T endidade);
        void Exclui(int id);
        void Atualiza(int id, T endidade);
        int ProximoId();
    }
    public enum Genero
    {
        Acao = 1,
        Aventura = 2,
        Comedia = 3,
        Documentario = 4,
        Drama = 5,
        Espionagem = 6,
        Faroeste = 7,
        Fantasia = 8,
        Ficcao_Cientifica = 9,
        Musical = 10,
        Romance = 11,
        Suspence = 12,
        Terror = 13
    }
    public class seriesGo : entidadeBase
    {
        //ATRIBUTOS
        private Genero Genero { get; set; }
        private string Titulo { get; set; }
        private string Descricao { get; set; }
        private int Ano { get; set; }
        private bool Excluido { get; set; }

        //METHODOS
        public seriesGo(int id, Genero genero, string titulo, string descricao, int ano)
        {
            this.id = id;
            this.Genero = genero;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Ano = ano;
            this.Excluido = false;
        }
        public override string ToString()
        {
            string retorno = "";
            retorno += "Genero: " + this.Genero + Environment.NewLine;
            retorno += "Titulo: " + this.Titulo + Environment.NewLine;
            retorno += "Descricao: " + this.Descricao + Environment.NewLine;
            retorno += "Ano de Inicio: " + this.Ano + Environment.NewLine;
            retorno += "Excluido: " + this.Excluido;
            return retorno;
        }
        public string retornaTitulo()
        {
            return this.Titulo;
        }
        public bool retornaExcluido()
        {
            return this.Excluido;
        }
        public int retornaId()
        {
            return this.id;
        }
        public void Excluir()
        {
            this.Excluido = true;
        }
    }
    public abstract class entidadeBase
    {
        public int id { get; protected set; }
    }
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();
            while(opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }
            Console.WriteLine("Obrigado por Ultilizar Nossos Servicos");
            Console.ReadLine();
        }
        private static void ExcluirSerie()
        {
            Console.WriteLine("Digite o ID da Serio: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            repositorio.Exclui(indiceSerie);
        }
        private static void VisualizarSerie()
        {
            Console.WriteLine("Digite o ID da Serie: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            var serie = repositorio.RetornaPorId(indiceSerie);
            Console.WriteLine(serie);
        }
        private static void AtualizarSerie()
        {
            Console.WriteLine("Digite o ID da Serie: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.Write("{0}-{1}\n", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o Genero entre as Opcoes acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o Titulo da Serie: ");
            string entradaTitulo = Console.ReadLine();
            Console.WriteLine("Digite o Ano de Lancamento da Serie: ");
            int entradaAno = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite a Descricao da Serie: ");
            string entradaDescricao = Console.ReadLine();
            seriesGo atualizaSerie = new seriesGo(id: indiceSerie, genero: (Genero)entradaGenero, titulo: entradaTitulo, ano: entradaAno, descricao: entradaDescricao);
            repositorio.Atualiza(indiceSerie, atualizaSerie);

        }
        private static void ListarSeries()
        {
            Console.WriteLine("Listar Series");
            var lista = repositorio.Lista();
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma Serie Cadastrada.");
                return;
            }
            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*EXCLUIDO*" : ""));  
            }
        }
        private static void InserirSerie()
        {
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.Write("{0}-{1}\n", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o Genero entre as Opcoes acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o Titulo da Serie: ");
            string entradaTitulo = Console.ReadLine();
            Console.WriteLine("Digite o Ano de Lancamento da Serie: ");
            int entradaAno = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite a Descricao da Serie: ");
            string entradaDescricao = Console.ReadLine();
            seriesGo novaSerie = new seriesGo(id: repositorio.ProximoId(), genero:(Genero)entradaGenero, titulo: entradaTitulo, ano: entradaAno, descricao: entradaDescricao);
            repositorio.Insere(novaSerie);
        }
        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("BRESOLIN Series, O Mundo Magico da Procrastinacao!");
            Console.WriteLine("Informe a Opcao Desejada: ");

            Console.WriteLine("1- Listar Series");
            Console.WriteLine("2- Inserir Nova Serie");
            Console.WriteLine("3- Atualizar Serie");
            Console.WriteLine("4- Excluir Serie");
            Console.WriteLine("5- Visualizar Serie");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
