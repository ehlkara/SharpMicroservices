using MediatR;
using Microsoft.Extensions.FileProviders;
using SharpMicroservices.Shared;

namespace SharpMicroservices.File.Api.Features.File.Upload;

public class UploadFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<UploadFileCommand, ServiceResult<UploadFileCommandResponse>>
{
    public Task<ServiceResult<UploadFileCommandResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
