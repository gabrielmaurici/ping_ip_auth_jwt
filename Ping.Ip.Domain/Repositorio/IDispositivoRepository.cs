using Ping.Ip.Domain.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ping.Ip.Domain
{
    public interface IDispositivoRepository
    {
        Task InserirDispositivo(Dispositivo model);
        Task<List<Dispositivo>> ListarDispositivos();
        Task<bool> ObterDispositivoPorIp(string ip);
    }
}
