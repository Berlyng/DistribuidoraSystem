namespace Distribuidora.API.Customers.Create
{
    public sealed record CreateCustomerRequests(
        string Name,
        string TaxId,
        string PhoneNumber,
        string Address,
        string? ContactName,
        bool CreditEnabled,
        int CreditDays);
   
}
