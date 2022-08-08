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

            var dispositivoRepositoryMock = new Mock<IDispositivoRepository>();

            var dispositivoService = new DispositivoService(dispositivoRepositoryMock.Object);

            // Act
            var resultado = await dispositivoService.InserirDispositivo(dispositivoDto);

            //Assert
            resultado.Status.ShouldBe(true);
            resultado.Modelo.ShouldBe(true);
            resultado.Mensagem.ShouldBe("Dispositivo cadastrado com sucesso.");

            dispositivoRepositoryMock.Verify(x => x.VerificaDispositivoExistePorIp(It.IsAny<string>()), Times.Once);
            dispositivoRepositoryMock.Verify(x => x.InserirDispositivo(It.IsAny<Dispositivo>()), Times.Once);
        }

        [Fact(DisplayName = "Insere um dispositivo com IP já cadastrado e retorna erro com mensagem")]
        public async void DispositivoComIpJaCadastrado_ChamaServicoInserir_RetornaModeloGenericoDeErroComMensagem()
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

        [Fact(DisplayName = "Atualiza um dispositivo e retorna sucesso")]
        public async void DadoDispositivoExistente_ChamaServicoAtualizar_RetornaModeloGenericoDeSucesso()
        {
            // Arrange
            var atualizaDispositivoDto = new Fixture().Create<AtualizaDispositivoDto>();

            var dispositivoRepositoryMock = new Mock<IDispositivoRepository>();

            var dispositivoService = new DispositivoService(dispositivoRepositoryMock.Object);

            // Act
            dispositivoRepositoryMock.Setup(x => x.ObterDispositivoPorId(It.IsAny<int>()).Result)
                .Returns(new Dispositivo());

            var resultado = await dispositivoService.AtualizarDispositivo(atualizaDispositivoDto);

            // Assert
            resultado.Status.ShouldBe(true);
            resultado.Modelo.ShouldBe(true);
            resultado.Mensagem.ShouldBe("Dispositivo alterado com sucesso.");

            dispositivoRepositoryMock.Verify(x => x.ObterDispositivoPorId(It.IsAny<int>()), Times.Once);
            dispositivoRepositoryMock.Verify(x => x.AtualizarDispositivo(It.IsAny<Dispositivo>()), Times.Once);
        }

        [Fact(DisplayName = "Atualiza um dispositivo não existente e retorna erro com mensagem")]
        public async void DadoDispositivoNaoExistente_ChamaServicoAtualizar_RetornaModeloGenericoDeErroComMensagem()
        {
            // Arrange
            var atualizaDispositivoDto = new Fixture().Create<AtualizaDispositivoDto>();

            var dispositivoRepositoryMock = new Mock<IDispositivoRepository>();

            var dispositivoService = new DispositivoService(dispositivoRepositoryMock.Object);

            // Act
            var resultado = await dispositivoService.AtualizarDispositivo(atualizaDispositivoDto);

            // Assert
            resultado.Status.ShouldBe(false);
            resultado.Modelo.ShouldBe(false);
            resultado.Mensagem.ShouldBe("Dispositivo não econtrado.");

            dispositivoRepositoryMock.Verify(x => x.ObterDispositivoPorId(It.IsAny<int>()), Times.Once);
            dispositivoRepositoryMock.Verify(x => x.AtualizarDispositivo(It.IsAny<Dispositivo>()), Times.Never);
        }

        [Fact(DisplayName = "Obtém listagem de dispositivos com stauts do ping realizado")]
        public async void DispositivosCadastrados_ChamaServicoObterStatusDispositivos_RetornaModeloGenericoDeSucessoComDto()
        {
            // Arrange
            var dispositivoRepositoryMock = new Mock<IDispositivoRepository>();

            var dispositivoService = new DispositivoService(dispositivoRepositoryMock.Object);
            var dispositivo = new Dispositivo()
                .AdicionaDispositivo("Dispositivo 1", "Teste", "120.190.0.116");

            List<RetornaPingIpDto> retornoPingIpDto = new()
            {
               new RetornaPingIpDto
               {
                   Id = dispositivo.Id,
                   Nome = dispositivo.Nome,
                   TipoDispositivo = dispositivo.TipoDispositivo,
                   Ip = dispositivo.Ip,
                   Status = false
               }
            }; 

            //Act
            dispositivoRepositoryMock.Setup(x => x.ListarDispositivos().Result)
                .Returns(new List<Dispositivo> { dispositivo });

            var resultado = await dispositivoService.ObterStatusDispositivos();

            // Assert
            resultado.Status.ShouldBe(true);
            resultado.Modelo[0].Id.ShouldBe(retornoPingIpDto[0].Id);
            resultado.Modelo[0].Nome.ShouldBe(retornoPingIpDto[0].Nome);
            resultado.Modelo[0].TipoDispositivo.ShouldBe(retornoPingIpDto[0].TipoDispositivo);
            resultado.Modelo[0].Ip.ShouldBe(retornoPingIpDto[0].Ip);
            resultado.Modelo[0].Status.ShouldBe(retornoPingIpDto[0].Status);
            resultado.Mensagem.ShouldBe(null);

            dispositivoRepositoryMock.Verify(x => x.ListarDispositivos(), Times.Once);
        }

        [Fact(DisplayName = "Chama serviço de listagem de dispositivos com IP sem ter dispositivos cadastrados e retorna Erro com mensagem")]
        public async void SemDispositivosCadastrados_ChamaServicoObterStatusDispositivos_RetornaModeloGenericoDeErroComMensagem()
        {
            // Arrange
            var dispositivoRepositoryMock = new Mock<IDispositivoRepository>();

            var dispositivoService = new DispositivoService(dispositivoRepositoryMock.Object);

            //Act
            dispositivoRepositoryMock.Setup(x => x.ListarDispositivos().Result)
                .Returns(new List<Dispositivo>());

            var resultado = await dispositivoService.ObterStatusDispositivos();

            // Assert
            resultado.Status.ShouldBe(false);
            resultado.Modelo.ShouldBe(null);
            resultado.Mensagem.ShouldBe("Nenhum dispositivo cadastrado");

            dispositivoRepositoryMock.Verify(x => x.ListarDispositivos(), Times.Once);
        }

        [Fact(DisplayName = "Chama serviço para deletar dispositivo com Id de um dispositivo existente e retorna sucesso")]
        public async void PassadoIdDipositivoExistente_ChamaServicoDeletarDispositivo_RetornaModeloGenericoDeSucesso()
        {
            // Arrange
            var dispositivoRepositoryMock = new Mock<IDispositivoRepository>();

            var dispositivoService = new DispositivoService(dispositivoRepositoryMock.Object);

            //Act
            dispositivoRepositoryMock.Setup(x => x.ObterDispositivoPorId(It.IsAny<int>()).Result)
                .Returns(new Dispositivo());

            var resultado = await dispositivoService.DeletarDispositivo(1);

            // Assert
            resultado.Status.ShouldBe(true);
            resultado.Modelo.ShouldBe(true);
            resultado.Mensagem.ShouldBe("Dispositivo deletado com sucesso.");

            dispositivoRepositoryMock.Verify(x => x.ObterDispositivoPorId(It.IsAny<int>()), Times.Once);
            dispositivoRepositoryMock.Verify(x => x.DeletarDispositivo(It.IsAny<Dispositivo>()), Times.Once);
        }

        [Fact(DisplayName = "Chama serviço para deletar dispositivo com Id de um dispositivo inexistente e retorna erro com mensagem")]
        public async void PassadoIdDipositivoInexistente_ChamaServicoDeletarDispositivo_RetornaModeloGenericoDeErroComMensagem()
        {
            // Arrange
            var dispositivoRepositoryMock = new Mock<IDispositivoRepository>();

            var dispositivoService = new DispositivoService(dispositivoRepositoryMock.Object);

            //Act
            var resultado = await dispositivoService.DeletarDispositivo(1);

            // Assert
            resultado.Status.ShouldBe(false);
            resultado.Modelo.ShouldBe(false);
            resultado.Mensagem.ShouldBe("Dispositivo não encontrado.");

            dispositivoRepositoryMock.Verify(x => x.ObterDispositivoPorId(It.IsAny<int>()), Times.Once);
            dispositivoRepositoryMock.Verify(x => x.DeletarDispositivo(It.IsAny<Dispositivo>()), Times.Never);
        }
    }
}