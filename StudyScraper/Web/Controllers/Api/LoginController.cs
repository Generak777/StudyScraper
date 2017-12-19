using StudyScraper.Models.Domain;
using StudyScraper.Models.Requests;
using StudyScraper.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudyScraper.Web.Controllers.Api
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpPost, AllowAnonymous]
        public HttpResponseMessage Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                LoginData res = new LoginData();
                LoginService svc = new LoginService();
                res = svc.Login(model);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
