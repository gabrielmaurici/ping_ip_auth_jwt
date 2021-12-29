using Auth.Jwt.App.Interface;
using Auth.Jwt.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var resultado = _geraToken.GerarToken(model);

            if(resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }
    }
}
