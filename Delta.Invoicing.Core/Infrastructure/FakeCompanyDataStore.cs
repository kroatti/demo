using Bogus;
using Delta.Invoicing.Core.Application.Abstraction;
using Delta.Invoicing.Core.Domain;

namespace Delta.Invoicing.Core.Infrastructure;

internal class FakeCompanyDataStore : ICompanyDataStore
{
    private readonly List<CompanyData> _companyDataList; 
    
    public FakeCompanyDataStore()
    {
        var fakeCompanyData = new Faker<CompanyData>()
            .StrictMode(true)
            .RuleFor(e => e.Name, f => f.Company.CompanyName())
            .RuleFor(e => e.Address, f => f.Address.FullAddress())
            .RuleFor(e => e.BankAccountNumber, f => f.Random.ReplaceNumbers("########-########-########"))
            .RuleFor(e => e.TaxNumber, f => f.Random.ReplaceNumbers("#######-##-#"));

        _companyDataList = fakeCompanyData.GenerateBetween(5000, 5000);
    }
    
    public List<CompanyData> Search(string searchTerm)
    {
        return _companyDataList.Where(e => e.Name.StartsWith(searchTerm, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }
}