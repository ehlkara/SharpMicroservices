using MediatR;
using SharpMicroservices.Shared;

namespace SharpMicroservices.Catalog.API.Features.Categories.Create;

public record CreateCategoryCommand(string Name) : IRequestByServiceResult<CreateCategoryResponse>;
