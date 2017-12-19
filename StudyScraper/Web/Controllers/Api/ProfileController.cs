using StudyScraper.Models.Responses;
using StudyScraper.Models.ViewModels;
using StudyScraper.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudyScraper.Web.Controllers.Api
{
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage SelectById(int id)
        {
            try
            {
                ItemResponse<Profile> resp = new ItemResponse<Profile>();
                ProfilesService svc = new ProfilesService();
                resp.Item = svc.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
