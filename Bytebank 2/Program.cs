namespace Bytebank
{

    using System;
    using System.Reflection;

    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            int option;
            do
            {
                Menu();
                option = int.Parse(Console.ReadLine());

                Console.WriteLine("-----------------");

                switch (option)
                {
                    case 1:
                        CadastrarNovoUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        ListarContas(cpfs, titulares, saldos);
                        break;
                    case 3:
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 4:
                        DetalhesUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 5:
                        menu2();
                        break;
                    case 6:
                        Console.WriteLine("Encerrando...");
                        break;
                }

                Console.WriteLine("-----------------");

            } while (option != 6);
        }

        static void Menu()
        {
            Console.WriteLine("Seja bem vindo ao Bytebank.");
            Console.WriteLine();
            Console.WriteLine("1 - Cadastrar novo usuário."); //feito
            Console.WriteLine("2 - Listar usuários registrados."); //feito
            Console.WriteLine("3 - Deletar usuário."); //feito
            Console.WriteLine("4 - Detalhes de um usuário."); //feito
            Console.WriteLine("5 - Gerenciar conta de um usuário.");
            Console.WriteLine("6 - Sair."); //feito
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");
        }

        static void CadastrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o CPF: "); cpfs.Add(Console.ReadLine());
            Console.Write("Digite o Nome: "); titulares.Add(Console.ReadLine());
            Console.Write("Digite a senha: "); senhas.Add(Console.ReadLine());
            saldos.Add(0);
            Console.Clear();
        }

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o CPF que será deletado: ");
            string cpfDeletar = Console.ReadLine();
            int indexDeletar = cpfs.FindIndex(d => d == cpfDeletar);

            if (indexDeletar == -1)
            {
                Console.WriteLine();
                Console.WriteLine("USUÁRIO NÃO ECONTRADO.");
                Console.WriteLine();
            }
            else
            {
                titulares.RemoveAt(indexDeletar);
                senhas.RemoveAt(indexDeletar);
                saldos.RemoveAt(indexDeletar);
                cpfs.RemoveAt(indexDeletar);
                Console.WriteLine();
                Console.WriteLine("USUÁRIO DELETADO COM SUCESSO");
                Console.WriteLine();
            }
        }

        static void ListarContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            for (int i = 0; i < cpfs.Count; i++)
            {
                DetalharConta(i, cpfs, titulares, saldos);
            }
        }

        static void DetalharConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldos = R${saldos[index]:F2}");
        }

        static void DetalhesUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o CPF a ser detalhado: ");
            string cpfPesquisa = Console.ReadLine();
            int indexMostrar = cpfs.FindIndex(cpf => cpf == cpfPesquisa);

            if (indexMostrar == -1)
            {
                Console.WriteLine();
                Console.WriteLine("USUÁRIO NÃO ECONTRADO.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                DetalharConta(indexMostrar, cpfs, titulares, saldos);
                Console.WriteLine();
            }

        }

        static void SubMenu()
        {
            Console.WriteLine("Menu de gerenciamento de usuário");
            Console.WriteLine();
            Console.WriteLine("1 - Sacar.");
            Console.WriteLine("2 - Depositar.");
            Console.WriteLine("3 - Transferir.");
            Console.WriteLine("4 - Voltar ao menu principal.");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");
        }

        static void menu2()
        {
            int option2;
            do
            {
                SubMenu();
                option2 = int.Parse(Console.ReadLine());

                Console.WriteLine("-----------------");

                switch (option2)
                {
                    case 1: //Depositar
                        break;

                    case 2: //sacar
                        break;

                    case 3: //transferir
                        break;

                    case 4: //voltar
                        return;
                }

                Console.WriteLine("-----------------");

            } while (option2 != 4);
        }

        static void Depositar(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine("Area pessoal do cliente.");
            Console.WriteLine("Digite o CPF: "); Console.ReadLine();
            Console.WriteLine("Digite a senha: "); Console.ReadLine();
        }
    }
}