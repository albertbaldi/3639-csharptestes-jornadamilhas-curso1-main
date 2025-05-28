using System.Runtime.InteropServices;
using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test;

public class OfertaViagemConstritor
{
    [Theory]
    [InlineData("", null, "2024-01-01", "2024-01-02", 0.0, false)]
    [InlineData("origem-teste", "destino-teste", "2024-02-01", "2024-02-05", 100.0, true)]
    [InlineData(null, "destino-teste", "2024-02-01", "2024-02-05", -1.0, false)]
    [InlineData("origem-teste", "destino-teste", "2024-02-01", "2024-02-01", 0.0, false)]
    [InlineData("origem-teste", "destino-teste", "2024-02-01", "2024-02-05", -500.0, false)]
    public void RetornaEhValidoDeAcordoComDadosDeEntrada(string origem, string destino, string dataIda, string dataVolta, double preco, bool validacao)
    {
        // Arrange
        Rota rota = new Rota(origem, destino);
        Periodo periodo = new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

        // Act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        // Assert
        Assert.Equal(validacao, oferta.EhValido);
    }

    [Fact]
    public void RetornaMensagemDeErroDeRotaOuPeriodoInvalidosQuandoRotaForNula()
    {
        // Arrange
        Rota rota = null;
        Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
        double preco = 100.0;

        // Act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        // Assert
        Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-250)]
    public void RetornaMensagemDeErroDePrecoInvalidoQuandoPrecoMenorOuIgualZero(double preco)
    {
        // Arrange
        Rota rota = new Rota("origem-teste", "destino-teste");
        Periodo periodo = new Periodo(new DateTime(2025, 5, 1), new DateTime(2025, 5, 10));

        // Act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        // Assert
        Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
    }
}
