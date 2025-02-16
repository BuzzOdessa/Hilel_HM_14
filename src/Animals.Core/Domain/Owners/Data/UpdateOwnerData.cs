using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Domain.Owners.Data
{
    public record UpdateOwnerData(
        string FirstName,
        string LastName,
        string? MiddleName,
        string Email,
        string PhoneNumber
    );
}
