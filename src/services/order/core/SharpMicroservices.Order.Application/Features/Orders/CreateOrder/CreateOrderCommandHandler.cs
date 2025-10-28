using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharpMicroservices.Bus.Events;
using SharpMicroservices.Order.Application.Contracts.Refit.PaymentService;
using SharpMicroservices.Order.Application.Contracts.Repositories;
using SharpMicroservices.Order.Application.Contracts.UnitOfWork;
using SharpMicroservices.Order.Domain.Entities;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Services;
using System.Net;

namespace SharpMicroservices.Order.Application.Features.Orders.CreateOrder;

public class CreateOrderCommandHandler(IOrderRepository orderRepository, IIdentityService identityService, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint, IPaymentService paymentService) : IRequestHandler<CreateOrderCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!request.Items.Any())
            return ServiceResult.Error(new ProblemDetails
            {
                Title = "Invalid Order",
                Detail = "Order must contain at least one item."
            }, HttpStatusCode.BadRequest);

        var newAddress = new Address
        {
            Province = request.Address.Province,
            District = request.Address.District,
            Street = request.Address.Street,
            ZipCode = request.Address.ZipCode,
            Line = request.Address.Line
        };
        var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.UserId, request.DiscountRate, newAddress.Id);

        foreach (var item in request.Items)
        {
            order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice);
        }

        order.Address = newAddress;

        orderRepository.Add(order);
        await unitOfWork.CommitAsync(cancellationToken);

        CreatePaymentRequest paymentRequest = new CreatePaymentRequest(order.Code, request.Payment.CardNumber, request.Payment.CardHolderName, request.Payment.Expiration, request.Payment.Cvc, order.TotalPrice);

        var paymentResponse = await paymentService.CreateAsync(paymentRequest);

        if (!paymentResponse.Status)
            return ServiceResult.Error(paymentResponse.ErrorMessage!, HttpStatusCode.InternalServerError);

        order.MarkAsPaid(paymentResponse.PaymentId);
        orderRepository.Update(order);
        await unitOfWork.CommitAsync(cancellationToken);

        await publishEndpoint.Publish(new OrderCreatedEvent(order.Id, identityService.UserId), cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}
