using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CalendarTelegramBot.Services
{
    public class OnionSearch
    {
        public static List<string> SearchOnion2(string searchText)
        {
            string url = @"https://yandex.ru/images/search?format=json&request={'blocks':[{'block':'content_type_search','params':{},'version':2}]}&text=".Replace("'", "\"") + searchText;
            var webClient = new WebClient();
            var domParser = new HtmlParser();

            List<string> urlsParser = new List<string>();
            var htmlDoc = webClient.DownloadString(url);


            Rootobject tmp = JsonConvert.DeserializeObject<Rootobject>(htmlDoc);
            if (tmp.blocks.Length == 0)
                return null;

            var htmlp = tmp.blocks[0].html;

            Regex regex = new Regex(@"img_url=http[^&]+", RegexOptions.Singleline);
            foreach(Match match in Regex.Matches(htmlp, @"img_url=http[^&]+", RegexOptions.Singleline))
            {
                if (match.Success)
                {
                    var urlImg = System.Web.HttpUtility.UrlDecode(match.Value);
                    urlsParser.Add(urlImg.Replace("img_url=", string.Empty));
                }
            }

            return urlsParser.Distinct().ToList();
        }
    }






    public class Rootobject
    {
        public string cnt { get; set; }
        public Block[] blocks { get; set; }
        public Metadata metadata { get; set; }
        public Assets1 assets { get; set; }
    }

    public class Metadata
    {
        public Bundles bundles { get; set; }
        public Assets assets { get; set; }
        public Extracontent extraContent { get; set; }
        public Bundlesmetadata bundlesMetadata { get; set; }
        public Assetsmetadata assetsMetadata { get; set; }
    }

    public class Bundles
    {
        public string lb { get; set; }
    }

    public class Assets
    {
        public string las { get; set; }
    }

    public class Extracontent
    {
        public object[] names { get; set; }
    }

    public class Bundlesmetadata
    {
        public string lb { get; set; }
    }

    public class Assetsmetadata
    {
        public string las { get; set; }
    }

    public class Assets1
    {
        public Asset[] assets { get; set; }
    }

    public class Asset
    {
        public string type { get; set; }
        public Attrs attrs { get; set; }
        public string url { get; set; }
    }

    public class Attrs
    {
        public string datarCid { get; set; }
        public string dataas { get; set; }
    }

    public class Block
    {
        public Name name { get; set; }
        public Params _params { get; set; }
        public string html { get; set; }
    }

    public class Name
    {
        public string block { get; set; }
        public Mods mods { get; set; }
    }

    public class Mods
    {
    }

    public class Params
    {
        public object[] bundles { get; set; }
    }


}
