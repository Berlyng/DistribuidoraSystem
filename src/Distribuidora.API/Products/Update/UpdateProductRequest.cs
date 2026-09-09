namespace Distribuidora.API.Products.Update
{
    public sealed record UpdateProductRequest(
        Guid ProductId,
        string Name,
        string Description,
        string TaxType);
    
}
