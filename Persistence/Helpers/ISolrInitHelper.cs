namespace Persistence.Helpers;

public interface ISolrInitHelper
{ 
    Task<bool> CreateCollections();
}