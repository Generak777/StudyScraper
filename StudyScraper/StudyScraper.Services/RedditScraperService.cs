using HtmlAgilityPack;
using StudyScraper.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyScraper.Services
{
    public class RedditScraperService
    {
        public RedditPost GetAll()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            RedditPost model = new RedditPost();
            model.PostTitle = new List<string>();
            model.PostUrl = new List<string>();
            model.StudyTitle = new List<string>();
            model.StudyUrl = new List<string>();

            //assigning url to model
            string url = "https://www.reddit.com/r/Nootropics/search?q=flair%3A%22Scientific+Study%22+OR+site%3Ancbi.nlm.nih.gov&restrict_sr=on&sort=new&t=all";

            //loading html from provided url
            var htmlWeb = new HtmlWeb();
            HtmlDocument document = null;
            try
            {
                document = htmlWeb.Load(url);
                //getting post titles from page
                var postTitleNodes = document.DocumentNode.SelectNodes("//a");

                foreach(var node in postTitleNodes)
                {
                    var result = node.InnerHtml;
                    var postTitles = result;
                }

                ////adding URLs to list
                //if (postTitles != null)
                //{
                //    List<string> list = postTitles.ToList();

                //    //looping through URL list and putting the first 5 into the model
                //    int i = 1;
                //    foreach (var item in list)
                //    {
                //        model.PostTitle.Add(item);
                //        if (++i > 10) break; // for breavity
                //    }
                //}
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                model.PostTitle.Add(str);
                model.PostUrl.Add(str);
                model.StudyTitle.Add(str);
                model.StudyUrl.Add(str);
                return model;
            }
            return model;
        }
    }
}
