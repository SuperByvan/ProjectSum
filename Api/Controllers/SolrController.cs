using Api.Responses;
using Application.Queries.GetOneTrackBySearching;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("solr")]
public class SolrController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SolrController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    [HttpGet("api/{search}")]
    public async Task<IActionResult> GetBySearch(string search)
    {
        var command = new GetOneTrackBySearchingQuery(search);
        var response = await _mediator.Send(command);
        return Ok(_mapper.Map<List<TrackResponse>>(response));
    }
}