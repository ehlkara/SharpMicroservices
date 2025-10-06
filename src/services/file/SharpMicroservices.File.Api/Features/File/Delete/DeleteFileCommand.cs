using SharpMicroservices.Shared;

namespace SharpMicroservices.File.Api.Features.File.Delete;

public record DeleteFileCommand(string FileName) : IRequestByServiceResult;
