using FluentValidation;
using Ping.Ip.Domain.Dto;

namespace Ping.Ip.Domain.Validations
{
    public class AtualizaDispositivoDtoValidation : AbstractValidator<AtualizaDispositivoDto>
    {
        public AtualizaDispositivoDtoValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id é um campo obrigatório");
        }
    }
}
