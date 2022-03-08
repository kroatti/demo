using Delta.Invoicing.Core.Domain;

namespace Delta.Invoicing.Core.Application.Abstraction;

public interface IInvoiceRepository
{
    Invoice Get(int id);
    List<Invoice> GetList(string? searchTerm);
    void Remove(int id);
    void Add(Invoice invoice);
}