using Domain.Models.Dal;
using SolrNet.Attributes;

namespace Persistence.SolrModel;

public class SolrTrackModel
{
    public SolrTrackModel()
    {
        
    }

    public SolrTrackModel(TrackDal track)
    {
        
    }

    [SolrUniqueKey("id")]
    public string Id { get; set; }
    [SolrField("cover")]
    public string Cover { get; set; }
    [SolrField("duration")]
    public string Duration { get; set; }
    [SolrField("artist")]
    public string Artist { get; set; }
    [SolrField("album")]
    public string Album { get; set; }
    [SolrField("title")]
    public string Title { get; set; }
}