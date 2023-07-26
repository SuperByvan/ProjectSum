using System.Text.Json.Serialization;

namespace Persistence.SolrModel.SolrSuggestor;

public class AddSearchComponentModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("class")]
    public string Class { get; set; }
    [JsonPropertyName("suggester")]
    public List<SuggesterModel> Suggester { get; set; }
}