using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ping.Ip.Domain.Dto;
using Ping.Ip.Domain.Service;
using System;
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
                if(retorno.Status)
                    return Ok(retorno.Mensagem);

                return Conflict(retorno.Mensagem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("AtualizarDispositivo")]
        public async Task<ActionResult> AtualizaDispositivo([FromBody] AtualizaDispositivoDto model)
        {
            try
            {
                var retorno = await _dispositivoService.AtualizarDispositivo(model);

                if (retorno.Status)
                    return Ok(retorno.Mensagem);

                return BadRequest(retorno.Mensagem);
            }
            catch
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

                if(retorno.Status)
                    return Ok(retorno.Modelo);

                return BadRequest(retorno.Mensagem);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("DeletarDispositivo/{id}")]
        public async Task<ActionResult> DeletarDispositivo(int id)
        {
            try
            {
                var retorno = await _dispositivoService.DeletarDispositivo(id);

                if (retorno.Status)
                    return Ok(retorno.Mensagem);

                return BadRequest(retorno.Mensagem);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
