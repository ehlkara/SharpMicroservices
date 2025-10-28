using MediatR;
using SharpMicroservices.Payment.API.Repositories;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Services;
using System.Net;

namespace SharpMicroservices.Payment.API.Features.Payments.Create;

public class CreatePaymentCommandHandler(AppDbContext context, IIdentityService identityService, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreatePaymentCommand, ServiceResult<CreatePaymentResponse>>
{
    public async Task<ServiceResult<CreatePaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var (isSuccess, errorMessage) = await ProcessPaymentAsync();

        if (!isSuccess)
        {
            return ServiceResult<CreatePaymentResponse>.Error("Payment Processing Failed", errorMessage ?? "An error occurred during payment processing.", HttpStatusCode.BadRequest);
        }

        var newPayment = new Repositories.Payment(identityService.UserId, request.OrderCode, request.Amount);
        newPayment.UpdateStatus(PaymentStatus.Completed);
        context.Payments.Add(newPayment);
        await context.SaveChangesAsync(cancellationToken);
        return ServiceResult<CreatePaymentResponse>.SuccessAsOk(new CreatePaymentResponse(newPayment.Id, true, null));
    }

    private async Task<(bool isSuccess, string? errorMessage)> ProcessPaymentAsync()
    {
        // Simulate payment processing logic
        await Task.Delay(2000); // Simulate a delay for processing
        return (true, null); // Assume payment is always successful for this example

        //return (false, "Payment failed due to insufficient funds.");
    }
}
