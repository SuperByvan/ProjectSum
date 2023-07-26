using System.Text.Json.Serialization;

namespace Persistence.SolrModel.SolrHandler;

public class AddRequestHandlerModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("class")]
    public string Class { get; set; }
    [JsonPropertyName("startup")]
    public string Startup { get; set; }
    [JsonPropertyName("defaults")]
    public Defaults Defaults { get; set; }
    [JsonPropertyName("components")]
    public List<string> Components { get; set; }
}