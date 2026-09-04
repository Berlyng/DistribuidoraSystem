namespace Distribuidora.API.Customers.Update
{
    public sealed record UpdateCustomerRequest(
        string Name,
        string TaxId,
        string PhoneNumber,
        string Address,
        string? ContactName,
        bool CreditEnable,
        int CreditDays
    );

}
