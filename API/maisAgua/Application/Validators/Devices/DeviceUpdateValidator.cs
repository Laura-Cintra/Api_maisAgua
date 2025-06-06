using FluentValidation;
using maisAgua.Application.DTOs.DeviceDTO;

namespace maisAgua.Application.Validators.Device
{
    public class DeviceUpdateValidator : AbstractValidator<DeviceUpdateDTO>
    {
        public DeviceUpdateValidator()
        {

            RuleFor(x => x.InstallationDate)
                .LessThanOrEqualTo(DateTime.Now.AddSeconds(3)) // adicionado delay de 3 segundos para evitar conflito de relógio do servidor com o node-red
                .WithMessage("Data de instalação não pode ser no futuro.");
        }
    }
}