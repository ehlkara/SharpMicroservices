using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharpMicroservices.Order.Application.Contracts.Repositories;
using SharpMicroservices.Order.Application.Contracts.UnitOfWork;
using SharpMicroservices.Order.Domain.Entities;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Services;
using System.Net;

namespace SharpMicroservices.Order.Application.Features.Orders.Create;

public class CreateOrderCommandHandler(IOrderRepository orderRepository, IIdentityService identityService, IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, ServiceResult>
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
        var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId, request.discountRate, newAddress.Id);

        foreach (var item in request.Items)
        {
            order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice);
        }

        order.Address = newAddress;

        orderRepository.Add(order);
        await unitOfWork.CommitAsync(cancellationToken);

        var paymentId = Guid.Empty;

        // Payment process would be here...

        order.MarkAsPaid(paymentId);
        orderRepository.Update(order);
        await unitOfWork.CommitAsync(cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}
