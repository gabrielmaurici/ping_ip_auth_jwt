using Microsoft.EntityFrameworkCore;
using Ping.Ip.Domain.Domain;
using Ping.Ip.Domain.GraphQl.Repositorio;
using Ping.Ip.Infra.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ping.Ip.Infra.GraphQl.Repositorios
{
    public class DispositivoGraphQlRepositorio /*: IDispositivoGraphQlRepositorio*/
    {
        public  List<Dispositivo> GetAll()
        {
            using var _context = new DispositivosContext();
            var disp = _context.Dispositivos.ToList();
            return disp;
        }
    }
}
