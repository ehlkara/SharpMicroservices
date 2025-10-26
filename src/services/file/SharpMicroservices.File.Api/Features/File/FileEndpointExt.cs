using Asp.Versioning.Builder;
using SharpMicroservices.File.Api.Features.File.Delete;
using SharpMicroservices.File.Api.Features.File.Upload;

namespace SharpMicroservices.File.Api.Features.File;

public static class FileEndpointExt
{
    public static void AddFileGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/files").WithTags("Files")
            .WithApiVersionSet(apiVersionSet)
            .UploadFileGroupItemEndpoint()
            .DeleteFileGroupItemEndpoint().RequireAuthorization("Password");
    }
}
