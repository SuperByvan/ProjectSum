using Api.Responses;
using Application.Commands.AddMusic;
using Application.Common.Models.Bl;
using Application.Queries.GetOneTrackBySearching;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.SolrModel;

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
    
    [HttpPost]
    [Route("api/create")]
    public async Task<IActionResult> Post([FromBody] SolrTrackModel music)
    {
        var command = new AddMusicCommand(_mapper.Map<TrackBl>(music));
        var response = await _mediator.Send(command);
        return Ok(_mapper.Map<CreateTrackResponse>(response));
    }
}