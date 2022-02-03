using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Ping.Ip.Domain.Dto
{
    public class RetornaPingIpDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TipoDispositivo { get; set; }
        public bool Status { get; set; }
    }
}
