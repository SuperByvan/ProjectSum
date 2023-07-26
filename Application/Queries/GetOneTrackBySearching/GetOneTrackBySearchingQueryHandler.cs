using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.Bl;
using MediatR;

namespace Application.Queries.GetOneTrackBySearching;

public class GetOneTrackBySearchingQueryHandler : IRequestHandler<GetOneTrackBySearchingQuery, List<SolrSuggestReturnBl>>
{
    private readonly ISolrCollectionService _solrCollectionService;
    
    public GetOneTrackBySearchingQueryHandler(ISolrCollectionService solrCollectionService)
    {
        _solrCollectionService = solrCollectionService;
    }
    
    public async Task<List<SolrSuggestReturnBl>> Handle(GetOneTrackBySearchingQuery request, CancellationToken cancellationToken)
    {
        var result = await _solrCollectionService.GetBySearchAsync(request.Search, cancellationToken);
        if (result == null)
        {
            throw new NotFoundException("Track", "Track not found");
        }
        return result;
    }
}