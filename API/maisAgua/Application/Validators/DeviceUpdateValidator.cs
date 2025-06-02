using FluentValidation;
using maisAgua.Application.DTOs.DeviceDTO;

namespace maisAgua.Application.Validators
{
    public class DeviceUpdateValidator : AbstractValidator<DeviceUpdateDTO>
    {
        public DeviceUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Nome do dispositivo não pode estar vazio");

            RuleFor(x => x.InstallationDate)
                .NotEmpty()
                .WithMessage("Data de instalação não pode estar vazia")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Data de instalação não pode ser no futuro.");
                
        }
    }
}