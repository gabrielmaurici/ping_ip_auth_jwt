using Auth.Jwt.Domain;
using Auth.Jwt.Domain.Repositorio;
using System;
using System.Threading.Tasks;

namespace Auth.Jwt.Infra
{
    public class UserRepositorio : IUserRepositorio
    {
        public User ValidaUsuario(User model)
        {
            if (model.UserName == "gabriel" && model.Senha == "123")
            {
                return new User
                {
                    UserName = "gabriel",
                    Senha = "123"
                };
            }
            return new User();
        }
    }
}
