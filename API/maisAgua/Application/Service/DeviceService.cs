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
    public readonly DeviceCreateValidator _validator;

    public DeviceService(DeviceRepository repository, DeviceCreateValidator validator) 
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<List<Device>> GetDevicesAsync() => await _repository.getAllAsync();

    public async Task<Device> CreateDeviceAsync(DeviceCreateDTO createDTO)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(createDTO);

        if (!validationResult.IsValid)
        {
            string error = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new DomainException("Falha ao validar dispositivo: " + error);
        }

        var device = new Device()
        {
            Name = createDTO.Name,
            InstallationDate = createDTO.InstallationDate
        };

        await _repository.AddAsync(device);

        return device;
    }
}
