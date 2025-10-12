using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharpMicroservices.Order.Application.Repositories.Contracts;
using SharpMicroservices.Order.Domain.Entities;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Services;
using System.Net;

namespace SharpMicroservices.Order.Application.Features.Orders.Create;

public class CreateOrderCommandHandler(IGenericRepository<Guid, Domain.Entities.Order> orderRepository, IGenericRepository<int, Address> addressRepository, IIdentityService identityService) : IRequestHandler<CreateOrderCommand, ServiceResult>
{
    public Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if(!request.Items.Any())
            return Task.FromResult(ServiceResult.Error(new ProblemDetails
            {
                Title = "Invalid Order",
                Detail = "Order must contain at least one item."
            }, HttpStatusCode.BadRequest));

        //TODO: begin transaction
        var newAddress = new Address
        {
            Province = request.Address.Province,
            District = request.Address.District,
            Street = request.Address.Street,
            ZipCode = request.Address.ZipCode,
            Line = request.Address.Line
        };
        addressRepository.Add(newAddress);
        //unitOfWork.Commit();

        var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId, request.discountRate, newAddress.Id);

        foreach (var item in request.Items)
        {
            order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice);
        }
        orderRepository.Add(order);
        //unitOfWork.Commit();

        var paymentId = Guid.Empty;
        // Payment process would be here...

        order.MarkAsPaid(paymentId);
        orderRepository.Update(order);
        //unitOfWork.Commit();

        return Task.FromResult(ServiceResult.SuccessAsNoContent());
    }
}
