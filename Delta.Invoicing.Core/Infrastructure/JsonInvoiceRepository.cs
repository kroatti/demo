using System.Text.Json;
using Delta.Invoicing.Core.Application;
using Delta.Invoicing.Core.Application.Abstraction;
using Delta.Invoicing.Core.Domain;
using Delta.Invoicing.Core.Exceptions;

namespace Delta.Invoicing.Core.Infrastructure;

class JsonInvoiceRepository : IInvoiceRepository
{
    private readonly object _sync = new object();

    private static readonly List<Invoice> Invoices = new();

    public Invoice Get(int id)
    {
        lock (_sync)
        {
            var invoice = Invoices.Find(e => e.Id == id);

            if (invoice == null)
                throw new NotFoundException();

            return invoice;
        }
    }

    public List<Invoice> GetList(string? searchTerm)
    {
        return searchTerm != null ? Invoices.Where(e => e.Serial.StartsWith(searchTerm)).ToList() : Invoices;
    }

    public void Remove(int id)
    {
        lock (_sync)
        {
            var invoice = Invoices.Find(e => e.Id == id);

            if (invoice == null)
                throw new NotFoundException();

            Invoices.Remove(invoice);
            Save();
        }
    }

    public void Add(Invoice invoice)
    {
        if (invoice == null) throw new ArgumentNullException(nameof(invoice));

        lock (_sync)
        {
            invoice.Id = Invoices.Any() ? Invoices.Select(e => e.Id).Max() + 1 : 1;
            Invoices.Add(invoice);
            Save();
        }
    }

    private void Save()
    {
        using var streamWriter = File.CreateText("DATA");
        streamWriter.Write(JsonSerializer.Serialize(Invoices));
        streamWriter.Flush();
        streamWriter.Close();
    }
}