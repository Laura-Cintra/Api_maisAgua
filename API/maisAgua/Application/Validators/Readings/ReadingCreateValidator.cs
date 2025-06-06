using FluentValidation;
using maisAgua.Application.DTOs.Readings;

namespace maisAgua.Application.Validators.Readings
{
    public class ReadingCreateValidator : AbstractValidator<ReadingCreateDTO>
    {
        public ReadingCreateValidator()
        {

            RuleFor(x => x.LevelPct) 
                
                .GreaterThanOrEqualTo(0).WithMessage("Nível do reservátorio tem que ser maior ou igual a 0%")
                .LessThanOrEqualTo(100).WithMessage("Nível do reservátorio tem que ser menor ou igual a 100%");

            RuleFor(x => x.TurbidityNtu)
                .GreaterThanOrEqualTo(0).WithMessage("Turbidez tem que ser maior ou igual a 0 NTU")
                .LessThanOrEqualTo(500).WithMessage("Turbidez tem que ser menor ou igual a 500 NTU");

            RuleFor(x => x.PhLevel)
                .GreaterThanOrEqualTo(0).WithMessage("Nível de PH tem que ser maior ou igual a 0")
                .LessThanOrEqualTo(14).WithMessage("Nível de PH tem que ser menor ou igual a 14");

            RuleFor(x => x.ReadingDatetime)
                .NotEmpty().WithMessage("Data da leitura não pode estar vazia.")
                .LessThanOrEqualTo(DateTime.Now.AddSeconds(3)).WithMessage("Data da leitura não pode ser no futuro."); // adicionado delay de 3 segundos para evitar conflito de relógio do servidor com o node-red

            RuleFor(x => x.IdDevice)
                .GreaterThan(0).WithMessage("ID do dispositivo deve ser maior que 0.");
        }
    }
}