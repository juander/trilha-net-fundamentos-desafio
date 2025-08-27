// Implemetação da classe Estacionamento, sendo ela a principal do sistema que vai permitir todo o gerenciamento dos veículos
namespace DesafioFundamentos.Models
{
    // DEC-02 — Construtor Primário no Estacionamento
    public class Estacionamento(decimal PrecoInicial, decimal PrecoPorHora)
    {
        // Lista de veiculos que serão gerenciados
        private List<Veiculo> veiculos = new List<Veiculo>();

        // DEC-03 — Separação parcial de responsabilidades
        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar (no formato XXX-1111):");

            // Tratando sempre em letras maiúsculas que é o padrão definido como regra do programa
            string placa = Console.ReadLine().ToUpper();

            // Verificando se já existe um veículo registrado com essa placa
            if (veiculos.Any(v => v.Placa == placa))
            {
                Console.WriteLine($"Erro: O veículo de placa '{placa}' já está no estacionamento.");
                return; // Encerra o método aqui nesse caso
            }

            try
            {
                // Tentando adicionar o veículo a lista
                Veiculo veiculo = new Veiculo(placa);
                veiculos.Add(veiculo);

                Console.WriteLine($"Veículo de placa '{placa}' adicionado ao estacionamento com sucesso");
            }
            catch (ArgumentException ex)
            {
                // Capturou uma exceção, nesse caso sobre o formato da placa
                Console.WriteLine($"Erro ao tentar adicionar o veículo ao estacionamento: {ex.Message}");
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            // Tratando sempre em letras maiúsculas que é o padrão definido
            string placa = Console.ReadLine().ToUpper();

            // Verifica se o veículo existe
            if (veiculos.Any(v => v.Placa == placa))
            {
                // Loop para receber o input de horas com validação e conversão para INT
                int horas = 0;
                while (true)
                {
                    Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
                    string input = Console.ReadLine();

                    // DEC-06 — Tratamento de input inválido
                    if (int.TryParse(input, out horas) && horas >= 0)
                        break;

                    Console.WriteLine("Valor inválido! Digite um número inteiro não negativo.");
                }

                // Calculo do valor total usando o método Private que a classe tem acesso
                decimal valorTotal = CalcularValorTotal(horas);

                // DEC-05 — Uso de RemoveAll para exclusão por placa
                int numeroDeVeiculosRemovidos = veiculos.RemoveAll(v => v.Placa == placa);

                // Validação para confirmar que o veículo foi removido com o .RemoveAll()
                if (numeroDeVeiculosRemovidos > 0)
                {
                    Console.WriteLine($"O veículo {placa} foi removido e o preço total a ser cobrado foi de: R$ {valorTotal:F2}");
                }
                else
                {
                    Console.WriteLine($"Erro ao remover o veículo {placa} usando o método .RemoveAll().");
                }
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
    
                foreach (var veiculo in veiculos)
                {
                    Console.WriteLine($"- {veiculo.Placa}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
        
        // DEC-04 — Cálculo do valor extraído para método separado
        private decimal CalcularValorTotal(int horas)
        {
            if (horas < 0)
                throw new ArgumentException("A quantidade de horas não pode ser negativa.");

            return PrecoInicial + (horas * PrecoPorHora);
        }
    }
}
