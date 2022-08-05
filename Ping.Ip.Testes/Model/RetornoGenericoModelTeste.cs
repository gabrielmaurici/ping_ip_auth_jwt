using Ping.Ip.Domain.Model;
using Shouldly;
using Xunit;

namespace Ping.Ip.Testes.Model
{
    public class RetornoGenericoModelTeste
    {
        [Theory(DisplayName = "Testando construtor de sucesso")]
        [InlineData(10, null)]
        [InlineData(15, "Mensagem de teste ")]
        public void DadoSucesso_ChamaConstrutorSucesso_RetornaModeloSucesso(int model, string mensagemSucesso)
        {
            // Arrange
            var modelo = model;
            var mensagem = mensagemSucesso;

            // Act
            var resultado = new RetornoGenericoModel<int>(model, mensagem);

            // Assert
            resultado.Status.ShouldBe(true);
            resultado.Modelo.ShouldBe(modelo);
            resultado.Mensagem.ShouldBe(mensagem);
        }

        [Fact(DisplayName = "Testando construtor de erro com mensagem")]
        public void DadoErro_ChamaConstrutorErro_RetornaModeloErroComMensagem()
        {
            // Arrange
            var mensagem = "Mensagem de erro";

            // Act
            var resultado = new RetornoGenericoModel<int>(mensagem);

            // Assert
            resultado.Status.ShouldBe(false);
            resultado.Modelo.ShouldBe(0);
            resultado.Mensagem.ShouldBe(mensagem);
        }
    }
}
