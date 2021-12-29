using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Jwt.Domain.Dto
{
    public class RetornoTokenDto
    {
        public bool Sucesso { get; set; }
        public string Token { get; set; }
        public string Mensagem { get; set; }
    }
}
