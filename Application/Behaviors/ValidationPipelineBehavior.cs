using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossCuttingConcerns.Exceptions;
using FluentValidation;
using MediatR;
using ValidationException = CrossCuttingConcerns.Exceptions.ValidationException;

namespace Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TReponse> : IPipelineBehavior<TRequest, TReponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TReponse> Handle(TRequest request, RequestHandlerDelegate<TReponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var errors = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .Select(failure => new Error()
            {
                PropertyName = failure.PropertyName,
                ErrorMessage = failure.ErrorMessage
            })
            .ToArray();

        if (errors.Any())
        {
            throw new ValidationException(errors);
        }

        return await next();
    }

}