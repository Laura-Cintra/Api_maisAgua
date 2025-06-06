using FluentValidation;
using maisAgua.Application.DTOs.DeviceDTO;

namespace maisAgua.Application.Domain.Devices
{
    public class DeviceCreateValidator : AbstractValidator<DeviceCreateDTO>
    {
        public DeviceCreateValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Nome do dispositivo é obrigatório");

            RuleFor(x => x.InstallationDate)
                .NotEmpty()
                .WithMessage("Data de instalação não pode estar vazia")
                .LessThanOrEqualTo(DateTime.Now.AddSeconds(3)) // adicionado delay de 3 segundos para evitar conflito de relógio do servidor com o node-red
                .WithMessage("Data de instalação não pode ser no futuro");
        }
    }
}
