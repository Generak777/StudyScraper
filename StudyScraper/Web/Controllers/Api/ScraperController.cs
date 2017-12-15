using StudyScraper.Models.Requests;
using StudyScraper.Models.Responses;
using StudyScraper.Models.ViewModels;
using StudyScraper.Services;
using System;
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
                ItemResponse<RedditPosts> resp = new ItemResponse<RedditPosts>();
                RedditScraperService svc = new RedditScraperService();
                resp.Item = svc.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            
        }

        [HttpPost]
        public HttpResponseMessage SavePost(SavePostRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                RedditScraperService svc = new RedditScraperService();
                int id = svc.SavePost(model);
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = id;
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}
