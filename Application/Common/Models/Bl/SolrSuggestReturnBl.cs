namespace Application.Common.Models.Bl;

public class SolrSuggestReturnBl
{
    public string Term { get; set; }
    public string Weight { get; set; }
    public string Payload { get; set; }
    public string Type { get; set; }
}