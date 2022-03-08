using Delta.Invoicing.Core.Domain;

namespace Delta.Invoicing.Core.Application.Abstraction;

public interface ICompanyDataStore
{
    List<CompanyData> Search(string searchTerm);
}