using EPiServer.Core;

namespace CustomClickTracking.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
