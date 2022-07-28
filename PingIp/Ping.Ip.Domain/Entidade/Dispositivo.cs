using System;

namespace Ping.Ip.Domain.Domain
{
    public class Dispositivo
    {
        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public string Nome { get; private set; }
        public string TipoDispositivo { get; private set; }
        public string Ip { get; private set; }

        public Dispositivo AdicionaDispositivo(string nome, string tipoDispositivo, string ip)
        {
            Guid = Guid.NewGuid();
            Nome = nome;
            TipoDispositivo = tipoDispositivo;
            Ip = ip;

            return this;
        }

        public Dispositivo AtualizaDispositivo(string nome, string tipoDispositivo, string ip, Dispositivo entidade)
        {
            Id = entidade.Id;
            Guid = entidade.Guid;
            Nome = !string.IsNullOrEmpty(nome) ? nome : entidade.Nome;
            TipoDispositivo = !string.IsNullOrEmpty(tipoDispositivo) ? tipoDispositivo : entidade.TipoDispositivo;
            Ip = !string.IsNullOrEmpty(ip) ? ip : entidade.Ip ;

            return this;
        }
    }
}
