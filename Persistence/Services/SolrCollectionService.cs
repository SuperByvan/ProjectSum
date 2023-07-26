using System.Net.Http.Headers;
using System.Text.Json;
using Application.Common.Interfaces;
using Application.Common.Models.Bl;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Persistence.Helpers;
using Persistence.SolrModel;
using SolrNet;

namespace Persistence.Services;

public class SolrCollectionService<TSolrOperations> : ISolrCollectionService 
    where TSolrOperations : ISolrOperations<SolrTrackModel>
{
    private readonly TSolrOperations _solr;
    private readonly IMapper _mapper;
    private readonly ILogger<SolrCollectionService<TSolrOperations>> _logger;
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    public string UrlQuery { get; }
    public string Username { get; }
    public string Password { get; }

    public SolrCollectionService(ISolrOperations<SolrTrackModel> solr, IMapper mapper, ILogger<SolrCollectionService<TSolrOperations>> logger, IConfiguration configuration, HttpClient client)
    {       
        _solr = (TSolrOperations) solr;
        _mapper = mapper;
        _logger = logger;
        _configuration = configuration;
        _client = client;
        UrlQuery = _configuration["Solr:UrlQuery"];
        Username = _configuration["Solr:Username"];
        Password = _configuration["Solr:Password"];
    }

    public async Task<TrackBl> AddAsync(TrackBl item, CancellationToken cancellationToken)
    {
        try
        {
            await _solr.AddAsync(_mapper.Map<SolrTrackModel>(item));
            await _solr.CommitAsync();
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while adding item to solr collection, item: {item}", item);
            throw;
        }
    }

    public async Task<IEnumerable<TrackBl>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            var results = await _solr.QueryAsync(new SolrQuery("*:*"), cancellationToken);
            return _mapper.Map<IEnumerable<TrackBl>>(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting all items from solr collection");
            throw;
        }
    }

    public async Task<List<SolrSuggestReturnBl>> GetBySearchAsync(string search, CancellationToken cancellationToken)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ConvertHelper.ConvertLoginToBase64(Username, Password));
            var results = await _client.GetAsync($"{UrlQuery}{search}", cancellationToken);
            var stringContent = await results.Content.ReadAsStringAsync().ConfigureAwait(false);
            var suggest = JsonDocument.Parse(stringContent).RootElement.GetProperty("suggest");
            var alb = JsonConvert.DeserializeObject<List<SolrSuggestReturnEntity>>(suggest.GetProperty("suggest").GetProperty(search).GetProperty("suggestions").ToString())!;
            alb.ForEach(x => x.Type = "title");
            var art = JsonConvert.DeserializeObject<List<SolrSuggestReturnEntity>>(suggest.GetProperty("artSuggest").GetProperty(search).GetProperty("suggestions").ToString())!;
            art.ForEach(x => x.Type = "artist");
            var list = alb.Concat(art).ToList();
            return _mapper.Map<List<SolrSuggestReturnBl>>(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting all items from solr collection");
            throw;
        }
    }

    public async Task<TrackBl> GetSongBySolrResponse(string song, CancellationToken cancellationToken)
    {
        try
        {
            var results = await _solr.QueryAsync(new SolrQuery($"title:\"{song}\""), cancellationToken);
            return _mapper.Map<TrackBl>(results.FirstOrDefault());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting an item from solr collection with the song: {id}", song);
            throw;
        }
    }

    public async Task<TrackBl> GetArtistBySolrResponse(string artist, CancellationToken cancellationToken)
    {
        try
        {
            var results = await _solr.QueryAsync(new SolrQuery($"artist:\"{artist}\""), cancellationToken);
            return _mapper.Map<TrackBl>(results.FirstOrDefault());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting an item from solr collection with the id: {id}", artist);
            throw;
        }
    }
}