namespace Delta.Invoicing.Core.Domain;

public class InvoiceItem
{
    public string? Name { get; set; }
    
    public decimal? Quantity { get; set; }
    
    public string? Unit { get; set; }
    
    public decimal? UnitPrice { get; set; }

    public decimal Amount => Quantity.GetValueOrDefault() * UnitPrice.GetValueOrDefault();

}