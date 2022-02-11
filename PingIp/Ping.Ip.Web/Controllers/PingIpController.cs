using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ping.Ip.Domain.Dto;
using Ping.Ip.Domain.Service;
using System.Threading.Tasks;

namespace Ping.Ip.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PingIpController : ControllerBase
    {
        private readonly IDispositivoService _dispositivoService;

        public PingIpController(IDispositivoService dispositivoService)
        {
            _dispositivoService = dispositivoService;
        }

        [HttpPost]
        [Route("InserirDispositivo")]
        public async Task<ActionResult> InserirDispositivo([FromBody] DispositivoDto model)
        {
            try
            {
                var retorno = await _dispositivoService.InserirDispositivo(model);

                if(retorno.Ip != null)
                    return Ok(retorno);

                return Conflict(retorno);

            } catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ObterStatusDispositivos")]
        public async Task<ActionResult> ObterStatusDispositivos()
        {
            try
            {
                var retorno = await _dispositivoService.ObterStatusDispositivos();

                if(retorno.Count > 0)
                    return Ok(retorno);

                return BadRequest("Não existem dispositivos cadastrados.");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("AtualizarDispositivo")]
        public async Task<ActionResult> AtualizaDispositivo([FromBody] AtualizaDispositivoDto model)
        {
            try
            {
                var retorno = await _dispositivoService.AtualizarDispositivo(model);

                if (retorno)
                    return Ok(retorno);

                return BadRequest("Falha ao atualizar dispositivo");
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
