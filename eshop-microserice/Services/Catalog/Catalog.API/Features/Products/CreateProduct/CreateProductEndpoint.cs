namespace Catalog.API.Features.Products.CreateProduct;

public record CreateProductRequest(string Name, List<String> Category,
								string Description, string ImageFile, decimal Price);

public record CreateProductResponse(Guid id);

public class CreateProductEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/products",
			async (CreateProductRequest request, ISender sender) =>
			{
				//map request to command
				var command = request.Adapt<CreateProductCommand>();
				// send to mediatR
				var response = await sender.Send(command);
				// return results
				return Results.Created($"/products/{response.Id}", response);
			})
			.WithName("Create Product")
			.Produces<CreateProductResponse>(StatusCodes.Status201Created)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithDescription("Create Product")
			.WithSummary("Create Product");
	}
}
