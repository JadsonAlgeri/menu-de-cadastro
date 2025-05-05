using System;

namespace MenuDeCadastro
{
    class Program
    {
        static List<Cadastro> cadastros = new List<Cadastro>();

        static void Main(string[] Args)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Menu de cadastro");
                    Console.WriteLine();
                    Console.WriteLine("1. Adicionar cadastro");
                    Console.WriteLine("2. Listar cadastro");
                    Console.WriteLine("3. Atualizar cadastro");
                    Console.WriteLine("4. Apagar cadastro");
                    Console.WriteLine();
                    Console.WriteLine("0. Sair");
                    Console.WriteLine();

                    Console.Write("Escolha uma opção: ");
                    string opcao = Console.ReadLine();

                    switch (opcao)
                    {
                        case "1":
                            AdicionarCadastro();
                            break;
                        case "2":
                            ListarCadastro();
                            break;
                        case "3":
                            AtualizarCadastro();
                            break;
                        case "4":
                            ApagarCadastro();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }

                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro de exibição: {ex.Message}");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void AdicionarCadastro()
        {
            try
            {
                Console.WriteLine("\nAdicionar Cadastro");

                int identificador;
                while (true)
                {
                    Console.Write("ID (1 a 10): ");
                    string entrada = Console.ReadLine();

                    if (int.TryParse(entrada, out identificador) && identificador >= 1 && identificador <= 10)
                    {
                        if (cadastros.Any(c => c.ID == identificador))
                        {
                            Console.WriteLine("Este ID já está em uso. Por favor, tente escolher outro.");
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID inválido. Por favor, tente novamente.");
                    }
                }

                string cep;
                while (true)
                {
                    Console.Write("CEP (apenas números, até 8 dígitos): ");
                    cep = Console.ReadLine();
                    if (cep.Length <= 8 && cep.All(char.IsDigit)) break;
                    Console.WriteLine("CEP inválido. Por favor, insira dados corretos.");
                }

                string[] ufsValidas = { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };
                string uf;
                while (true)
                {
                    Console.Write("UF: ");
                    uf = Console.ReadLine().Trim().ToUpper();
                    if (ufsValidas.Contains(uf)) break;
                    Console.WriteLine("UF inválida. Por favor, insira dados corretos.");
                }

                Console.Write("Cidade: ");
                string cidade = Console.ReadLine();

                Console.Write("Bairro: ");
                string bairro = Console.ReadLine();

                Console.Write("Logradouro: ");
                string logradouro = Console.ReadLine();

                int numero;
                while (true)
                {
                    Console.Write("Número da casa: ");
                    if (int.TryParse(Console.ReadLine(), out numero)) break;
                    Console.WriteLine("Número inválido. Por favor, insira dados corretos.");
                }

                Console.Write("Confirmar cadastro? Por favor, responda com S ou N. ");
                string resposta = Console.ReadLine().Trim().ToUpper();

                if (resposta == "S")
                {
                    Cadastro novo = new Cadastro
                    {
                        ID = identificador,
                        CEP = cep,
                        UF = uf,
                        Cidade = cidade,
                        Bairro = bairro,
                        Logradouro = logradouro,
                        Numero = numero
                    };
                    cadastros.Add(novo);
                    Console.WriteLine("Cadastro salvo com sucesso. Por favor, confira em menu inicial (2).");
                }
                else
                {
                    Console.WriteLine("Cadastro cancelado, breve irá retorna-lo ao menu inicial.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro de exibição: {ex.Message}");
            }
        }

        static void ListarCadastro()
        {
            Console.WriteLine("\nListar Cadastro");
            Console.WriteLine("1. Listar todos");
            Console.WriteLine("2. Listar por ID");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            if (opcao == "1")
            {
                ListarTodos();
            }
            else if (opcao == "2")
            {
                ListarUm();
            }
            else
            {
                Console.WriteLine("Opção inválida. Por favor, tente novamente.");
            }
        }

        static void ListarTodos()
        {
            Console.WriteLine("\nTodos os Cadastros foi selecionado. Por favor, aguarde.");

            if (cadastros.Count == 0)
            {
                Console.WriteLine("Nenhum cadastro encontrado. Por favor, confira se está correto.");
                return;
            }

            foreach (var c in cadastros)
            {
                ExibirCadastro(c);
            }
        }

        static void ListarUm()
        {
            try
            {
                Console.Write("\nPor favor, digite o ID desejado: ");
                if (!int.TryParse(Console.ReadLine(), out int idBusca))
                {
                    Console.WriteLine("ID inválido. Por favor, confira se está correto.");
                    return;
                }

                var c = cadastros.FirstOrDefault(c => c.ID == idBusca);

                if (c == null)
                {
                    Console.WriteLine("Cadastro não encontrado. Por favor, confira se está correto.");
                }
                else
                {
                    ExibirCadastro(c);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar cadastro: {ex.Message}");
            }
        }

        static void AtualizarCadastro()
        {
            try
            {
                Console.Write("\nPor favor, digite o ID do cadastro que deseja atualizar: ");
                if (!int.TryParse(Console.ReadLine(), out int idBusca))
                {
                    Console.WriteLine("ID inválido. Por favor, confira se está correto.");
                    return;
                }

                var cadastro = cadastros.FirstOrDefault(c => c.ID == idBusca);

                if (cadastro == null)
                {
                    Console.WriteLine("Cadastro não encontrado. Por favor, confira se está correto.");
                    return;
                }

                Console.WriteLine("\nCadastro encontrado. Por favor, deixe em branco se deseja deixar atualmente como está.\n");

                Console.Write($"CEP atual: {cadastro.CEP} | Novo CEP: ");
                string cep = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(cep)) cadastro.CEP = cep;

                Console.Write($"UF atual: {cadastro.UF} | Nova UF: ");
                string uf = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(uf)) cadastro.UF = uf.ToUpper();

                Console.Write($"Cidade atual: {cadastro.Cidade} | Nova cidade: ");
                string cidade = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(cidade)) cadastro.Cidade = cidade;

                Console.Write($"Bairro atual: {cadastro.Bairro} | Novo bairro: ");
                string bairro = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(bairro)) cadastro.Bairro = bairro;

                Console.Write($"Logradouro atual: {cadastro.Logradouro} | Novo logradouro: ");
                string logradouro = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(logradouro)) cadastro.Logradouro = logradouro;

