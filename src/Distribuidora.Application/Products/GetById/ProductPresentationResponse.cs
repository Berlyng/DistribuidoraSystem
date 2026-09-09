namespace Distribuidora.API.Products.GetById
{
    public sealed record ProductPresentationResponse(
        Guid Id,
        string Name,
        int ConversionFactor,
        decimal RetailPrice,
        decimal WholesalePrice,
        bool IsActive);

}
