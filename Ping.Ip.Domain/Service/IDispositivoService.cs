using Ping.Ip.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ping.Ip.Domain.Service
{
    public interface IDispositivoService
    {
        Task<RetornaDispositivoDto> InserirDispositivo(DispositivoDto model);
        Task<List<RetornaPingIpDto>> ObterStatusDispositivos();
        Task<bool> AtualizarDispositivo(AtualizaDispositivoDto model);
    }
}
