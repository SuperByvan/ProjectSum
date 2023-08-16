using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.Bl;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;

namespace Application.Queries.GetOneTrackBySearching;

public class GetOneTrackBySearchingQueryHandler : IRequestHandler<GetOneTrackBySearchingQuery, List<SolrSuggestReturnBl>>
{
    private readonly ISolrCollectionService _solrCollectionService;
    private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;
    private readonly ILogger<GetOneTrackBySearchingQueryHandler> _logger;

    public GetOneTrackBySearchingQueryHandler(ISolrCollectionService solrCollectionService, AsyncCircuitBreakerPolicy circuitBreakerPolicy, ILogger<GetOneTrackBySearchingQueryHandler> logger)
    {
        _solrCollectionService = solrCollectionService;
        _circuitBreakerPolicy = circuitBreakerPolicy;
        _logger = logger;
    }
    
    public async Task<List<SolrSuggestReturnBl>> Handle(GetOneTrackBySearchingQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _circuitBreakerPolicy.ExecuteAsync(async () =>
            {
                var result = await _solrCollectionService.GetBySearchAsync(request.Search, cancellationToken);
                if (result == null)
                {
                    throw new NotFoundException("Track", "Track not found");
                }
                return result;
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