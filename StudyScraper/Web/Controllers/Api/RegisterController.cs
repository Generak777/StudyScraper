using StudyScraper.Models.Requests;
using StudyScraper.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudyScraper.Web.Controllers.Api
{
    [RoutePrefix("api/register")]
    [AllowAnonymous]
    public class RegisterController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post(RegisterRequest model)
        {
            if(!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ModelState);
            }
            try
            {
                RegisterService svc = new RegisterService();
                int id = svc.RegisterUser(model);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
