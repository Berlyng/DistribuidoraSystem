namespace Distribuidora.API.Products.GetById
{
    public sealed record ProductResponse(
        Guid Id,
        string Name,
        string? Description,
        string TaxType,
        bool IsActive,
        IReadOnlyCollection<ProductPresentationResponse> Presentations
        );
   
}
