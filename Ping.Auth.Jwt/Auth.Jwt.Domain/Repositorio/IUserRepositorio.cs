using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Jwt.Domain.Repositorio
{
    public interface IUserRepositorio
    {
        User ValidaUsuario(User model);
    }
}
