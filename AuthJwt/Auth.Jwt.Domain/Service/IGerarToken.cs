using Auth.Jwt.Domain.Dto;

namespace Auth.Jwt.Domain.Service
{
    public interface IGerarToken
    {
        RetornoTokenDto GerarToken(User model);
    }
}
