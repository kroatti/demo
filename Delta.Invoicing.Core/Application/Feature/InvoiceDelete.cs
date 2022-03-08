using Delta.Invoicing.Core.Application.Abstraction;
using FluentValidation;
using MediatR;

namespace Delta.Invoicing.Core.Application.Feature;

public class InvoiceDelete : IRequestHandler<InvoiceDelete.Command>
{
    private readonly IInvoiceRepository _repository;

    public InvoiceDelete(IInvoiceRepository repository)
    {
        _repository = repository;
    }
    
    public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
        _repository.Remove(request.Id);
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