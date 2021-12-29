using Auth.Jwt.Domain;
using Auth.Jwt.Domain.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Jwt.App.Interface
{
    public interface IGerarToken
    {
        RetornoTokenDto GerarToken(User model);
    }
}
