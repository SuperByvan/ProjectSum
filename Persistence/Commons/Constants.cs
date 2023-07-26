using Persistence.SolrModel.SolrHandler;
using Persistence.SolrModel.SolrSuggestor;

namespace Persistence.Commons;

public class Constants
{
    public static SolrSuggesterModel Suggestor = new SolrSuggesterModel
    {
        AddSearchcomponent = new AddSearchComponentModel()
        {
            Name = "suggest",
            Class = "solr.SuggestComponent",
            Suggester = new List<SuggesterModel>()
            {
                new SuggesterModel()
                {
                    Name = "suggest",
                    Field = "title",
                    SuggestAnalyzerFieldType = "text_general",
                    BuildOnStartup = "true"
                },
                new SuggesterModel()
                {
                    Name = "artSuggest",
                    Field = "artist",
                    SuggestAnalyzerFieldType = "text_general",
                    BuildOnStartup = "true"
                }
            }
        },
    };

    public static SolrRequestHandlerModel Handler = new SolrRequestHandlerModel()
    {
        AddRequestHandler = new AddRequestHandlerModel()
        {
            Name = "/suggest",
            Class = "solr.SearchHandler",
            Startup = "lazy",
            Defaults = new Defaults()
            {
                Suggest = "true",
                SuggestCount = "10"
            },
            Components = new  List<string>()
            {
                "suggest"
            }
        }
    };
}