using Ping.Ip.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ping.Ip.Domain
{
    public interface IDispositivoRepository
    {
        Task InserirDispositivo(Dispositivo model);
        Task<List<Dispositivo>> ListarDispositivos();
    }
}
