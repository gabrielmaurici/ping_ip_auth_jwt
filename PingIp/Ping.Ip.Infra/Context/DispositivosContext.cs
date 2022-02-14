using Ping.Ip.Domain.Domain;
using System.Data.Entity;

namespace Ping.Ip.Infra.Context
{
    public class DispositivosContext : DbContext
    {
        public DbSet<Dispositivo> Dispositivos { get; set; }

        public DispositivosContext() : base(@"Data Source=GABRIEL\SQLEXPRESS;Initial Catalog=Estudos;Integrated Security=True")
        {
        }
    }
}
