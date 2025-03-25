
namespace Catalog.API.Features.Products.QueryProduct;

public record GetProductQuery() : IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);

internal class GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger)
    : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProuctQueryHandler.Query called with {@Query}", query);

        var products = await session.Query<Product>().ToListAsync(token: cancellationToken);
        return new(products);
    }
}
