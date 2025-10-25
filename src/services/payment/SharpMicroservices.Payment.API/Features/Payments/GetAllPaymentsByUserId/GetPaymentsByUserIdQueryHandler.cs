using MediatR;
using Microsoft.EntityFrameworkCore;
using SharpMicroservices.Payment.API.Repositories;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Services;

namespace SharpMicroservices.Payment.API.Features.Payments.GetAllPaymentsByUserId;

public class GetPaymentsByUserIdQueryHandler(AppDbContext context, IIdentityService identityService) : IRequestHandler<GetPaymentsByUserIdQuery, ServiceResult<List<GetPaymentsByUserIdResponse>>>
{
    public async Task<ServiceResult<List<GetPaymentsByUserIdResponse>>> Handle(GetPaymentsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userId = identityService.UserId;
        var payments = await context.Payments.Where(p => p.UserId == userId).Select(p => new GetPaymentsByUserIdResponse(p.Id, p.OrderCode, p.Amount.ToString("C"), p.Created, p.PaymentStatus)).ToListAsync(cancellationToken);

        return ServiceResult<List<GetPaymentsByUserIdResponse>>.SuccessAsOk(payments);
    }
}
