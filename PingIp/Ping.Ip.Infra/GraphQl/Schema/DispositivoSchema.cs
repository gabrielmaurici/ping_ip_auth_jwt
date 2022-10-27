using Microsoft.Extensions.DependencyInjection;
using Ping.Ip.Infra.GraphQl.Queries;
using System;

namespace Ping.Ip.Infra.GraphQl.Schema
{
    public class DispositivoSchema : GraphQL.Types.Schema
    {
        public DispositivoSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<DispositivoQuery>();
        }
    }
}
