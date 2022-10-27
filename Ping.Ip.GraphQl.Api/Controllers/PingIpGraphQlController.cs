using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Ping.Ip.GraphQl.Api.Core;
using Ping.Ip.Infra.GraphQl.Schema;

namespace Ping.Ip.GraphQl.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingIpGraphQlController : ControllerBase
    {
        private readonly DispositivoSchema _dispositivoSchema;

        public PingIpGraphQlController(DispositivoSchema dispositivoSchema)
        {
            _dispositivoSchema = dispositivoSchema;
        }

        [HttpPost("dispositivos")]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            var schema = _dispositivoSchema;

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Variables = (Inputs)query.Variables;
            }).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}