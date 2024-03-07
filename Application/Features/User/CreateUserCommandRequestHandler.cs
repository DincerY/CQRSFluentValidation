using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.User;

public class CreateUserCommandRequestHandler : IRequestHandler<CreateUserCommandRequest>
{
    public Task Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}