using Auth.Jwt.App.Interface;
using Auth.Jwt.Domain;
using Auth.Jwt.Domain.Dto;
using Auth.Jwt.Domain.Repositorio;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Jwt.App.Service
{
    public class GeraToken : IGerarToken
    {
        private readonly IUserRepositorio _userRepositorio;
        private readonly IConfiguration _configuration;

        public GeraToken(IUserRepositorio userRepositorio, IConfiguration configuration)
        {
            _userRepositorio = userRepositorio;
            _configuration = configuration;
        }

        public RetornoTokenDto GerarToken(User model)
        {
            var user = _userRepositorio.ValidaUsuario(model);

            if(user.UserName != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["ChavePrivada"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new RetornoTokenDto
                {
                    Sucesso = true,
                    Token = tokenHandler.WriteToken(token),
                    Mensagem = "Autenticação realizada com sucesso."
                };
            }

            return new RetornoTokenDto
            {
                Sucesso = false,
                Token = "",
                Mensagem = "Autenticação negada."
            };
        }
    }
}
