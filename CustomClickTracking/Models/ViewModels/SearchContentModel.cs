using System.Collections.Generic;
using System.Web;
using CustomClickTracking.Models.Pages;

namespace CustomClickTracking.Models.ViewModels
{
    public class SearchContentModel : PageViewModel<SearchPage>
    {
        public SearchContentModel(SearchPage currentPage) : base(currentPage)
        {
        }

        public bool SearchServiceDisabled { get; set; }
        public string SearchedQuery { get; set; }
        public int NumberOfHits { get; set; }
        public IEnumerable<SearchHit> Hits { get; set; }
        public IEnumerable<SearchHit> Articles { get; set; }
        public IEnumerable<SearchHit> Products { get; set; }
        public IEnumerable<SearchHit> News { get; set; }
        public string TrackId { get; set; }

        public class SearchHit
        {
            public string Title { get; set; }
            public string Url { get; set; }
            public string Excerpt { get; set; }
            public string HitId { get; set; }
            public string HitType { get; set; }
        }
    }
}
