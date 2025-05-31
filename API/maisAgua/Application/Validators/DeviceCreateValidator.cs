using FluentValidation;
using maisAgua.Application.DTOs.DeviceDTO;

namespace maisAgua.Application.Validators
{
    public class DeviceCreateValidator : AbstractValidator<DeviceCreateDTO>
    {
        public DeviceCreateValidator() 
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage("Nome do dispositivo é obrigatório");

            RuleFor(d => d.InstallationDate)
                .NotEmpty()
                .WithMessage("Data de instalação não pode estar vazia")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Data de instalação não pode ser no futuro");
        }
    }
}
