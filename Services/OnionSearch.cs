using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PexelsDotNetSDK.Api;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.Text.RegularExpressions;

namespace CalendarTelegramBot.Services
{
    public class OnionSearch
    {
        public static List<string> SearchOnion2(string searchText)
        {
            string url = @"https://yandex.ru/images/search?text=" + searchText;
            var webClient = new WebClient();
            var domParser = new HtmlParser();

            List<string> urlsParser = new List<string>();
            var htmlDoc = webClient.DownloadString(url);
            var document = domParser.ParseDocument(htmlDoc);
            //отбираем элементы c заголовка
            var parseLinks = document.QuerySelectorAll("div.serp-item__preview");

            Regex regex = new Regex(@"img_url=http[^&]+", RegexOptions.Singleline);

            foreach (var html in parseLinks)
            {
                MatchCollection matches = regex.Matches(html.OuterHtml);
                if (matches.Count > 0)
                {
                    var urlImg = System.Web.HttpUtility.UrlDecode(matches.First().Value);
                    urlsParser.Add(urlImg.Replace("img_url=", string.Empty));
                }

            }

            return urlsParser;
        }

        static string key;
        static string Key
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                {
                    key = Environment.GetEnvironmentVariable("IMG_TOKEN");
                }
                return key;
            }
        }

        public static async Task<IEnumerable<PexelsDotNetSDK.Models.Photo>> SearchOnion(string message)
        {            
            var pexelsClient = new PexelsClient(Key);

            try
            {
                //делаем запрос, получаем ответ
                var result = await pexelsClient.SearchPhotosAsync(message, pageSize: 80);
                return result.photos;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
