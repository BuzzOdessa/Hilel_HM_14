using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Animals.Api.Domain.Owners.Records;
using Animals.Application.Domain.Owners.Commands.CreateOwner;
using Animals.Api.Constants;

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

    }
}
