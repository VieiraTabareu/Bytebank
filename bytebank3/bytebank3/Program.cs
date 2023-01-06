using System;

namespace ByteBank
{
    public class Program
    {
        public static void MenuPrincipal()
        {
            Console.WriteLine("- MENU PRINCIPAL -");
            Console.WriteLine();
            Console.WriteLine("1. Cadastrar usuário."); //feito
            Console.WriteLine("2. Listar contas cadastradas."); //feito
            Console.WriteLine("3. Gerenciar uma conta"); //feito
            Console.WriteLine("4. Quantia armazenada no banco."); //feito
            Console.WriteLine("5. Sair"); //feito
            Console.WriteLine();
            Console.Write("Digite uma opção: ");
        }

        public static void ManipularConta(int indexParaLogar, List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos, List<int> contas)
        {
            int opcaoMenuManipular;

            do
            {
                Console.Clear();
                Console.WriteLine("- ESPAÇO DO CLIENTE -");
                Console.WriteLine();
                Console.WriteLine("1. Detalhes da conta");
                Console.WriteLine("2. Realizar transações");
                Console.WriteLine("3. Excluir conta");
                Console.WriteLine("4. Voltar");
                Console.WriteLine();
                Console.Write("Digite a opção desejada: ");

                int.TryParse(Console.ReadLine(), out opcaoMenuManipular);

                switch (opcaoMenuManipular)
                {
                    case 1:
                        Console.Clear();
                        DetalhesConta(indexParaLogar, contas, cpfs, titulares, saldos);
                        Console.WriteLine();
                        Console.WriteLine("-> Pressione qualquer tecla para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        MenuTransacoes();
                        int.TryParse(Console.ReadLine(), out int opcaoMenuTransacoes);
                        switch (opcaoMenuTransacoes)
                        {
                            case 1:
                                Sacar(indexParaLogar, saldos);
                                Console.WriteLine();
                                Console.WriteLine("-> Pressione qualquer tecla para continuar");
                                Console.ReadKey();
                                Console.Clear();
                                break;

                            case 2:
                                Depositar(indexParaLogar, saldos);
                                Console.WriteLine();
                                Console.WriteLine("-> Pressione qualquer tecla para continuar");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 3:
                                Transferencia(indexParaLogar, cpfs, contas, titulares, saldos);
                                Console.WriteLine();
                                break;
                            case 4:
                                return;
                            default:
                                Console.WriteLine("Opção inválida.");
                                Console.WriteLine();
                                Console.WriteLine("-> Pressione qualquer tecla para voltar.");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                        }
                        break;
                    case 3:
                        ExcluirConta(indexParaLogar, cpfs, titulares, senhas, saldos, contas);
                        return;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

            } while (opcaoMenuManipular != 4);

        }

        private static void MenuTransacoes()
        {
            Console.Clear();
            Console.WriteLine("- TRANSAÇÕES -");
            Console.WriteLine();
            Console.WriteLine("1. Sacar.");
            Console.WriteLine("2. Depositar.");
            Console.WriteLine("3. Transferir.");
            Console.WriteLine("4. Voltar.");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
        }

        public static void CadastrarUsuario(List<string> titulares, List<string> cpfs, List<string> senhas, List<int> contas, List<double> saldos)
        {
            Console.WriteLine(" - CADASTRO DE USUÁRIO -");
            Console.WriteLine();
            Console.Write("Digite o nome completo: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite o CPF: ");
            cpfs.Add(Console.ReadLine());
            saldos.Add(0);

            string senha = "0";
            string senha2 = "1";

            do
            {
                Console.Write("Digite uma nova senha: ");
                senha = MascararSenha();
                Console.Write("Repita a nova senha: ");
                senha2 = MascararSenha();

                if (senha != senha2)
                {
                    Console.WriteLine("Senha inválida.");
                }
            }
            while (senha != senha2);

            Console.WriteLine("Senha cadastrada com sucesso.");

            senhas.Add(senha);

            Random numeroAleatorio = new Random();
            int conta = numeroAleatorio.Next(2500, 8750);
            contas.Add(conta);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Conta nº {0} cadastrada com sucesso.", conta);
            Console.WriteLine();
            Console.WriteLine();
        }

        public static string MascararSenha()
        {
            var pass = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine();
            return pass;
        }

        public static void ListarContas(List<int> contas, List<string> titulares, List<string> cpfs)
        {
            Console.WriteLine("- CONTAS BYTEBANK -");
            Console.WriteLine();
            if (cpfs.Count > 0)
            {
                for (int i = 0; i < cpfs.Count; i++)
                {
                    Console.WriteLine($"Conta: {contas[i]} | Titular: {titulares[i]} | CPF: {cpfs[i]}\n");
                }
            }
            else
            {
                Console.WriteLine("Não há contas registradas no momento.");
            }
        }

        public static void Login(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos, List<int> contas)
        {
            Console.WriteLine("LOGIN DE USUÁRIO");
            Console.WriteLine();

            int indexParaLogar;
            string entrada = "n";

            do
            {
                Console.Write("Digite o CPF: ");
                string cpfParaLogar = Console.ReadLine();
                indexParaLogar = cpfs.FindIndex(cpf => cpf == cpfParaLogar);

                if (indexParaLogar == -1)
                {
                    Console.Write("CPF inválido. Tentar novamente (S/N)? ");
                    entrada = Console.ReadLine().ToLower();
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("Digite a senha: ");
                    string senhaLogin = MascararSenha();

                    if (senhas[indexParaLogar] == senhaLogin)
                    {
                        Console.WriteLine("Login efetuado. Bem vindo.");
                        ManipularConta(indexParaLogar, cpfs, titulares, senhas, saldos, contas);
                    }
                    else
                    {
                        Console.Write("Senha inválida. Tentar novamente (S/N)? ");
                        entrada = Console.ReadLine().ToLower();
                        Console.WriteLine();
                    }
                }

            }
            while (entrada == "s");

        }

        public static void ExcluirConta(int indexParaLogar, List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos, List<int> contas)
        {
            string entrada = "n";
            Console.WriteLine("- EXCLUSÃO DE USUÁRIO -");
            Console.WriteLine();
            do
            {
                DetalhesConta(indexParaLogar, contas, cpfs, titulares, saldos);

                Console.Write("Confirmar exclusão (S/N)? ");
                entrada = Console.ReadLine().ToLower();

                if (entrada == "s")
                {
                    cpfs.Remove(cpfs[indexParaLogar]);
                    titulares.RemoveAt(indexParaLogar);
                    senhas.RemoveAt(indexParaLogar);
                    saldos.RemoveAt(indexParaLogar);
                    contas.RemoveAt(indexParaLogar);

                    Console.WriteLine("Conta deletada com sucesso.");
                    Console.WriteLine();
                    Console.WriteLine("-> Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                }
                else if (entrada == "n")
                {

                    Console.WriteLine("Pressione qualquer tecla para voltar.");
                    return;
                }
                else
                {
                    Console.WriteLine("Comando inválido.");
                }

            } while (entrada != "s");

        }

        public static void DetalhesConta(int indexParaLogar, List<int> contas, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine("- DETALHES DA CONTA -");
            Console.WriteLine();
            Console.WriteLine($"Conta nº {contas[indexParaLogar]} | CPF:  {cpfs[indexParaLogar]} | Titular: {titulares[indexParaLogar]} | Saldo: R${saldos[indexParaLogar]:F2}");
        }

        public static void TotalArmazenado(List<double> saldos)
        {
            Console.WriteLine("- PATRIMONIO BYTEBANK -");
            Console.WriteLine();
            Console.WriteLine($"Total armazenado no banco: R${saldos.Sum()}");
        }


        public static void Depositar(int indexParaLogar, List<double> saldos)
        {
            Console.Clear();
            Console.WriteLine("- MENU DE DEPOSITO -");
            Console.WriteLine();
            double valorDeposito;

            do
            {
                Console.Write("Valor a ser depositado: R$ ");
                double.TryParse(Console.ReadLine(), out valorDeposito);


                if (valorDeposito <= 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Valor inválido.");
                    Console.WriteLine();
                    return;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Depósito realizado com sucesso.");
                    Console.WriteLine();

                    saldos[indexParaLogar] += valorDeposito;

                    Console.WriteLine($"Saldo atual: R${saldos[indexParaLogar]:f2}");
                    Console.WriteLine();
                    return;
                }

            } while (valorDeposito <= 0);
        }

        public static void Sacar(int indexParaLogar, List<double> saldos)
        {
            Console.Clear();
            Console.WriteLine("- MENU DE SAQUE -");
            Console.WriteLine();
            double valorSaque;

            do
            {
                Console.WriteLine();
                Console.Write("Valor do saque: R$ ");
                double.TryParse(Console.ReadLine(), out valorSaque);


                if (valorSaque <= 0 || valorSaque > saldos[indexParaLogar])
                {
                    Console.WriteLine();
                    Console.WriteLine("Valor inválido.");
                    Console.WriteLine();
                    return;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Saque realizado com sucesso.");
                    Console.WriteLine();

                    saldos[indexParaLogar] -= valorSaque;

                    Console.WriteLine($"Saldo atual: R${saldos[indexParaLogar]:f2}");
                    Console.WriteLine();
                    return;
                }

            } while (valorSaque <= 0 || valorSaque > saldos[indexParaLogar]);
        }

        public static void Transferencia(int indexParaLogar, List<string> cpfs, List<int> contas, List<string> titulares, List<double> saldos)
        {
            int indexContaDestino;
            string entrada = "n";

            do
            {
                Console.WriteLine();
                Console.Write("Forneça o CPF da conta na qual deseja realizar a transferência: ");
                string cpfContaDestino = Console.ReadLine();
                indexContaDestino = cpfs.FindIndex(cpf => cpf == cpfContaDestino);

                if (indexContaDestino == -1)
                {
                    Console.WriteLine("Conta de destino não encontrada.");
                    Console.WriteLine();
                    return;
                }
                else if (indexContaDestino == indexParaLogar)
                {
                    Console.WriteLine("Não é possível transferir para a mesma conta. Tente novamente.");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Confirme a conta:" +
                        $"Conta nº {contas[indexContaDestino]} | CPF:  {cpfs[indexContaDestino]} | Titular: {titulares[indexContaDestino]}");
                    Console.WriteLine();
                }

            } while (indexContaDestino == -1 || indexContaDestino == indexParaLogar);

            double valorTransferencia;

            do
            {
                Console.Write("Valor da transferência: R$");
                double.TryParse(Console.ReadLine(), out valorTransferencia);

                if (valorTransferencia <= 0 || valorTransferencia > saldos[indexParaLogar])
                {
                    Console.WriteLine("Valor inválido. Tente novamente");
                    return;
                }
                else
                {
                    saldos[indexParaLogar] -= valorTransferencia;
                    saldos[indexContaDestino] += valorTransferencia;

                    Console.WriteLine("Transferência realizada.");
                    Console.WriteLine();
                    Console.WriteLine($"Saldo atual: R$ {saldos[indexParaLogar]:f2}");
                    Console.WriteLine();
                    Console.WriteLine("-> Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                }

            } while (valorTransferencia <= 0 || valorTransferencia > saldos[indexParaLogar]);

        }

        public static void Main(string[] args)
        {
            Console.WriteLine("B Y T E B A N K");

            List<int> contas = new List<int>();
            List<string> titulares = new List<string>();
            List<string> cpfs = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            Console.WriteLine();

            int opcao;

            do
            {
                Console.Clear();
                MenuPrincipal();

                int.TryParse(Console.ReadLine(), out opcao);

                Console.WriteLine();
                Console.WriteLine("----------------------");
                Console.WriteLine();

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        CadastrarUsuario(titulares, cpfs, senhas, contas, saldos);
                        Console.WriteLine();
                        Console.WriteLine("> Pressione qualquer tecla para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        ListarContas(contas, titulares, cpfs);
                        Console.WriteLine();
                        Console.WriteLine("> Pressione qualquer tecla para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        Login(cpfs, titulares, senhas, saldos, contas);
                        Console.WriteLine();
                        break;
                    case 5:
                        Console.WriteLine("Encerrando...");
                        break;
                    case 4:
                        TotalArmazenado(saldos);
                        Console.WriteLine();
                        Console.WriteLine("-> Pressione qualquer tecla para continuar.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Opção invalida.");
                        Console.WriteLine();
                        break;
                }
            }
            while (opcao != 5);
        }
    }
}