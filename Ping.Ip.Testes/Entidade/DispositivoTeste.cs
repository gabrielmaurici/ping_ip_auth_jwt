using Ping.Ip.Domain.Domain;
using Xunit;

namespace Ping.Ip.Testes.Entidade
{ 
    public class DispositivoTeste
    {
        [Fact]
        public void Valida_Criacao_De_Um_Novo_Dispositivo()
        {
            // Arrange
            var dispositivo = new Dispositivo();

            // Act
            dispositivo = dispositivo.AdicionaDispositivo("Iphone", "Celular", "192.168.0.116");

            // Assert
            Assert.Equal("Iphone", dispositivo.Nome);
            Assert.Equal("Celular", dispositivo.TipoDispositivo);
            Assert.Equal("192.168.0.116", dispositivo.Ip);
            Assert.NotEqual(new Guid(), dispositivo.Guid);
        }

        [Fact]
        public void Valida_Alteracao_De_Um_Dispositivo()
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
            Assert.NotEqual("Iphone", dispositivo.Nome);
            Assert.NotEqual("Celular", dispositivo.TipoDispositivo);
        }
    }
}
