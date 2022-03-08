using Bogus;
using Delta.Invoicing.Core.Application.Abstraction;
using Delta.Invoicing.Core.Domain;

namespace Delta.Invoicing.Core.Infrastructure;

class FakeItemRepository : IItemRepository
{
    private readonly List<Item> _items;

    public FakeItemRepository()
    {
        var fakeItems = new Faker<Item>()
            .StrictMode(true)
            .RuleFor(e => e.Name, f => f.Commerce.ProductName());

        _items = fakeItems.GenerateBetween(50, 50);
    }
    
    public IEnumerable<Item> GetAllOrdered()
    {
        return _items.OrderBy(e => e.Name);
    }
}