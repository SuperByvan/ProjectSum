using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using Application.Common.Interfaces;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Helpers;
using Persistence.Services;
using Persistence.SolrModel;
using SolrNet;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSolrNet(configuration["Solr:Url"], options =>
        {
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(configuration["Solr:Username"] + ":" + configuration["Solr:Password"]));
            options.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", credentials);
        });
        services.AddScoped<ISolrCollectionService, SolrCollectionService<ISolrOperations<SolrTrackModel>>>();
        var config = new TypeAdapterConfig();
        services.AddSingleton(config);
        services.AddHttpClient<ISolrCollectionService, SolrCollectionService<ISolrOperations<SolrTrackModel>>>();
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddTransient<ISolrInitHelper, SolrInitHelper>();
        services.BuildServiceProvider().GetService<ISolrInitHelper>().CreateCollections();
        
        return services;
    }
}