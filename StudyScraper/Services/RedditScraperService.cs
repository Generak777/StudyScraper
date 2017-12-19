using HtmlAgilityPack;
using StudyScraper.Models.Requests;
using StudyScraper.Models.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace StudyScraper.Services
{
    public class RedditScraperService : BaseService
    {
        public RedditPosts GetAll()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            //instantiate model and the lists inside the model
            RedditPosts model = new RedditPosts();
            model.Posts = new List<RedditPost>();

            //assigning url to the string
            string url = "https://www.reddit.com/r/Nootropics/search?q=flair%3A%22Scientific+Study%22+OR+site%3Ancbi.nlm.nih.gov&restrict_sr=on&sort=new&t=all";

            //instantiating loading HtmlWeb from provided url
            var htmlWeb = new HtmlWeb();
            HtmlDocument document = null;
            document = htmlWeb.Load(url);

            //getting desired post titles and links from page
            var anchorTags = document.DocumentNode.Descendants("a")
                .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("search-title"));

            //looping through the variable and assigning the desired values to my model
            foreach (var node in anchorTags)
            {
                RedditPost item = new RedditPost();
                item.PostTitle = node.InnerText;
                item.PostUrl = node.GetAttributeValue("href", null);
                //item is pushed to model list
                model.Posts.Add(item);
            }
            //returning model
            return model;
        }

        public int SavePost(SavePostRequest model)
        {
            int id = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("RedditPosts_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Url", model.Url);

                    SqlParameter param = new SqlParameter("@Id", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();

                    id = (int)cmd.Parameters["@Id"].Value;
                }
                conn.Close();
            }
            return id;
        }
    }
}
