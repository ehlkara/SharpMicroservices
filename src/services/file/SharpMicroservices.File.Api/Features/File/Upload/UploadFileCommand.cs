using SharpMicroservices.Shared;

namespace SharpMicroservices.File.Api.Features.File.Upload;

public record UploadFileCommand(IFormFile File) : IRequestByServiceResult<UploadFileCommandResponse>;
