using System.Net.Http.Json;
using System.Web;
using AutoMapper;
using Delta.Invoicing.Core.Application.Abstraction;
using Delta.Invoicing.Core.Domain;
using FluentValidation;
using MediatR;

namespace Delta.Invoicing.Core.Application.Feature;

public class CompanyDataGet : IRequestHandler<CompanyDataGet.Query, IEnumerable<CompanyDataGet.CompanyDataDto>>
{
    private readonly ICompanyDataStore _companyDataStore;
    private readonly IMapper _mapper;

    public CompanyDataGet(ICompanyDataStore companyDataStore, IMapper mapper)
    {
        _companyDataStore = companyDataStore;
        _mapper = mapper;
    }

    public Task<IEnumerable<CompanyDataDto>> Handle(Query request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_mapper.Map<IEnumerable<CompanyDataDto>>(_companyDataStore.Search(request.SearchTerm!)));
    }

    public class Query : IRequest<IEnumerable<CompanyDataDto>>
    {
        public string? SearchTerm { get; set; }
    }

    public class CompanyDataDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? TaxNumber { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(e => e.SearchTerm).NotEmpty().Length(3, int.MaxValue);
        }
    }

    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CompanyData, CompanyDataDto>();
        }
    }
}