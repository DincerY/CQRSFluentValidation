using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCuttingConcerns.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(Error[] errors)
    {
        Errors = errors;
    }

    public Error[] Errors { get; set; }
}