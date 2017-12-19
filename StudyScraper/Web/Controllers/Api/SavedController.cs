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
    [RoutePrefix("api/saved"), AllowAnonymous]
    public class SavedController : ApiController
    {
        [Route("user/{id:int}"), HttpGet]
        public HttpResponseMessage GetAll(int id)
        {
            try
            {
                ItemsResponse<SavedPost> resp = new ItemsResponse<SavedPost>();
                SavedService svc = new SavedService();
                resp.Items = svc.GetAll(id);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage SelectById(int id)
        {
            try
            {
                ItemResponse<SavedPost> resp = new ItemResponse<SavedPost>();
                SavedService svc = new SavedService();
                resp.Item = svc.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage Update(UpdatePostRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                SuccessResponse resp = new SuccessResponse();
                SavedService svc = new SavedService();
                svc.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                SuccessResponse response = new SuccessResponse();
                SavedService svc = new SavedService();
                svc.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
