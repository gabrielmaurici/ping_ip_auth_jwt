using GraphQL.Types;
using Ping.Ip.Domain.Domain;

namespace Ping.Ip.Domain.GraphQl.DispositivoType
{
    public class DispositivoType : ObjectGraphType<Dispositivo>
    {
        public DispositivoType()
        {
            Name = "Dispositivo";
            Description = "Dispositivo Type";

            Field(x => x.Id);
            Field(x => x.Guid);
            Field(x => x.Nome);
            Field(x => x.Ip);
            Field(x => x.TipoDispositivo);
        }
    }
}
