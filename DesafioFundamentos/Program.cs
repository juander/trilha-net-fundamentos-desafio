using DesafioFundamentos.Models;

// Coloca o encoding para UTF8 para exibir acentuação
Console.OutputEncoding = System.Text.Encoding.UTF8;

// DEC-09 — Comportamento seguro de Clear / ReadKey com I/O redirecionado
LimparTerminal();

// DEC-07 — Uso de Raw String Literals para menus
Console.WriteLine($"""
==================================================
  Seja bem - vindo ao sistema de estacionamento!
================================================== 

""");

Console.Write("Por favor, digite o preço inicial do seu estacionamento: R$ ");

// Criando e preenchendo as variáveis
// DEC-06 — Tratamento de input inválido
decimal precoInicial = Convert.ToDecimal(Console.ReadLine());

Console.Write("Agora digite o preço por hora do seu estacionamento: R$ ");
decimal precoPorHora = Convert.ToDecimal(Console.ReadLine());

// Instanciando a classe Estacionamento com tipo explícito sem usar var para melhor legibilidade
Estacionamento estacionamento = new Estacionamento(precoInicial, precoPorHora);

// string opcao = string.Empty; Essa variável não é necessária já que estamos lidando com swith case
bool exibirMenu = true;

// Realiza o loop do menu
while (exibirMenu)
{
    LimparTerminal();
    Console.WriteLine("""
    ==================================================
                    MENU PRINCIPAL
    --------------------------------------------------
    1 - Cadastrar veículo
    2 - Remover veículo
    3 - Listar veículos
    4 - Encerrar
    ==================================================

    """); 
    Console.Write("Digite a sua opção: ");

    switch (Console.ReadLine())
    {
        case "1":
            estacionamento.AdicionarVeiculo();
            break;

        case "2":
            estacionamento.RemoverVeiculo();
            break;

        case "3":
            estacionamento.ListarVeiculos();
            break;

        case "4":
            exibirMenu = false;
            break;

        default:
            Console.WriteLine("\nOpção inválida! Tente novamente.");
            break;
    }

    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    // DEC-09 — Comportamento seguro de Clear / ReadKey com I/O redirecionado
    if (!Console.IsInputRedirected)
    {
        Console.ReadKey();
    }
    else
    {
        // Se estiver no modo debug (ou com entrada redirecionada), 
        // espera por um Enter para evitar o erro.
        Console.ReadLine();
    }
}

LimparTerminal();
Console.WriteLine("""
==================================================
        Obrigado por utilizar o sistema!
==================================================
""");

static void LimparTerminal()
{
    // DEC-09 — Comportamento seguro de Clear / ReadKey com I/O redirecionado
    if (!Console.IsOutputRedirected)
    {
        Console.Clear();
    }
    // Se for redirecionada, o método simplesmente não faz nada
}