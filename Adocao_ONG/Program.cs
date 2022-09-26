using System;

namespace Adocao_ONG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MostrarMenu();
        }

        static void MostrarMenu()
        {
            Banco banco = new Banco();
            int opcao = 8;

            do
            {
                Console.Clear();
                Console.WriteLine(" |===============  MENU  ===============|");
                Console.WriteLine(" |Opção 1 : Cadastrar Pessoa            |");
                Console.WriteLine(" |Opção 2 : Alterar Dados Pessoa        |");
                Console.WriteLine(" |Opção 3 : Mostrar Pessoas Cadastradas |");
                Console.WriteLine(" |======================================|");
                Console.WriteLine(" |Opção 4 : Cadastrar Animal            |");
                Console.WriteLine(" |Opção 5 : Alterar Dados Animal        |");
                Console.WriteLine(" |Opção 6 : Mostrar Animais Cadastrados |");
                Console.WriteLine(" |======================================|");
                Console.WriteLine(" |Opção 7 : Adotar um Animal            |");
                Console.WriteLine(" |======================================|");
                Console.WriteLine(" |Opção 0 : Sair                        |");
                Console.WriteLine(" |======================================|");

                Console.Write("\n Informe a opção: ");

                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }

                switch (opcao)
                {
                    case 0:
                        Environment.Exit(0);
                        break;

                    case 1:
                        Console.Clear();
                        banco.InserirPessoaBanco();
                        MostrarMenu();
                        break;

                    case 2:
                        Console.Clear();
                        banco.AtualizarPessoaBanco();
                        MostrarMenu();
                        break;

                    case 3:
                        Console.Clear();
                        banco.SelecionarPessoasBanco();
                        MostrarMenu();
                        break;

                    case 4:
                        Console.Clear();
                        banco.InserirAnimalBanco();
                        MostrarMenu();
                        break;

                    case 5:
                        Console.Clear();
                        banco.AtualizarAnimalBanco();
                        MostrarMenu();
                        break;

                    case 6:
                        Console.Clear();
                        banco.SelecionarAnimaisBanco();
                        MostrarMenu();
                        break;

                    case 7:
                        Console.Clear();
                        banco.AdotarAnimal();
                        MostrarMenu();
                        break;

                    default:
                        Console.Write("\n Opcao Inválida! Aperte [ENTER] para executar novamente.");
                        Console.ReadKey();
                        break;
                }
            } while (true);
        }
    }
}
