// Implementação da classe veículo
// DEC-01 — Validação de placa diretamente no modelo
namespace DesafioFundamentos.Models
{
    public class Veiculo
    {
        // So quem pode Setar a placa é a própria classe
        public string Placa { get; private set; }

        // Construtor Parametrizado 
        public Veiculo(string placa)
        {
            // Fazendo uma validação básica da placa do veículo 
            if (string.IsNullOrWhiteSpace(placa) || placa.Length != 8)
                // Laçando uma exceção para ser capturada por um blobo try-catch
                throw new ArgumentException("Placa inválida. Deve conter exatamente 8 caracteres.");

            // Mantendo letras da placa em maiúsculas por padrão
            Placa = placa.ToUpper();
        }
    }
}