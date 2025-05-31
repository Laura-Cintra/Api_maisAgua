using maisAgua.Domain.Exceptions;
using maisAgua.Presentation.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace maisAgua.Presentation.Filters;

public class DomainExceptionFilter : IExceptionFilter
{

    private readonly IWebHostEnvironment _enviroment;

    public DomainExceptionFilter(IWebHostEnvironment enviroment)
    {
        _enviroment = enviroment;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DomainException domainException)
        {
            // Aprender mais sobre Logger para adicionar aqui

            var errorResponse = new ErrorResponseDTO
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = domainException.Message
            };

            if (_enviroment.IsDevelopment())
            {
                errorResponse.StackTrace = context.Exception.StackTrace;
            }

            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = errorResponse.StatusCode,
            };
            context.ExceptionHandled = true; // Marca como 'lidado'
        }
    }
}
