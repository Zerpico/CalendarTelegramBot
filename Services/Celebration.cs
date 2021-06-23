using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CalendarTelegramBot.Services
{
    public class Celebration
    {
        public static async Task<string[]> GetCelebrationToday()
        {
            List<string> result = new List<string>();

            HttpWebRequest request = WebRequest.CreateHttp(@"https://my-calend.ru/holidays");
            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();

            // parse
            var parser = new HtmlParser();
            var doc = parser.ParseDocument(stream);

            //отбираем элементы
            var parseItem = doc.QuerySelectorAll("ul").Where(item => item.ClassName == "holidays-items").First();
            var parseText = doc.QuerySelectorAll("li");

            foreach (var p in parseItem.Children)
            {
                //находим блок названия праздника
                var find = p.QuerySelectorAll("a").FirstOrDefault();
                if (find != null)
                    result.Add(find.TextContent.Trim());
                else
                {
                    find = p.QuerySelectorAll("span").FirstOrDefault();
                    if (find != null)
                        result.Add(find.TextContent.Trim());
                }
            }

            return result.ToArray();
        }
    }
}
