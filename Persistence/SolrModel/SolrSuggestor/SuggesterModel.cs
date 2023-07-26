using System.Text.Json.Serialization;

namespace Persistence.SolrModel.SolrSuggestor;

public class SuggesterModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("field")]
    public string Field { get; set; }
    [JsonPropertyName("suggestAnalyzerFieldType")]
    public string SuggestAnalyzerFieldType { get; set; }
    [JsonPropertyName("buildOnStartup")]
    public string BuildOnStartup { get; set; }
}