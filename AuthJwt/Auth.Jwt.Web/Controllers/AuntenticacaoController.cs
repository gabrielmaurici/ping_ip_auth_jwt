using Auth.Jwt.Domain;
using Auth.Jwt.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Jwt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuntenticacaoController : ControllerBase
    {
        private readonly IGerarToken _geraToken;

        public AuntenticacaoController(IGerarToken geraToken)
        {
            _geraToken = geraToken;
        }

        [HttpPost]
        [Route("loginAuth")]
        public ActionResult Autenticacao (User model)
        {
            try
            {
                var resultado = _geraToken.GerarToken(model);

                if(resultado.Sucesso)
                    return Ok(resultado);

                return BadRequest(resultado);
            } 
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
