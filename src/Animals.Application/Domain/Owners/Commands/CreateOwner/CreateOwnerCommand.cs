using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Animals.Application.Domain.Owners.Commands.CreateOwner
{

    public record CreateOwnerCommand(
        string FirstName,
        string LastName,
        string MiddleName,
        string Email,
        string PhoneNumber
    ) : IRequest<Guid>
        ;
}
