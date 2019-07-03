using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace calendar_flood_bot.Services
{
    public class Celebration
    {
        public static async Task<string> GetCelebrationToday()
        {
            string result = string.Empty;

            HttpWebRequest request = WebRequest.CreateHttp(@"http://kakoysegodnyaprazdnik.ru/");
            var response = request.GetResponse();
            var stream = response.GetResponseStream();

            // parse
            var parser = new HtmlParser();
            var doc = parser.ParseDocument(stream);

            //отбираем элементы
            var parseItem = doc.QuerySelectorAll("div").Where(item => item.ClassName == "listing_wr").First();
            var parseText = doc.QuerySelectorAll("span").First();

            foreach (var p in parseItem.Children)
            {
                //выйдем если наткнемся на рекламный блок, там чаще всего начичинаются праздники других стран
                var dis = p.GetAttribute("id");
                if (!string.IsNullOrEmpty(dis) && dis.Contains("pr_in"))
                    break;

                //находим блок перечисления праздния
                var textparse = p.QuerySelectorAll("span");
                if (textparse.Count() != 0)
                    result += textparse.First().TextContent.Trim().Replace("•", "") + System.Environment.NewLine;
            }

            return result;
        }
    }
}
