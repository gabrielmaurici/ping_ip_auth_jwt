using FluentValidation;
using Ping.Ip.Domain.Dto;
using System.Text.RegularExpressions;

namespace Ping.Ip.Domain.Validations
{
    public class DispositivoDtoValidation : AbstractValidator<DispositivoDto>
    {
        public DispositivoDtoValidation()
        {
            RuleFor(x => x.Ip)
                .NotEmpty().WithMessage("IP é um campo obrigatório")
                .Must(ValidaIp).WithMessage("Formato de IP incorreto, exemplo de IP correto: 192.168.0.116");
        }

        private bool ValidaIp(string ip)
        {
            var ipFormato = "[0-9]{3}\\.?[0-9]{3}\\.?[0-9]{1}\\.?[0-9]{3}";
            return Regex.Match(ip, ipFormato).Success;
        }
    }
}
