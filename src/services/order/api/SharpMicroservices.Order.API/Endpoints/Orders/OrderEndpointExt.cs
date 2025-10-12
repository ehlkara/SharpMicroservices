﻿using Asp.Versioning.Builder;

namespace SharpMicroservices.Order.API.Endpoints.Orders;

public static class OrderEndpointExt
{
    public static void AddOrderGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/orders").WithTags("Orders")
            .WithApiVersionSet(apiVersionSet)
            .CreateOrderGroupItemEndpoint();
    }
}
