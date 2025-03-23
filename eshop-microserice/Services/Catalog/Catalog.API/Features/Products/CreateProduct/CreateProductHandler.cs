

namespace Catalog.API.Features.Products.CreateProduct;
public record CreateProductCommand(string Name, List<String> Category,
								string Description, string ImageFile, decimal Price)
				: ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductHandler(IDocumentSession session) :
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
		session.Store(product);
		await session.SaveChangesAsync(cancellationToken);
		//return result 
		return new CreateProductResult(product.Id);
	}
}
