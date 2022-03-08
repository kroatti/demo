using AutoMapper;
using Delta.Invoicing.Core.Application.Abstraction;
using Delta.Invoicing.Core.Domain;
using FluentValidation;
using MediatR;

namespace Delta.Invoicing.Core.Application.Feature;

public class InvoiceSearch : IRequestHandler<InvoiceSearch.Query, List<InvoiceSearch.InvoiceDto>>
{
    private readonly IInvoiceRepository _repository;
    private readonly IMapper _mapper;

    public InvoiceSearch(IInvoiceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<List<InvoiceDto>> Handle(Query request, CancellationToken cancellationToken)
    {
        List<Domain.Invoice> invoices = _repository.GetList(request.SearchTerm);

        return  Task.FromResult(_mapper.Map<List<InvoiceDto>>(invoices));
    }

    public class Query : IRequest<List<InvoiceDto>>
    {
        public string? SearchTerm { get; set; }
    }
    
    public class InvoiceDto
    {
        public int? Id { get; set; }
        public string? Serial { get; set; }
        public string? ClientName { get; set; }
        public decimal? Amount { get; set; }
        public bool? HasStorno { get; set; }
        public int? OriginalInvoiceId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    internal class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Invoice, InvoiceDto>();
        }
    }
}