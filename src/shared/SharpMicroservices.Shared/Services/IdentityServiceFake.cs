
namespace SharpMicroservices.Shared.Services;

public class IdentityServiceFake : IIdentityService
{
    public Guid UserId => Guid.Parse("8e38b13e-45a1-4150-9254-80b34bcbe707");

    public string UserName => "Ehlullah";

    public List<string> Roles => [];
}
