using StudyScraper.Models.Requests;
using StudyScraper.Models.Responses;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudyScraper.Web.Controllers.Api
{
    [RoutePrefix("api/forgotPassword"), AllowAnonymous]
    public class ForgotPasswordController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SendEmail(ResetPasswordRequest model)
        {
            if(!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                SuccessResponse resp = new SuccessResponse();
                //ForgotPasswordService svc = new ForgotPasswordService();
                svc.ForgotPasswordInsert(model.Email);

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
