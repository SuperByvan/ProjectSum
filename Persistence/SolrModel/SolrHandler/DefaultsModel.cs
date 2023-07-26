using System.Text.Json.Serialization;

namespace Persistence.SolrModel.SolrHandler;

public class Defaults
{
    [JsonPropertyName("suggest")]
    public string Suggest { get; set; }
    [JsonPropertyName("suggest.count")]
    public string SuggestCount { get; set; }
}