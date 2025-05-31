using System;
using Bogus;
using JornadaMilhasV1.Gerencidor;
using JornadaMilhasV1.Modelos;
using Xunit.Sdk;

namespace JornadaMilhas.Test;

public class GerenciadorDeOfertasRecuperaMaiorDesconto
{
    [Fact]
    public void RetornaOfertaNulaQuandoListaEstaVazia()
    {
        // Arrange
        var lista = new List<OfertaViagem>();
        var gerenciador = new GerenciadorDeOfertas(lista);
        Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");

        // Act
        var oferta = gerenciador.RecuperaMaiorDesconto(filtro);

        // Assert
        Assert.Null(oferta);
    }

    [Fact]
    //destino sao paulo, desconto 40 preco 80
    public void RetornaOfertaEspecificaQuandoDestinoSaoPauloEDescontoQuarenta()
    {
        // Arrange
        var fakerPeriodo = new Faker<Periodo>()
        .CustomInstantiator(f =>
        {
            return new Periodo(f.Date.Soon(), f.Date.Soon().AddDays(10));
        });
        var rota = new Rota("Curitiba", "São Paulo");
        var fakerOferta = new Faker<OfertaViagem>()
        .CustomInstantiator(f => new OfertaViagem(rota, fakerPeriodo.Generate(), 100 * f.Random.Number(1, 100)))
        .RuleFor(o => o.Desconto, f => 40)
        .RuleFor(o => o.Ativa, f => true);
        var ofertaEscolhida = new OfertaViagem(rota, fakerPeriodo.Generate(), 80)
        {
            Desconto = 40,
            Ativa = true,
        };

        var ofertaInativa = new OfertaViagem(rota, fakerPeriodo.Generate(), 70)
        {
            Desconto = 40,
            Ativa = false,
        };

        var lista = fakerOferta.Generate(200).ToList();
        lista.Add(ofertaEscolhida);
        lista.Add(ofertaInativa);

        var gerenciador = new GerenciadorDeOfertas(lista);
        Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");
        var precoEsperado = 40.0;

        // Act
        var oferta = gerenciador.RecuperaMaiorDesconto(filtro);

        // Assert
        Assert.NotNull(oferta);
        Assert.Equal(precoEsperado, oferta.Preco, 0.0001);
    }
}
