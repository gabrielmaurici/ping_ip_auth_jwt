using Ping.Ip.Domain.Domain;
using Ping.Ip.Domain.Exceptions;
using Xunit;

namespace Ping.Ip.Testes.Entidade
{ 
    public class DispositivoTeste
    {
        [Fact]
        public void DispositivoComDadosValidos_AdicionaDispositivo_RetornaDispositivoValido()
        {
            // Arrange
            var dispositivo = new Dispositivo();

            // Act
            dispositivo = dispositivo.AdicionaDispositivo("Iphone", "Celular", "192.168.0.116");

            // Assert
            Assert.Equal("Iphone", dispositivo.Nome);
            Assert.Equal("Celular", dispositivo.TipoDispositivo);
            Assert.Equal("192.168.0.116", dispositivo.Ip);
            Assert.Equal(dispositivo.Guid, dispositivo.Guid);
        }

        [Fact]
        public void DispositivoComDadosInvalidos_AdicionaDispositivo_RetornaException()
        {
            // Arrange
            var dispositivo = new Dispositivo();

            // Act - Assert
            var ex = Assert.Throws<IpInvalidoException>(() => dispositivo.AdicionaDispositivo("Iphone", "Celular", ""));
            Assert.Contains(new IpInvalidoException().Message, ex.Message);
        }

        [Fact]
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
            Assert.Equal(id, dispositivo.Id);
            Assert.Equal(guid, dispositivo.Guid);
            Assert.Equal("192.168.0.116", dispositivo.Ip);
            Assert.Equal("Iphone 13", dispositivo.Nome);
            Assert.Equal("SmartPhone", dispositivo.TipoDispositivo);
        }
    }
}
