using Application.Common.Interfaces;
using Application.Common.Models.Bl;
using MediatR;

namespace Application.Commands.AddMusic;

public class AddMusicCommandHandler : IRequestHandler<AddMusicCommand, TrackBl>
{
    private readonly ISolrCollectionService _solrMusicCollectionService;

    public AddMusicCommandHandler(ISolrCollectionService solrMusicCollectionService)
    {
        _solrMusicCollectionService = solrMusicCollectionService;
    }
    
    public async Task<TrackBl> Handle(AddMusicCommand request, CancellationToken cancellationToken)
    {
        var song = await _solrMusicCollectionService.AddAsync(request.TrackBl, cancellationToken);
        return song;
    }
}