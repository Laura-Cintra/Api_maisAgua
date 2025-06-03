using FluentValidation.Results;
using maisAgua.Application.DTOs.Readings;
using maisAgua.Application.Repository;
using maisAgua.Application.Validators.Readings;
using maisAgua.Domain.Exceptions;
using maisAgua.Domain.Persistence.Readings;


namespace maisAgua.Application.Service
{
    public class ReadingService
    {
        private readonly ReadingsRepository _repository;
        private readonly ReadingCreateValidator _createValidator;
        private readonly ReadingUpdateValidator _updateValidator;

        public ReadingService(ReadingsRepository readingsRepository, ReadingCreateValidator createValidator, ReadingUpdateValidator updateValidator)
        {
            _repository = readingsRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<List<ReadingReadDTO>> GetAllReadingsAsync()
        {
            var devices = await _repository.GetAllAsync();

            var readingReadDTO = devices.OrderByDescending(x => x.Id).Select(x => new ReadingReadDTO()
            {
                Id = x.Id,
                LevelPct = x.LevelPct,
                TurbidityNtu = x.TurbidityNtu,
                PhLevel = x.PhLevel,
                ReadingDatetime = x.ReadingDatetime,
                IdDevice = x.IdDevice
            }).ToList();

            return readingReadDTO;
        }

        public async Task<ReadingReadDTO> AddReadingAsync(ReadingCreateDTO createDTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(createDTO);

            if (!validationResult.IsValid)
            {
                string error = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new DomainException(error);
            }

            var reading = new Reading()
            {
                LevelPct = createDTO.LevelPct,
                TurbidityNtu = createDTO.TurbidityNtu,
                PhLevel = createDTO.PhLevel,
                ReadingDatetime = createDTO.ReadingDatetime,
                IdDevice = createDTO.IdDevice
            };

            reading = await _repository.AddAsync(reading);

            var readingReadDTO = new ReadingReadDTO()
            {
                Id = reading.Id,
                LevelPct = reading.LevelPct,
                TurbidityNtu = reading.TurbidityNtu,
                PhLevel = reading.PhLevel,
                ReadingDatetime = reading.ReadingDatetime,
                IdDevice = reading.IdDevice
            };

            return readingReadDTO;
        }


        public async Task<ReadingReadDTO> UpdateReadingAsync(int id, ReadingUpdateDTO updateDTO)
        {
            var reading = await _repository.GetByIdAsync(id) ?? throw new ReadingNotFoundException(id);

            ValidationResult validationResult = await _updateValidator.ValidateAsync(updateDTO);

            if (!validationResult.IsValid)
            {
                string error = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new DomainException(error);
            }

            // Aprender mais sobre auto mapper para evitar essa verificação manual
            if (updateDTO.LevelPct != null)
                reading.LevelPct = updateDTO.LevelPct.Value;
            if (updateDTO.TurbidityNtu != null)
                reading.TurbidityNtu = updateDTO.TurbidityNtu.Value;
            if (updateDTO.PhLevel != null)
                reading.PhLevel = updateDTO.PhLevel.Value;
            if (updateDTO.ReadingDatetime != null)
                reading.ReadingDatetime = updateDTO.ReadingDatetime.Value;
            if (updateDTO.IdDevice != null)
                reading.IdDevice = updateDTO.IdDevice.Value;

            reading = await _repository.UpdateAsync(reading);

            var readingReadDTO = new ReadingReadDTO()
            {
                Id = reading.Id,
                LevelPct = reading.LevelPct,
                TurbidityNtu = reading.TurbidityNtu,
                PhLevel = reading.PhLevel,
                ReadingDatetime = reading.ReadingDatetime,
                IdDevice = reading.IdDevice
            };

            return readingReadDTO;
        }



        public async Task<bool> DeleteReadingAsync(int id)
        {
            var reading = await _repository.GetByIdAsync(id) ?? throw new ReadingNotFoundException(id);
            return await _repository.DeleteAsync(reading);
        }

        public async Task<ReadingReadDTO> GetReadingByIdAsync(int id)
        {
            var reading = await _repository.GetByIdAsync(id) ?? throw new ReadingNotFoundException(id);

            var readingReadDTO = new ReadingReadDTO()
            {
                Id = reading.Id,
                LevelPct = reading.LevelPct,
                TurbidityNtu = reading.TurbidityNtu,
                PhLevel = reading.PhLevel,
                ReadingDatetime = reading.ReadingDatetime,
                IdDevice = reading.IdDevice
            };
            return readingReadDTO;
        }
    }
}
