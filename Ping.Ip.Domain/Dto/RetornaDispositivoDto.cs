using System;

namespace Ping.Ip.Domain.Dto
{
    public class RetornaDispositivoDto
    {
        public Guid Guid { get; set; }
        public string Nome { get; set; }
        public string TipoDispositivo { get; set; }
        public string Ip { get; set; }
        public string Mensagem { get; set; }
    }
}
