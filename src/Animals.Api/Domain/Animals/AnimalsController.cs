using System.ComponentModel.DataAnnotations;
using Animals.Api.Constants;
using Animals.Api.Domain.Animals.Records;
using Animals.Application.Domain.Animals.Commands.CreateAnimal;
using Animals.Application.Domain.Animals.Commands.DeleteAnimal;
using Animals.Application.Domain.Animals.Commands.UpdateAnimal;
using Animals.Application.Domain.Animals.Queries.GetAnimalDetails;
using Animals.Application.Domain.Animals.Queries.GetAnimals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Animals.Api.Domain.Animals;

[Route(Routes.Animals)]
public class AnimalsController(
    IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Отримати список тварин
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> GetAnimals(
        [FromQuery] [Required] int page = 1,
        [FromQuery] [Required] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAnimalsQuery(page, pageSize);
        var animals = await mediator.Send(query, cancellationToken);
        return Ok(animals);
    }

    /// <summary>
    /// Знайти тварину по ID 
    /// </summary>
    /// <param name="id">ідентифікатор тварини в отелі</param>             
    /// <returns></returns>


    [HttpGet("{id}")]
    public async Task<ActionResult> GetAnimalDetails(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAnimalDetailsQuery(id);
        var good = await mediator.Send(query, cancellationToken);
        return Ok(good);
    }

    /// <summary>
    /// Додати тварину 
    /// </summary>
    /// <param name="request"></param>        
    /// <returns></returns>

    [HttpPost]
    public async Task<ActionResult> AddAnimal(
        [FromBody] [Required] CreateAnimalRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateAnimalCommand(
            request.Name, 
            request.Age, 
            request.Description);
        var id = await mediator.Send(command, cancellationToken);
        return Ok(id);
    }

    /// <summary>
    /// оновити інформацію по  тварині
    /// </summary>
    /// <param name="id">ідентифікатор тварини </param>        
    /// <returns></returns>

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAnimal(
        [FromRoute] Guid id,
        [FromBody] [Required] UpdateAnimalRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateAnimalCommand(
            id,
            request.Name,
            request.Age,
            request.Description);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// видалити тварину 
    /// </summary>
    /// <param name="id">ідентифікатор тварини </param>
    /// <returns></returns>

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAnimal(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteAnimalCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}