namespace Distribuidora.API.Products.Create
{
    public sealed record CreateProductPresentationRequest(
        string Name,
        int ConversionFactor,
        decimal RetailPrice,
        decimal WholesalePrice);
    
}
