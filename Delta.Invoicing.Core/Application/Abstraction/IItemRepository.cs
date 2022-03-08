using Delta.Invoicing.Core.Domain;

namespace Delta.Invoicing.Core.Application.Abstraction;

public interface IItemRepository
{
    IEnumerable<Item> GetAllOrdered();
}