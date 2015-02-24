using System.Collections.Generic;
using System.Linq;
using CustomClickTracking.Models.Pages;
using CustomClickTracking.Models.ViewModels;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Framework;
using EPiServer.Find.Framework.Statistics;
using EPiServer.Find.Statistics;
using EPiServer.ServiceLocation;

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
            var searchResults = query.Take(maxResults).GetResult();

            return searchResults.Hits.Select(hit => new SearchContentModel.SearchHit()
            {
                Title = hit.Document.Title,
                Url = hit.Document.Url,
                Excerpt = hit.Document.Excerpt,
                HitId = hit.Id,
                HitType = hit.Type
            });
        }

        public IEnumerable<SearchContentModel.SearchHit> SearchForArticles(string searchText, int maxResults)
        {
            var query = _client.Search<ArticlePage>().For(searchText);
            var searchResults = query.Take(maxResults).GetContentResult();

            return searchResults.Items.Select(s => new SearchContentModel.SearchHit()
            {
                Title = s.Name,
                Url = s.LinkURL,
                Excerpt = s.Name,
                HitId = SearchClient.Instance.Conventions.IdConvention.GetId(s),
                HitType = SearchClient.Instance.Conventions.TypeNameConvention.GetTypeName(s.GetType())
            });
        }

        public IEnumerable<SearchContentModel.SearchHit> SearchForProducts(string searchText, int maxResults)
        {
            var query = _client.Search<ProductPage>().For(searchText);
            var searchResults = query.Take(maxResults).GetContentResult();

            return searchResults.Items.Select(s => new SearchContentModel.SearchHit()
            {
                Title = s.Name,
                Url = s.LinkURL,
                Excerpt = s.Name,
                HitId = SearchClient.Instance.Conventions.IdConvention.GetId(s),
                HitType = SearchClient.Instance.Conventions.TypeNameConvention.GetTypeName(s.GetType())
            });
        }

        public IEnumerable<SearchContentModel.SearchHit> SearchForNews(string searchText, int maxResults)
        {
            var query = _client.Search<NewsPage>().For(searchText);
            var searchResults = query.Take(maxResults).GetContentResult();

            return searchResults.Items.Select(s => new SearchContentModel.SearchHit()
            {
                Title = s.Name,
                Url = s.LinkURL,
                Excerpt = s.Name,
                HitId = SearchClient.Instance.Conventions.IdConvention.GetId(s),
                HitType = SearchClient.Instance.Conventions.TypeNameConvention.GetTypeName(s.GetType())
            });
        }

        public void TrackQuery(string query, int nrOfHits, string id)
        {
            SearchClient.Instance.Statistics().TrackQuery(query, x =>
            {
                x.Id = id;
                x.Tags = ServiceLocator.Current.GetInstance<IStatisticTagsHelper>().GetTags();
                x.Query.Hits = nrOfHits;
            });
        }

        public void TrackClick(string query, string hitId, string trackId)
        {
            SearchClient.Instance.Statistics().TrackHit(query, hitId, command =>
            {
                command.Hit.Id = hitId;
                command.Id = trackId;
                command.Ip = "127.0.0.1";
                command.Tags = ServiceLocator.Current.GetInstance<IStatisticTagsHelper>().GetTags();
                command.Hit.Position = null;
            });
        }
    }
}