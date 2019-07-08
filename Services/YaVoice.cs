using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace calendar_flood_bot.Services
{
    public enum format { mp3, wav, opus };
    public enum quality { hi, lo };
    public enum speaker { jane, oksana, alyss, omazh, zahar, ermil };
    public enum emotion { good, neutral, evil }

    class Speech
    {
        static String URL = @"https://tts.voicetech.yandex.net/generate";
        static String Key = @"069b6659-984b-4c5f-880e-aaedcfd84102";
       

        /// <summary>
        /// Сгенерировать синтезированую речь из текста
        /// </summary>
        /// <param name="key">ключ доступа</param>
        /// <param name="text">текст для синтеза</param>
        /// <param name="_format">формат получаемого файла</param>
        /// <param name="lang">язык</param>
        /// <param name="_speaker">голос синтеза</param>
        /// <param name="_quality">качество получаемого файла</param>
        /// <param name="speed">скорость речи</param>
        /// <param name="_emotion">эмоция речи</param>
        public async Task<byte[]> Synthes(string text, format _format,
            speaker _speaker, quality _quality = quality.lo, double speed = 1.0,
            emotion _emotion = emotion.good)
        {
            string url = String.Format(URL + "?text={0}&format={1}&lang={7}&speaker={2}&emotion={3}&quality={4}&speed={5}&key={6}",
                text, _format, _speaker, _emotion, _quality, speed.ToString().Replace(',', '.'), Key, "ru-RU");

            TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>();
            HttpWebRequest request = WebRequest.CreateHttp(url);
            using (HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync()))
            using (Stream stream = response.GetResponseStream())
            using (MemoryStream ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                tcs.SetResult(ms.ToArray());
                return await tcs.Task;
            }
        }

      
    }
}
