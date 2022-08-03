using System;

namespace Ping.Ip.Domain.Exceptions
{
    public class IpInvalidoException : ArgumentException
    {
        public IpInvalidoException() : base("IP inválido, dispositivo não pode ter IP nulo ou vázio")
        { }
    }
}
