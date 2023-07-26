using Application.Common.Models.Bl;

namespace Application.Common.Interfaces;

public interface ISolrCollectionService
{
    Task<TrackBl> AddAsync(TrackBl item, CancellationToken cancellationToken);
    Task<IEnumerable<TrackBl>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<SolrSuggestReturnBl>> GetBySearchAsync(string search, CancellationToken cancellationToken);
    Task<TrackBl> GetSongBySolrResponse(string song, CancellationToken cancellationToken);
    Task<TrackBl> GetArtistBySolrResponse(string artist, CancellationToken cancellationToken);
}