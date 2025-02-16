using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _5Layers.Animals.Persistence.EFCore.AnimalsDb;
using Animals.Core.Domain.Animals.Common;
using Animals.Core.Domain.Animals.Models;
using Animals.Core.Domain.Owners.Common;
using Animals.Core.Domain.Owners.Models;
using Microsoft.EntityFrameworkCore;

namespace Animals.Infrastructure.Core.Domain.Owners.Common
{
    
    public class OwnersEFCoreRepository(AnimalsDbContext dbContext) : IOwnersRepository
    {
        
        public void Add(Owner owner)
        {
            dbContext.Add(owner);
        }

        public void Delete(Owner owner)
        {
            dbContext.Remove(owner);
        }

        public async Task<Owner> GetOwnerById(Guid ownerId, CancellationToken cancellationToken)
        {
            return await 
                 dbContext.Owners
                   .Where(x => x.Id == ownerId)
                   .FirstOrDefaultAsync(cancellationToken) ?? throw new InvalidOperationException("owner was not found");
        }
    }
}
