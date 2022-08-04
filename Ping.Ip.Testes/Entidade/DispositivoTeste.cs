using Ping.Ip.Domain.Domain;
using Ping.Ip.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace Ping.Ip.Testes.Entidade
{ 
    public class DispositivoTeste
    {
        [Fact(DisplayName = "Passa dados válidos para método AdiconaDispositivo e retorna a mesma instância com os valores atualizados")]
        public void DispositivoComDadosValidos_AdicionaDispositivo_RetornaDispositivoValido()
        {
            // Arrange
            var dispositivo = new Dispositivo();

            // Act
            dispositivo = dispositivo.AdicionaDispositivo("Iphone", "Celular", "192.168.0.116");

            // Assert
            dispositivo.Nome.ShouldBe("Iphone");
            dispositivo.TipoDispositivo.ShouldBe("Celular");
            dispositivo.Ip.ShouldBe("192.168.0.116");
            dispositivo.Guid.ShouldBe(dispositivo.Guid);
        }

        [Fact(DisplayName = "Chama método AdicionaDispositivo sem passar IP e retorna Exception customizada ")]
        public void DispositivoComDadosInvalidos_AdicionaDispositivo_RetornaException()
        {
            // Arrange
            var dispositivo = new Dispositivo();

            // Act - Assert
            Should.Throw<IpInvalidoException>(() => dispositivo
                .AdicionaDispositivo("Iphone", "Celular", "")).Message
                .ShouldBe(new IpInvalidoException().Message);
        }

        [Fact(DisplayName = "Passa dados válidos para método AtualizaDispositivo e retorna a mesma instância com os valores atualizados")]
        public void DispositivoComDadosValidos_AlteraDispositivo_RetornaDispositivoValido()
        {
            // Arrange
            var dispositivo = new Dispositivo()
                .AdicionaDispositivo("Iphone", "Celular", "192.168.0.116");
           
            var id = dispositivo.Id;
            var guid = dispositivo.Guid;

            // Act
            dispositivo = dispositivo
                .AtualizaDispositivo("Iphone 13", "SmartPhone", "", dispositivo);

            // Assert
            dispositivo.Id.ShouldBe(id);
            dispositivo.Guid.ShouldBe(guid);
            dispositivo.Ip.ShouldBe("192.168.0.116");
            dispositivo.Nome.ShouldBe("Iphone 13");
            dispositivo.TipoDispositivo.ShouldBe("SmartPhone");
        }
    }
}
