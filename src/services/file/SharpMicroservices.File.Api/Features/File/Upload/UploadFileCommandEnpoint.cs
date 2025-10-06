using MediatR;
using SharpMicroservices.Shared.Extensions;

namespace SharpMicroservices.File.Api.Features.File.Upload;

public static class UploadFileCommandEnpoint
{
    public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (UploadFileCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
            .WithName("UploadFile")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        return group;
    }
}
