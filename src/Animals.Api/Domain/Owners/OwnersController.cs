using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Animals.Api.Domain.Owners.Records;
using Animals.Application.Domain.Owners.Commands.CreateOwner;
using Animals.Api.Constants;
using Animals.Api.Domain.Animals.Records;
using Animals.Application.Domain.Animals.Commands.UpdateAnimal;
using Animals.Application.Domain.Owners.Commands.UpdateOwner;

namespace Animals.Api.Domain.Owners
{
    [Route(Routes.Owners)]
    public class OwnersController(IMediator mediator) :ControllerBase
    {
        /// <summary>
        /// Додати власника !!!
        /// </summary>
        /// <param name="request"></param>        
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult> AddOwner(
            [FromBody][Required] CreateOwnerRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = new CreateOwnerCommand(
                    request.FirstName,
                    request.LastName,
                    request.MiddleName,
                    request.Email,
                    request.PhoneNumber
                );
            var id = await mediator.Send(command, cancellationToken);
            return Ok(id);
        }


        /// <summary>
        /// оновити інформацію по  власнику
        /// </summary>
        /// <param name="id">ідентифікатор влксника </param>        
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOwner(
            [FromRoute] Guid id,
            [FromBody][Required] UpdateOwnerRequest request,
           CancellationToken cancellationToken = default)
       {
           var command = new UpdateOwnerCommand(
               id,
               request.FirstName,
               request.LastName,
               request.MiddleName,
               request.Email,
               request.PhoneNumber
            );
            await mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}
