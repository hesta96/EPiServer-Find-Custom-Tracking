using System.Collections.Generic;
using System.Linq;
using CustomClickTracking.Models.Pages;
using CustomClickTracking.Models.ViewModels;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Framework.Statistics;
using EPiServer.Find.UnifiedSearch;

namespace CustomClickTracking.Business
{
    public class SearchService : ISearchService
    {
        private readonly IClient _client;

        public SearchService(IClient iClient)
        {
            _client = iClient;
        }

        public IEnumerable<SearchContentModel.SearchHit> Search(string searchText, int maxResults)
        {
            var query = _client.UnifiedSearchFor(searchText)
                .Filter(f => !f.MatchType(typeof (ArticlePage)))
                .Filter(f => !f.MatchType(typeof(ProductPage)))
                .Filter(f => !f.MatchType(typeof(NewsPage)));
            var searchResults = query.Take(maxResults).Track().GetResult();

            return searchResults.Hits.SelectMany(s => CreateHitModel(s.Document));
        }

        public IEnumerable<SearchContentModel.SearchHit> SearchForArticles(string searchText, int maxResults)
        {
            var query = _client.Search<ArticlePage>().For(searchText);
            var searchResults = query.Take(maxResults).Track().GetContentResult();

            return searchResults.Items.Select(s => new SearchContentModel.SearchHit()
            {
                Title = s.Name,
                Url = s.LinkURL,
                Excerpt = s.Name
            });
        }

        public IEnumerable<SearchContentModel.SearchHit> SearchForProducts(string searchText, int maxResults)
        {
            var query = _client.Search<ProductPage>().For(searchText);
            var searchResults = query.Take(maxResults).Track().GetContentResult();

            return searchResults.Items.Select(s => new SearchContentModel.SearchHit()
            {
                Title = s.Name,
                Url = s.LinkURL,
                Excerpt = s.Name
            });
        }

        public IEnumerable<SearchContentModel.SearchHit> SearchForNews(string searchText, int maxResults)
        {
            var query = _client.Search<NewsPage>().For(searchText);
            var searchResults = query.Take(maxResults).Track().GetContentResult();

            return searchResults.Items.Select(s => new SearchContentModel.SearchHit()
            {
                Title = s.Name,
                Url = s.LinkURL,
                Excerpt = s.Name
            });
        }

        private IEnumerable<SearchContentModel.SearchHit> CreateHitModel(UnifiedSearchHit hit)
        {
            yield return CreatePageHit(hit);
        }

        private SearchContentModel.SearchHit CreatePageHit(UnifiedSearchHit hit)
        {
            return new SearchContentModel.SearchHit
            {
                Title = hit.Title,
                Url = hit.Url,
                Excerpt = hit.Excerpt
            };
        }

        
    }
}