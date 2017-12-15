namespace StudyScraper.Services
{
    public abstract class BaseService
    {
        protected string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
}
