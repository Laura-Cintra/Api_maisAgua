using FluentValidation;
using FluentValidation.Results;
using maisAgua.Application.DTOs.DeviceDTO;
using maisAgua.Application.Repository;
using maisAgua.Application.Validators;
using maisAgua.Domain.Device;
using maisAgua.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

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

    public async Task<List<Device>> GetDevicesAsync() => await _repository.GetAllAsync();

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

    public async Task<Device> GetByIdAsync(int id)
    {
        var device = await _repository.GetByIdAsync(id) ?? throw new DeviceNotFoundException(id);

        return device;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var device = await _repository.GetByIdAsync(id) ?? throw new DeviceNotFoundException(id);
        await _repository.DeleteAsync(device);
        return true;
    }

    public async Task<Device> UpdateAsync(int id, DeviceUpdateDTO updateDTO)
    {
        var device = await _repository.GetByIdAsync(id) ?? throw new DeviceNotFoundException(id);

        var validationResult =  await  _updateValidator.ValidateAsync(updateDTO);

        if (!validationResult.IsValid)
        {
            var errorMsg = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new DomainException("Falha ao atualizar dispositivo: " + errorMsg);
        }

        device.Name = updateDTO.Name ?? device.Name;
        device.InstallationDate = updateDTO.InstallationDate.HasValue ? (DateTime)updateDTO.InstallationDate : device.InstallationDate;

        return await _repository.UpdateAsync(device);
    }
}

