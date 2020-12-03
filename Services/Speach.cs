using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CalendarTelegramBot.Services
{
    public enum speaker { Dasha_n, Anna_n, Julia_n, Vladimir_n };
    public class Speach
    {
        public static speaker _speaker = speaker.Dasha_n;

        static string URL = @"https://apihost.ru/d2.php";

        static HttpClient client;
        static HttpClient Client
        {
            get
            {
                if (client == null)
                {
                    HttpClientHandler httpClientHandler = new HttpClientHandler();
                    httpClientHandler.DefaultProxyCredentials = CredentialCache.DefaultCredentials;
                    httpClientHandler.UseProxy = true;
                    httpClientHandler.AllowAutoRedirect = true;

                    client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
                }
                return client;
            }
        }
        public static async Task<byte[]> Synthes(string text)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("speaker", _speaker.ToString()),
                    new KeyValuePair<string, string>("from", text)
                });



                var dict = new Dictionary<string, string>();
                dict.Add("speaker", "Dasha_n");
                dict.Add("from", text);
               
                var req = new HttpRequestMessage(HttpMethod.Post, URL) { Content = formContent };
                HttpResponseMessage response = null;


                response = Client.SendAsync(req).ConfigureAwait(false).GetAwaiter().GetResult();




                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("OK");
                    var result = await response.Content.ReadAsStringAsync();
                    var index = result.IndexOf("\"fname\":\"");
                    var apiurl = result.Substring(index + 10, result.Length - 13);
                    apiurl = apiurl.Replace("\\", string.Empty);

                    req = new HttpRequestMessage(HttpMethod.Get, "https://apihost.ru" + apiurl);
                    response = Client.SendAsync(req).ConfigureAwait(true).GetAwaiter().GetResult();


                    return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                }
                else Console.WriteLine("Error: " + response.StatusCode.ToString());
                return null;
                //var response = await client2.PostAsync(URL, formContent);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

    }
}
