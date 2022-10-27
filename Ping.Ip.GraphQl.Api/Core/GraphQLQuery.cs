using Newtonsoft.Json.Linq;

namespace Ping.Ip.GraphQl.Api.Core
{
    public class GraphQLQuery
    {
        public string? OperationName { get; set; }
        public string? NamedQuery { get; set; }
        public string? Query { get; set; }
        public object Variables { get; set; } = null!;
    }
}
