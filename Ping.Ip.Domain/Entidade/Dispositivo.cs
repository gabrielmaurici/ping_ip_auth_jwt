using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ping.Ip.Domain.Domain
{
    public class Dispositivo
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Nome { get; set; }
        public string Ip { get; set; }
    }
}
