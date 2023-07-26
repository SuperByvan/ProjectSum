namespace Persistence.SolrModel;

public class SolrSuggestReturnEntity
{
    public string Term { get; set; }
    public string Weight { get; set; }
    public string Payload { get; set; }
    public string Type { get; set; }
}