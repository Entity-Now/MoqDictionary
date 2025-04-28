using MoqDictionary.NetworkTranslates.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MoqDictionary.NetworkTranslates
{
    public class YandexTranslate : ANetworkTranslate
    {
        private static readonly string baseUrl = "https://translate.yandex.net/api/v1/tr.json/translate";

        private static readonly Dictionary<string, string> supportLanguages = new Dictionary<string, string>
        {
            {"zho-CN", "zh"},
            {"zho-TW", "zh"},
            {"eng", "en"},
            {"jpn", "ja"},
            {"kor", "ko"},
            {"fre", "fr"},
            {"spa", "es"},
            {"rus", "ru"},
            {"ger", "de"},
            {"ita", "it"},
            {"tur", "tr"},
            {"por-PT", "pt"},
            {"por", "pt"},
            {"vie", "vi"},
            {"ind", "id"},
            {"tha", "th"},
            {"msa", "ms"},
            {"ara", "ar"},
            {"hin", "hi"},
        };

        private static readonly HttpClient httpClient = new HttpClient();

        public override Task<string> Detect(string text, string token = null)
        {
            throw new NotImplementedException();
        }

        public override Task<string> GetToken()
        {
            throw new NotImplementedException();
        }

        public override async Task<List<string>> Translate(List<string> texts, string fromLan, string toLan, string token = null)
        {
            List<string> result = new List<string>();

            var queryParams = new Dictionary<string, string>
            {
                { "id", Guid.NewGuid().ToString("N") + "-0-0" },
                { "srv", "android" }
            };
            var queryString = new FormUrlEncodedContent(queryParams).ReadAsStringAsync().Result;
            var fullUrl = $"{baseUrl}?{queryString}";

            var bodyForm = new Dictionary<string, string>
            {
                { "source_lang", supportLanguages[fromLan] },
                { "target_lang", supportLanguages[toLan] },
                { "text", ArrayToString(texts)},
            };
            var content = new FormUrlEncodedContent(bodyForm);

            var response = await httpClient.PostAsync(fullUrl, content);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var transResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);

            if (transResponse.ContainsKey("text") && transResponse["text"] is Newtonsoft.Json.Linq.JArray textArray && textArray.Count > 0)
            {
                result = StringToArray(textArray[0].ToString());
            }
            else
            {
                throw new Exception($"Unexpected response format: {jsonResponse}");
            }

            return result;
        }
    }
}
