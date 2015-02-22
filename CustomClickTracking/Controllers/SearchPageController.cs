using System.Linq;
using System.Web.Mvc;
using CustomClickTracking.Business;
using CustomClickTracking.Models.Pages;
using CustomClickTracking.Models.ViewModels;

namespace CustomClickTracking.Controllers
{
    public class SearchPageController : PageControllerBase<SearchPage>
    {
        private readonly ISearchService _ePiServerFindSearchService;

        public SearchPageController(ISearchService ePiServerFindSearchService)
        {
            _ePiServerFindSearchService = ePiServerFindSearchService;
        }

        [ValidateInput(false)]
        public ViewResult Index(SearchPage currentPage, string q)
        {
            const int maxResults = 40;
            var model = new SearchContentModel(currentPage)
            {
                SearchServiceDisabled = false,
                SearchedQuery = q
            };
            if (!string.IsNullOrWhiteSpace(q))
            {
                model.Hits = _ePiServerFindSearchService.Search(q.Trim(), maxResults);
                model.Articles = _ePiServerFindSearchService.SearchForArticles(q.Trim(), maxResults);
                model.Products = _ePiServerFindSearchService.SearchForProducts(q.Trim(), maxResults);
                model.News = _ePiServerFindSearchService.SearchForNews(q.Trim(), maxResults);
                model.NumberOfHits = model.Articles.Count() + model.Products.Count() + model.News.Count() + model.Hits.Count();
            }

            return View(model);
        }
    }
}
