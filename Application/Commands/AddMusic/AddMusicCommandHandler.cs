using Application.Common.Interfaces;
using Application.Common.Models.Bl;
using Application.Queries.GetOneTrackBySearching;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;

namespace Application.Commands.AddMusic;

public class AddMusicCommandHandler : IRequestHandler<AddMusicCommand, TrackBl>
{
    private readonly ISolrCollectionService _solrMusicCollectionService;
    private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;
    private readonly ILogger<GetOneTrackBySearchingQueryHandler> _logger;

    public AddMusicCommandHandler(ISolrCollectionService solrMusicCollectionService, AsyncCircuitBreakerPolicy circuitBreakerPolicy, ILogger<GetOneTrackBySearchingQueryHandler> logger)
    {
        _solrMusicCollectionService = solrMusicCollectionService;
        _circuitBreakerPolicy = circuitBreakerPolicy;
        _logger = logger;
    }

    public async Task<TrackBl> Handle(AddMusicCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _circuitBreakerPolicy.ExecuteAsync(async () =>
            {
                var song = await _solrMusicCollectionService.AddAsync(request.TrackBl, cancellationToken);
                return song;
            });
        }
        catch (BrokenCircuitException e)
        {
            _logger.LogError(e, "Circuit breaker is open, service temporarily unavailable");
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong");
            throw;
        }
    }
}