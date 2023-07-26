using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Persistence.Commons;

namespace Persistence.Helpers;

public class SolrInitHelper : ISolrInitHelper
{
    private readonly HttpClient _client;
    private readonly ILogger<SolrInitHelper> _logger;
    private readonly IConfiguration _configuration;
    public string UrlConfig { get; }
    public string Username { get; set; }
    public string Password { get; set; }

    public SolrInitHelper(ILogger<SolrInitHelper> logger, IConfiguration configuration, HttpClient client)
    {
        _logger = logger;
        _configuration = configuration;
        _client = client;
        UrlConfig = _configuration["Solr:Config"];
        Username = _configuration["Solr:Username"];
        Password = _configuration["Solr:Password"];
    }


    public async Task<bool> CreateCollections()
    {
        return await InitSuggester();
    }

    private async Task<bool> InitSuggester()
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ConvertHelper.ConvertLoginToBase64(Username, Password));
        var jsonSuggester = JsonSerializer.Serialize(Constants.Suggestor);
        var jsonHandler = JsonSerializer.Serialize(Constants.Handler);
        var jsonContentSuggester = new StringContent(jsonSuggester, Encoding.UTF8, "application/json");
        var jsonContentHandler = new StringContent(jsonHandler, Encoding.UTF8, "application/json");
        
        try
        {
            var responseSuggester = await _client.PostAsync(UrlConfig, jsonContentSuggester);
            var responseHandler = await _client.PostAsync(UrlConfig, jsonContentHandler);
            
            if (responseSuggester.IsSuccessStatusCode && responseHandler.IsSuccessStatusCode)
                return true;

            var updateJsonSuggester = jsonSuggester.Replace("add", "update");
            var updateJsonHandler = jsonHandler.Replace("add", "update");
            var updateJsonContentSuggester = new StringContent(updateJsonSuggester, Encoding.UTF8, "application/json");
            var updateJsonContentHandler = new StringContent(updateJsonHandler, Encoding.UTF8, "application/json");
            await _client.PostAsync(UrlConfig, updateJsonContentSuggester);
            await _client.PostAsync(UrlConfig, updateJsonContentHandler);
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to POST data: {ex.Message}");
            throw;
        }
    }
}