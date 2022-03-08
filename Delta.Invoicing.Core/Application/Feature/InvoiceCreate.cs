using System.Text.RegularExpressions;
using AutoMapper;
using Delta.Invoicing.Core.Application.Abstraction;
using Delta.Invoicing.Core.Domain;
using FluentValidation;
using MediatR;

namespace Delta.Invoicing.Core.Application.Feature;

public class InvoiceCreate : IRequestHandler<InvoiceCreate.Command>
{
    private readonly IInvoiceRepository _repository;
    private readonly IMapper _mapper;

    public InvoiceCreate(IInvoiceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<Unit> Handle(Command command, CancellationToken cancellationToken)
    {
        _repository.Add(_mapper.Map<Invoice>(command));
        return Unit.Task;
    }

    public class Command : IRequest<Unit>
    {
        public string? ClientName { get; set; }
        public string? ClientAddress { get; set; }
        public string? ClientTaxNumber { get; set; }
        public string? ClientBankAccountNumber { get; set; }
        public ICollection<InvoiceItemDto> InvoiceItems { get; set; } = new List<InvoiceItemDto>();
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(e => e.ClientName).NotEmpty();
            RuleFor(e => e.ClientAddress).NotEmpty();
            RuleFor(e => e.ClientTaxNumber).NotEmpty().Matches(new Regex(@"^\d{7}-\d{2}-\d$"));
            RuleFor(e => e.ClientBankAccountNumber).NotEmpty().Matches(new Regex(@"^\d{8}-\d{8}-\d{8}$"));
            RuleFor(e => e.InvoiceItems).NotEmpty();
            RuleForEach(e => e.InvoiceItems)
                .ChildRules(e=>
                {
                    e.RuleFor(i => i.Name).NotEmpty();
                    e.RuleFor(i => i.Quantity).GreaterThanOrEqualTo(1);
                    e.RuleFor(i => i.Unit).NotEmpty();
                    e.RuleFor(i => i.UnitPrice).GreaterThan(0);
                });
        }
    }
    
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InvoiceCreate.Command, Invoice>()
                .ForMember(e => e.Id, e => e.Ignore())
                .ForMember(e => e.Serial, e => e.Ignore())
                .ForMember(e => e.Amount, e => e.Ignore())
                .ForMember(e => e.HasStorno, e => e.Ignore())
                .ForMember(e => e.OriginalInvoiceId, e => e.Ignore())
                .ForMember(e => e.CreatedOn, e=> e.MapFrom(_ => DateTime.UtcNow));

            CreateMap<InvoiceCreate.InvoiceItemDto, InvoiceItem>()
                .ForMember(e => e.Amount, e => e.Ignore());
        }
    }

    public class InvoiceItemDto
    {
        public string? Name { get; set; }
        public decimal? Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}