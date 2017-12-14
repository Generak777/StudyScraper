using StudyScraper.Models.Responses;
using StudyScraper.Models.ViewModels;
using StudyScraper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudyScraper.Web.Controllers.Api
{
    [RoutePrefix("api/scraper"), AllowAnonymous]
    public class ScraperController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                ItemResponse<RedditPost> resp = new ItemResponse<RedditPost>();
                RedditScraperService svc = new RedditScraperService();
                resp.Item = svc.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            
        }
    }
}
