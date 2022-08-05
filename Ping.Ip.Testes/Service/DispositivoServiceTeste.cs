using AutoFixture;
using Moq;
using Ping.Ip.App.Service;
using Ping.Ip.Domain;
using Ping.Ip.Domain.Domain;
using Ping.Ip.Domain.Dto;
using Shouldly;
using Xunit;

namespace Ping.Ip.Testes.Service
{
    public class DispositivoServiceTeste
    {
        [Fact(DisplayName = "Insere um dispositivo válido e retorna sucesso")]
        public async void DispositivoValido_ChamaServicoInserir_RetornaModeloGenericoDeSucesso()
        {
            // Arrange
            var dispositivoDto = new Fixture().Create<DispositivoDto>();

            var dispositivo = new Fixture().Create<Dispositivo>();

            var dispositivoRepositoryMock = new Mock<IDispositivoRepository>();

            var dispositivoService = new DispositivoService(dispositivoRepositoryMock.Object);

            // Act
            var resultado = await dispositivoService.InserirDispositivo(dispositivoDto);

            //Assert
            resultado.Status.ShouldBe(true);
            resultado.Modelo.ShouldBe(true);
            resultado.Mensagem.ShouldBe("Dispositivo cadastrado com sucesso.");

            dispositivoRepositoryMock.Verify(x => x.VerificaDispositivoExistePorIp(dispositivoDto.Ip), Times.Once);
            dispositivoRepositoryMock.Verify(x => x.InserirDispositivo(It.IsAny<Dispositivo>()), Times.Once);
        }

        [Fact(DisplayName = "Insere um dispositivo com IP já cadastrado e retorna erro com mensagem")]
        public async void DispositivoComIpJaCadastradp_ChamaServicoInserir_RetornaModeloGenericoDeErroComMensagem()
        {
            // Arrange
            var dispositivoDto = new Fixture().Create<DispositivoDto>();

            var dispositivo = new Fixture().Create<Dispositivo>();

            var dispositivoRepositoryMock = new Mock<IDispositivoRepository>();

            var dispositivoService = new DispositivoService(dispositivoRepositoryMock.Object);

            // Act
            dispositivoRepositoryMock.Setup(x => x.VerificaDispositivoExistePorIp(It.IsAny<string>()).Result)
                .Returns(true);

            var resultado = await dispositivoService.InserirDispositivo(dispositivoDto);

            //Assert
            resultado.Status.ShouldBe(false);
            resultado.Modelo.ShouldBe(false);
            resultado.Mensagem.ShouldBe("Esse IP já está cadastrado.");

            dispositivoRepositoryMock.Verify(x => x.VerificaDispositivoExistePorIp(It.IsAny<string>()), Times.Once);
            dispositivoRepositoryMock.Verify(x => x.InserirDispositivo(It.IsAny<Dispositivo>()), Times.Never);
        }
    }
}
