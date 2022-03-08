using AutoMapper;
using Delta.Invoicing.Core.Application.Abstraction;
using Delta.Invoicing.Core.Domain;
using MediatR;

namespace Delta.Invoicing.Core.Application.Feature;

public class ItemGet : IRequestHandler<ItemGet.Query, IEnumerable<ItemGet.ItemDto>>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public ItemGet(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public Task<IEnumerable<ItemDto>> Handle(Query request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_mapper.Map<IEnumerable<ItemDto>>(_itemRepository.GetAllOrdered()));
    }

    public class Query : IRequest<IEnumerable<ItemDto>>
    {
    }

    public class ItemDto
    {
        public string? Name { get; set; }
    }

    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ItemDto>();
        }
    }
}