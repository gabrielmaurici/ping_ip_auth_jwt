using Ping.Ip.Domain.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ping.Ip.Domain
{
    public interface IDispositivoRepository
    {
        Task InserirDispositivo(Dispositivo model);
        Task AtualizarDispositivo(Dispositivo model);
        Task DeletarDispositivo(Dispositivo model);
        Task<List<Dispositivo>> ListarDispositivos();
        Task<bool> VerificaDispositivoExistePorIp(string ip);
        Task<Dispositivo> ObterDispositivoPorId(int id);
    }
}
