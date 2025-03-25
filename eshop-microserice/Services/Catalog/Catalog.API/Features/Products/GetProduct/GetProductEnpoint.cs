
using Catalog.API.Features.Products.QueryProduct;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Features.Products.GetProduct;

//public record GetProductRequest();

public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductEnpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products",
            async (/*GetProductRequest request,*/ ISender sender) =>
            {
                //map request to query
                //var query = request.Adapt<GetProductQuery>();
                //send mediatR
                var result = await sender.Send(new GetProductQuery());
                //return result
                var response = result.Adapt<GetProductResponse>();
                return Results.Ok(response);
            });
    }
}
