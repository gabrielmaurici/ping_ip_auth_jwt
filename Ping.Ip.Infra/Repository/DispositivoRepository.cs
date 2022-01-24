using Ping.Ip.Domain;
using Ping.Ip.Domain.Domain;
using Ping.Ip.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Ping.Ip.Infra.Repository
{
    public class DispositivoRepository : IDispositivoRepository
    { 
        public async Task InserirDispositivo(Dispositivo model) {
            using (var context = new DispositivosContext())
            {
                context.Dispositivos.Add(model);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Dispositivo>> ListarDispositivos()
        {
            using (var context = new DispositivosContext())
            {
                return await context.Dispositivos.ToListAsync();
            }
        }

        public async Task<bool> ObterDispositivoPorIp(string ip)
        {
            using (var context = new DispositivosContext())
            {
                var dispositivo = await context.Dispositivos.FirstOrDefaultAsync(x => x.Ip.Equals(ip));

                return dispositivo == null;
            }
        }
    }
}
