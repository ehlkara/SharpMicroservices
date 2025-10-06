using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharpMicroservices.Shared.Extensions;

namespace SharpMicroservices.File.Api.Features.File.Delete;

public static class DeleteFileCommandEnpoint
{
    public static RouteGroupBuilder DeleteFileGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/", async ([FromBody] DeleteFileCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
            .WithName("DeleteFile")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        return group;
    }
}
