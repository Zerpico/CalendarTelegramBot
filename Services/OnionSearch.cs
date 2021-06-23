using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PexelsDotNetSDK.Api;

namespace CalendarTelegramBot.Services
{
    public class OnionSearch
    {
        
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
