using Ping.Ip.Domain.Dto;
using Ping.Ip.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ping.Ip.Domain.Service
{
    public interface IDispositivoService
    {
        Task<RetornoGenericoModel<bool>> InserirDispositivo(DispositivoDto model);
        Task<RetornoGenericoModel<bool>> AtualizarDispositivo(AtualizaDispositivoDto model);
        Task<RetornoGenericoModel<bool>> DeletarDispositivo(int id);
        Task<RetornoGenericoModel<List<RetornaPingIpDto>>> ObterStatusDispositivos();
    }
}
