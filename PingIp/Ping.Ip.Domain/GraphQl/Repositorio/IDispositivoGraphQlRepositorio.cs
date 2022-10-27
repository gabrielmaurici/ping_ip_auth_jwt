using Ping.Ip.Domain.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ping.Ip.Domain.GraphQl.Repositorio
{
    public interface IDispositivoGraphQlRepositorio
    {
        Task<List<Dispositivo>> GetAll();
    }
}
