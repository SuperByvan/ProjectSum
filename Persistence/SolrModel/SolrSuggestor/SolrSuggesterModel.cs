using System.Text.Json.Serialization;

namespace Persistence.SolrModel.SolrSuggestor;

public class SolrSuggesterModel
{
    [JsonPropertyName("add-searchcomponent")]
    public AddSearchComponentModel AddSearchcomponent { get; set; }
}