using System.Collections.Generic;
using CustomClickTracking.Models.ViewModels;

namespace CustomClickTracking.Business
{
    public interface ISearchService
    {
        IEnumerable<SearchContentModel.SearchHit> Search(string searchText, int maxResults);
        IEnumerable<SearchContentModel.SearchHit> SearchForArticles(string searchText, int maxResults);
        IEnumerable<SearchContentModel.SearchHit> SearchForProducts(string searchText, int maxResults);
        IEnumerable<SearchContentModel.SearchHit> SearchForNews(string searchText, int maxResults);
        void TrackQuery(string query, int nrOfHits, string id);
        void TrackClick(string query, string hitId, string trackId);
    }
}