namespace Distribuidora.API.Products.Create
{
    public sealed record CreateProductRequest(
        string Name,
        string? Description,
        string TaxType,
        IReadOnlyCollection<CreateProductPresentationRequest> Presentations);
   
}
