using Application.Common.Models.Bl;
using MediatR;

namespace Application.Queries.GetOneTrackBySearching;

public class GetOneTrackBySearchingQuery : IRequest<List<SolrSuggestReturnBl>>
{
    public GetOneTrackBySearchingQuery(string search)
    {
        Search = search;
    }

    public string Search { get; }
}