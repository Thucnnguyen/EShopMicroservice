using BuildingBlocks.CQRS;
using Catalog.API.Model;
using Marten;

namespace Catalog.API.Features.Products.CreateProduct;
public record CreateProductCommand(string Name, List<String> Category, 
								string Description, string ImageFile, decimal Price) 
				: ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductHandler() : 
	ICommandHandller<CreateProductCommand, CreateProductResult>
{
	public async Task<CreateProductResult> Handle(CreateProductCommand command,
		CancellationToken cancellationToken)
	{
		// business create product 
		var product = new Product()
		{
			Name = command.Name,
			Category = command.Category,
			Description = command.Description,
			ImageFile = command.ImageFile,
			Price = command.Price,
		};
		//save to database

		//return result 
		return new CreateProductResult(Guid.NewGuid());
	}
}
