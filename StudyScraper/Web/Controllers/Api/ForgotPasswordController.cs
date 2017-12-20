using StudyScraper.Models.Requests;
using StudyScraper.Models.Responses;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudyScraper.Web.Controllers.Api
{

    [RoutePrefix("api/confirmation")]
    public class ForgotPasswordController : ApiController
    {
        [Route, HttpGet, AllowAnonymous]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, "GET success");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{email}/g"), HttpGet, AllowAnonymous]
        public HttpResponseMessage GetEmail(string email)
        {
            try
            {
                ForgotPasswordService svc = new ForgotPasswordService();
                svc.ForgotPasswordInsert(email);
                SuccessResponse resp = new SuccessResponse();

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{token}"), HttpGet, AllowAnonymous]
        public HttpResponseMessage GetByToken(string token)
        {
            try
            {
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = forgotPasswordService.GetByToken(token);

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route, HttpPost, AllowAnonymous]
        public HttpResponseMessage GetUserId(ConfirmationTokenPassword model)
        {
            try
            {
                forgotPasswordService.GetUserId(model.Token, model.NewBasicPassword);
                SuccessResponse resp = new SuccessResponse();

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
