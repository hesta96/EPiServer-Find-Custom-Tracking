using System;
using System.Web.Mvc;
using CustomClickTracking.Business;
using EPiServer.ServiceLocation;

namespace CustomClickTracking.Controllers.Ajax
{
    public class ClickTrackingController :  Controller
    {
        [HttpGet]
        public JsonResult Track(string query, string hitId, string trackId)
        {
            try
            {
                var searchService = ServiceLocator.Current.GetInstance<SearchService>();

                searchService.TrackClick(query, hitId, trackId);

                return Json(new {msg = "Click tracked"}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new {Success = false}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}