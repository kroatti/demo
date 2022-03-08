using Delta.Invoicing.Core.Application.Feature;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Delta.InvoicingAngular.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(InvoiceGet.InvoiceDetailsDto), 200)]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _mediator.Send(new InvoiceGet.Query {Id = id}));
    }
    
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<InvoiceSearch.InvoiceDto>), 200)]
    public async Task<IActionResult> Search([FromQuery] string? search)
    {
        return Ok(await _mediator.Send(new InvoiceSearch.Query {SearchTerm = search}));
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(object), 200)]
    public async Task<IActionResult> Create([FromBody] InvoiceCreate.Command command)
    {
        return Ok(await _mediator.Send(command));
    }
    
    [HttpDelete]
    [ProducesResponseType(typeof(object), 200)]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _mediator.Send(new InvoiceDelete.Command() {Id = id}));
    }
    
    [HttpPost("storno")]
    [ProducesResponseType(typeof(object), 200)]
    public async Task<IActionResult> Storno(int id)
    {
        return Ok(await _mediator.Send(new InvoiceStorno.Command() {Id = id}));
    }
}