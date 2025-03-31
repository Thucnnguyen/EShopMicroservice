


namespace Catalog.API.Features.Products.CreateProduct;
public record CreateProductCommand(string Name, List<String> Category,
								string Description, string ImageFile, decimal Price)
				: ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
		RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
		RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
		RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImagFile is Required");
		RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price mut be greater than 0");
    }
}

internal class CreateProductHandler(IDocumentSession session, 
						IValidator<CreateProductCommand> validator) :
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
