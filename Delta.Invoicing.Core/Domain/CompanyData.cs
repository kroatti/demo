namespace Delta.Invoicing.Core.Domain;

public class CompanyData
{
    internal CompanyData()
    {
    }

    public CompanyData(string name, string address, string bankAccountNumber, string taxNumber)
    {
        Name = name;
        Address = address;
        BankAccountNumber = bankAccountNumber;
        TaxNumber = taxNumber;
    }

    public string Name { get; set; }
    public string Address { get; set; }
    public string BankAccountNumber { get; set; }
    public string TaxNumber { get; set; }
}