using FluentValidation;
using maisAgua.Application.DTOs.Readings;

namespace maisAgua.Application.Validators.Readings
{
    public class ReadingUpdateValidator : AbstractValidator<ReadingUpdateDTO>
    {
        public ReadingUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID da leitura não pode estar vazio.")
                .GreaterThan(0).WithMessage("ID da leitura deve ser maior que 0.");
            RuleFor(x => x.LevelPct)
                .NotEmpty().WithMessage("Nível do reservatório não pode estar vazio.")
                .GreaterThanOrEqualTo(0).WithMessage("Nível do reservatório tem que ser maior ou igual a 0%")
                .LessThanOrEqualTo(100).WithMessage("Nível do reservatório tem que ser menor ou igual a 100%");
            RuleFor(x => x.TurbidityNtu)
                .NotEmpty().WithMessage("Turbidez não pode estar vazio.")
                .GreaterThanOrEqualTo(0).WithMessage("Turbidez tem que ser maior ou igual a 0 NTU")
                .LessThanOrEqualTo(500).WithMessage("Turbidez tem que ser menor ou igual a 500 NTU");
            RuleFor(x => x.PhLevel)
                .NotEmpty().WithMessage("Nível de PH não pode estar vazio.")
                .GreaterThanOrEqualTo(0).WithMessage("Nível de PH tem que ser maior ou igual a 0")
                .LessThanOrEqualTo(14).WithMessage("Nível de PH tem que ser menor ou igual a 14");
            RuleFor(x => x.ReadingDatetime)
                .NotEmpty().WithMessage("Data da leitura não pode estar vazia.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Data da leitura não pode ser no futuro.");
            RuleFor(x => x.IdDevice)
                .NotEmpty().WithMessage("ID do dispositivo não pode estar vazio.")
                .GreaterThan(0).WithMessage("ID do dispositivo deve ser maior que 0.");
        }
    }
}