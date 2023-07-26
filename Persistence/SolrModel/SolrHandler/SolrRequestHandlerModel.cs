using System.Text.Json.Serialization;

namespace Persistence.SolrModel.SolrHandler;

public class SolrRequestHandlerModel
{
    [JsonPropertyName("add-requesthandler")]
    public AddRequestHandlerModel AddRequestHandler { get; set; }
}