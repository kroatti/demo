using AutoMapper;
using Delta.Invoicing.Core.Application.Abstraction;
using Delta.Invoicing.Core.Domain;
using FluentValidation;
using MediatR;

namespace Delta.Invoicing.Core.Application.Feature;

public class InvoiceGet : IRequestHandler<InvoiceGet.Query, InvoiceGet.InvoiceDetailsDto>
{
    private readonly IInvoiceRepository _repository;
    private readonly IMapper _mapper;

    public InvoiceGet(IInvoiceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<InvoiceDetailsDto> Handle(Query request, CancellationToken cancellationToken)
    {
        Domain.Invoice invoice = _repository.Get(request.Id);

        return  Task.FromResult(_mapper.Map<InvoiceDetailsDto>(invoice));
    }

    public class Query : IRequest<InvoiceDetailsDto>
    {
        public int Id { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(e => e.Id).GreaterThanOrEqualTo(1);
        }
    }
    
    public class InvoiceDetailsDto
    {
        public int? Id { get; set; }
        public string? Serial { get; set; }
        public string? ClientName { get; set; }
        public string? ClientAddress { get; set; }
        public string? ClientBankAccountNumber { get; set; }
        public string? ClientTaxNumber { get; set; }
        public decimal? Amount { get; set; }
        public bool? HasStorno { get; set; }
        public int? OriginalInvoiceId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public ICollection<InvoiceItemDto>? InvoiceItems { get; set; } = new List<InvoiceItemDto>();
    }

    public class InvoiceItemDto
    {
        public string? Name { get; set; }
        public decimal? Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
    }

    internal class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Invoice, InvoiceDetailsDto>();
            CreateMap<InvoiceItem, InvoiceItemDto>();
        }
    }
}