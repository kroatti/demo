using Delta.Invoicing.Core.Application.Feature;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Delta.InvoicingAngular.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyDataController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompanyDataController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<CompanyDataGet.CompanyDataDto>), 200)]
    public async Task<IActionResult> Search([FromQuery] string? search)
    {
        return Ok(await _mediator.Send(new CompanyDataGet.Query {SearchTerm = search}));
    }
}