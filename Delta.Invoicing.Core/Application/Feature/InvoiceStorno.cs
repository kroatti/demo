using Delta.Invoicing.Core.Application.Abstraction;
using FluentValidation;
using MediatR;

namespace Delta.Invoicing.Core.Application.Feature;

public class InvoiceStorno : IRequestHandler<InvoiceDelete.Command>
{
    private readonly IInvoiceRepository _repository;

    public InvoiceStorno(IInvoiceRepository repository)
    {
        _repository = repository;
    }
    
    public Task<Unit> Handle(InvoiceDelete.Command command, CancellationToken cancellationToken)
    {
        var invoice = _repository.Get(command.Id);
        invoice = invoice.CreateStorno();
        _repository.Add(invoice);

        return Unit.Task;
    }

    public class Command : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(e => e.Id).GreaterThanOrEqualTo(1);
        }
    }
}