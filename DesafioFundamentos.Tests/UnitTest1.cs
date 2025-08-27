using DesafioFundamentos.Models;
using System.IO;
using System;
using Xunit;

namespace DesafioFundamentos.Tests
{
    public class EstacionamentoTests
    {
        private readonly decimal _precoInicial = 2.00m;
        private readonly decimal _precoPorHora = 1.50m;

        [Fact]
        public void AdicionarVeiculo_DeveAdicionarUmVeiculoComSucesso()
        {
            // Arrange
            var estacionamento = new Estacionamento(_precoInicial, _precoPorHora);
            var placa = "ABC-1234";

            // Simula a entrada do usuário no console
            var stringReader = new StringReader(placa);
            Console.SetIn(stringReader);

            // Redireciona a saída do console para uma variável
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            estacionamento.AdicionarVeiculo();

            // Assert
            var output = stringWriter.ToString();
            // Verifica se a mensagem de sucesso foi exibida
            Assert.Contains($"Veículo de placa '{placa}' adicionado ao estacionamento com sucesso", output);
        }

        [Fact]
        public void RemoverVeiculo_DeveRemoverVeiculoECalcularValorCorretamente()
        {
            // Arrange
            var estacionamento = new Estacionamento(_precoInicial, _precoPorHora);
            var placa = "XYZ-5678";
            var horasEstacionado = "3";

            // Adiciona um veículo primeiro
            var inputPlaca = new StringReader(placa);
            Console.SetIn(inputPlaca);
            estacionamento.AdicionarVeiculo();

            // Simula a entrada do usuário para remover o veículo
            var inputRemocao = new StringReader($"{placa}\n{horasEstacionado}");
            Console.SetIn(inputRemocao);

            // Redireciona a saída do console
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            estacionamento.RemoverVeiculo();

            // Assert
            var output = stringWriter.ToString();
            // Calcula o valor esperado: precoInicial + (horas * precoPorHora)
            decimal valorEsperado = _precoInicial + (int.Parse(horasEstacionado) * _precoPorHora);
            Assert.Contains($"O veículo {placa} foi removido e o preço total a ser cobrado foi de: R$ {valorEsperado:F2}", output);
        }

        [Fact]
        public void ListarVeiculos_DeveListarOsVeiculosEstacionados()
        {
            // Arrange
            var estacionamento = new Estacionamento(_precoInicial, _precoPorHora);
            var placa1 = "PLT-0001";
            var placa2 = "PLT-0002";

            // Adiciona dois veículos
            Console.SetIn(new StringReader(placa1));
            estacionamento.AdicionarVeiculo();
            Console.SetIn(new StringReader(placa2));
            estacionamento.AdicionarVeiculo();

            // Redireciona a saída do console
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            estacionamento.ListarVeiculos();

            // Assert
            var output = stringWriter.ToString();
            Assert.Contains("Os veículos estacionados são:", output);
            Assert.Contains($"- {placa1}", output);
            Assert.Contains($"- {placa2}", output);
        }
        
        [Fact]
        public void ListarVeiculos_DeveInformarQuandoNaoHaVeiculos()
        {
            // Arrange
            var estacionamento = new Estacionamento(_precoInicial, _precoPorHora);
            
            // Redireciona a saída do console
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            estacionamento.ListarVeiculos();

            // Assert
            var output = stringWriter.ToString();
            Assert.Contains("Não há veículos estacionados.", output);
        }
    }
}