using GraphQL.Types;
using Ping.Ip.Domain.GraphQl.DispositivoType;
using Ping.Ip.Domain.GraphQl.Repositorio;
using Ping.Ip.Infra.GraphQl.Repositorios;

namespace Ping.Ip.Infra.GraphQl.Queries
{
    public class DispositivoQuery : ObjectGraphType
    {
        //private readonly IDispositivoGraphQlRepositorio _dispositivoGraphQlRepositorio;
        public DispositivoQuery()
        {
            var _dispositivoGraphQlRepositorio = new DispositivoGraphQlRepositorio();

            Field<ListGraphType<DispositivoType>>("dispositivos")
                .Resolve(context => _dispositivoGraphQlRepositorio.GetAll());
        }
    }
}
