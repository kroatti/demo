using Delta.Invoicing.Core.Exceptions;

namespace Delta.Invoicing.Core.Domain;

public class Invoice
{
    public int Id { get; set; }
    
    public string Serial => Id.ToString().PadLeft(8, '0');
    
    public string? ClientName { get; set; }
    
    public string? ClientAddress { get; set; }
    
    public string? ClientBankAccountNumber { get; set; }
    
    public string? ClientTaxNumber { get; set; }

    public decimal Amount => InvoiceItems.Sum(e => e.Amount);

    public bool HasStorno { get; set; }
    
    public int? OriginalInvoiceId { get; set; }
    
    public DateTime? CreatedOn { get; set; }
    
    public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public Invoice CreateStorno()
    {
        // TODO handle DomainException
        if (HasStorno)
            throw new DomainException("The invoice has already had a storno invoice.");
        
        HasStorno = true;

        return new Invoice()
        {
            OriginalInvoiceId = Id,
            // TODO copy others
        };
    }
}