using System;
using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test;

public class OfertaViagemDesconto
{
    [Fact]
    public void RetornaPrecoAtualizadoQuandoAplicadoDesconto()
    {
        // Arrange
        Rota rota = new Rota("origem-teste", "destino-teste");
        Periodo periodo = new Periodo(new DateTime(2025, 1, 1), new DateTime(2025, 1, 10));
        double precoOriginal = 100.0;
        double desconto = 20.0;
        double precoComDesconto = precoOriginal - desconto;
        OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

        // Act
        oferta.Desconto = desconto;

        // Assert
        Assert.Equal(precoComDesconto, oferta.Preco);
    }
    [Fact]
    public void RetornaDescontoMaximoQuandoValorDescontoMaiorQuePreco()
    {
        // Arrange
        Rota rota = new Rota("origem-teste", "destino-teste");
        Periodo periodo = new Periodo(new DateTime(2025, 1, 1), new DateTime(2025, 1, 10));
        double precoOriginal = 100.0;
        double desconto = 120.0;
        double precoComDesconto = 30.0;
        OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

        // Act
        oferta.Desconto = desconto;

        // Assert
        Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
    }

    [Fact]
    public void RetornaPrecoOriginalQUsndoValorDescontoNegativo()
    {
        // Arrange
        Rota rota = new Rota("origem-teste", "destino-teste");
        Periodo periodo = new Periodo(new DateTime(2025, 1, 1), new DateTime(2025, 1, 10));
        double precoOriginal = 100.0;
        double desconto = -20.0;
        double precoComDesconto = precoOriginal;
        OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

        // Act
        oferta.Desconto = desconto;

        // Assert
        Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
    }
}
