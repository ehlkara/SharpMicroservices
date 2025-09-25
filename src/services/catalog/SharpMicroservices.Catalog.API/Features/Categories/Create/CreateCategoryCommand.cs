using MediatR;
using SharpMicroservices.Shared;

namespace SharpMicroservices.Catalog.API.Features.Categories.Create;

public record CreateCategoryCommand(string Name) : IRequest<ServiceResult<CreateCategoryResponse>>;
