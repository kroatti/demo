using Delta.Invoicing.Core.Application.Feature;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Delta.InvoicingAngular.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("ordered")]
    [ProducesResponseType(typeof(IEnumerable<ItemGet.ItemDto>), 200)]
    public async Task<IActionResult> GetOrdered()
    {
        return Ok(await _mediator.Send(new ItemGet.Query()));
    }
}