using FluentValidation.Results;
using maisAgua.Application.Domain.Devices;
using maisAgua.Application.DTOs.DeviceDTO;
using maisAgua.Application.DTOs.Devices;
using maisAgua.Application.DTOs.Readings;
using maisAgua.Application.Repository;
using maisAgua.Application.Validators.Device;
using maisAgua.Domain.Exceptions;
using maisAgua.Domain.Persistence.Devices;

namespace maisAgua.Application.Service;

public class DeviceService
{
    public readonly DeviceRepository _repository;
    public readonly DeviceCreateValidator _createValidator;
    public readonly DeviceUpdateValidator _updateValidator;

    public DeviceService(DeviceRepository repository, DeviceCreateValidator createValidator, DeviceUpdateValidator updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<List<DevicesReadDTO>> GetAllDevicesAsync()
    {
        var devices = await _repository.GetAllAsync();
        var deviceReadDTO = devices.OrderByDescending(x => x.Id).Select(x => new DevicesReadDTO()
        {
            Id = x.Id,
            Name = x.Name,
            InstallationDate = x.InstallationDate,
            Readings = x.Readings.OrderByDescending(z => z.Id).Take(5).Select(z => new ReadingBasicInfoDTO()
            {
                Id = z.Id,
                ReadingDateTime = z.ReadingDatetime,
            }).ToList(),
        }).ToList();

        return deviceReadDTO;
    }

    public async Task<DeviceReadDTO> CreateDeviceAsync(DeviceCreateDTO createDTO)
    {
        ValidationResult validationResult = await _createValidator.ValidateAsync(createDTO);

        if (!validationResult.IsValid)
        {
            string error = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new DomainException(error);
        }

        var device = new Device()
        {
            Name = createDTO.Name,
            InstallationDate = createDTO.InstallationDate
        };

        device = await _repository.AddAsync(device);

        var deviceReadDTO = new DeviceReadDTO()
        {
            Id = device.Id,
            InstallationDate = device.InstallationDate,
            Name = device.Name,
            Readings = new List<ReadingReadDTO>()
        };

        return deviceReadDTO;
    }

    public async Task<DeviceReadDTO> GetDeviceByIdAsync(int id)
    {

        var device = await _repository.GetByIdAsync(id) ?? throw new DeviceNotFoundException(id);
        var deviceReadDTO = new DeviceReadDTO
        {
            Id = device.Id,
            Name = device.Name,
            InstallationDate = device.InstallationDate,
            Readings = device.Readings.OrderByDescending(x => x.Id).Take(10).Select(x => new ReadingReadDTO()
            {
                Id = x.Id,
                LevelPct = x.LevelPct,
                TurbidityNtu = x.TurbidityNtu,
                PhLevel = x.PhLevel,
                ReadingDatetime = x.ReadingDatetime,
                IdDevice = x.IdDevice,
            }).ToList(),
        };
        return deviceReadDTO;
    }

    public async Task<bool> DeleteDeviceAsync(int id)
    {
        var device = await _repository.GetByIdAsync(id) ?? throw new DeviceNotFoundException(id);
        await _repository.DeleteAsync(device);
        return true;
    }

    public async Task<DeviceReadDTO> UpdateDeviceAsync(int id, DeviceUpdateDTO updateDTO)
    {
        var device = await _repository.GetByIdAsync(id) ?? throw new DeviceNotFoundException(id);

        var validationResult = await _updateValidator.ValidateAsync(updateDTO);

        if (!validationResult.IsValid)
        {
            var errorMsg = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new DomainException("Erro ao atualizar dispositivo: " + errorMsg);
        }

        // Aprender mais sobre auto mapper para evitar essa verificação manual
        if (updateDTO.Name != null)
            device.Name = updateDTO.Name;
        if (updateDTO.InstallationDate != null)
            device.InstallationDate = (DateTime)updateDTO.InstallationDate;
        
        
        device = await _repository.UpdateAsync(device);

        var deviceReadDTO = new DeviceReadDTO()
        {
            Id = device.Id,
            Name = device.Name,
            InstallationDate = device.InstallationDate,
            Readings = device.Readings.OrderByDescending(x => x.Id).Take(10).Select(x => new ReadingReadDTO()
            {
                Id = x.Id,
                LevelPct = x.LevelPct,
                TurbidityNtu = x.TurbidityNtu,
                PhLevel = x.PhLevel,
                ReadingDatetime = x.ReadingDatetime,
                IdDevice = x.IdDevice
            }).ToList(),
        };
        return deviceReadDTO;
    }
}

