using MediatR;
using SharpMicroservices.Payment.API.Repositories;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Services;
using System.Net;

namespace SharpMicroservices.Payment.API.Features.Payments.Create;

public class CreatePaymentCommandHandler(AppDbContext context, IIdentityService identityService) : IRequestHandler<CreatePaymentCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var (isSuccess, errorMessage) = await ProcessPaymentAsync();

        if (!isSuccess)
        {
            return ServiceResult.Error("Payment Processing Failed", errorMessage ?? "An error occurred during payment processing.", HttpStatusCode.BadRequest);
        }

        var newPayment = new Repositories.Payment(identityService.GetUserId, request.OrderCode, request.Amount);
        newPayment.UpdateStatus(PaymentStatus.Completed);
        context.Payments.Add(newPayment);
        await context.SaveChangesAsync(cancellationToken);
        return ServiceResult.SuccessAsNoContent();
    }

    private async Task<(bool isSuccess, string? errorMessage)> ProcessPaymentAsync()
    {
        // Simulate payment processing logic
        await Task.Delay(2000); // Simulate a delay for processing
        return (true, null); // Assume payment is always successful for this example

        //return (false, "Payment failed due to insufficient funds.");
    }
}
