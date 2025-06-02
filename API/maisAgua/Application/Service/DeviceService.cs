using FluentValidation.Results;
using maisAgua.Application.Domain.Devices;
using maisAgua.Application.DTOs.DeviceDTO;
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

    public async Task<List<Device>> GetAllDevicesAsync()
    {

        return await _repository.GetAllAsync();


    }

    public async Task<Device> CreateDeviceAsync(DeviceCreateDTO createDTO)
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

        await _repository.AddAsync(device);

        return device;

    }

    public async Task<Device> GetDeviceByIdAsync(int id)
    {

        var device = await _repository.GetByIdAsync(id) ?? throw new DeviceNotFoundException(id);
        return device;


    }

    public async Task<bool> DeleteDeviceAsync(int id)
    {
        var device = await _repository.GetByIdAsync(id) ?? throw new DeviceNotFoundException(id);
        await _repository.DeleteAsync(device);
        return true;
    }

    public async Task<Device> UpdateDeviceAsync(int id, DeviceUpdateDTO updateDTO)
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

        return await _repository.UpdateAsync(device);
    }
}