                Console.Write($"Número atual: {cadastro.Numero} | Novo número: ");
                string numeroStr = Console.ReadLine();
                if (int.TryParse(numeroStr, out int novoNumero)) cadastro.Numero = novoNumero;

                Console.WriteLine("Cadastro atualizado com sucesso. Por favor, confira em menu (2).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro de exibição: {ex.Message}");
            }
        }

        static void ApagarCadastro()
        {
            try
            {
                Console.Write("\nPor favor, digite o ID do cadastro que deseja apagar: ");
                if (!int.TryParse(Console.ReadLine(), out int idBusca))
                {
                    Console.WriteLine("ID inválido. Por favor, confira se está correto.");
                    return;
                }

                var cadastro = cadastros.FirstOrDefault(c => c.ID == idBusca);

                if (cadastro == null)
                {
                    Console.WriteLine("Cadastro não encontrado. Por favor, confira se está correto.");
                    return;
                }

                Console.WriteLine($"Deseja apagar o cadastro de ID {cadastro.ID}? Por favor, responda com S ou N.");
                string confirmacao = Console.ReadLine().Trim().ToUpper();

                if (confirmacao == "S")
                {
                    cadastros.Remove(cadastro);
                    Console.WriteLine("Cadastro apagado com sucesso. Por favor, confira em menu (2).");
                }
                else
                {
                    Console.WriteLine("Operação cancelada, breve irá retorna-lo ao menu inicial.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro de exibição: {ex.Message}");
            }
        }

        static void ExibirCadastro(Cadastro c)
        {
            try
            {
                Console.WriteLine($"\nID: {c.ID}");
                Console.WriteLine($"CEP: {c.CEP}");
                Console.WriteLine($"UF: {c.UF}");
                Console.WriteLine($"Cidade: {c.Cidade}");
                Console.WriteLine($"Bairro: {c.Bairro}");
                Console.WriteLine($"Logradouro: {c.Logradouro}");
                Console.WriteLine($"Número: {c.Numero}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro de exibição: {ex.Message}");
            }
        }
    }

    class Cadastro
    {
        public int ID { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
    }
}