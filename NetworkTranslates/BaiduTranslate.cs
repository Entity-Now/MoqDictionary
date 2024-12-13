using EnTranslate.NetworkTranslates.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EnTranslate.NetworkTranslates
{
    public class BaiduTranslate : ANetworkTranslate
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string BaseUrl = "https://fanyi.baidu.com/langdetect";

        private class BaiduDetect
        {
            public int error { get; set; }
            public string msg { get; set; }
            public string lan { get; set; }
        }

        public override async Task<string> Detect(string text, string token = null)
        {
            var res = await httpClient.GetAsync(BaseUrl + $"?query={HttpUtility.UrlEncode(text)}");
            var jsonRes = await res.Content.ReadAsStringAsync();
            var objectRes = JsonConvert.DeserializeObject<BaiduDetect>(jsonRes);
            return objectRes.lan;
        }

        public override Task<string> GetToken()
        {
            throw new NotImplementedException();
        }

        public override Task<List<string>> Translate(List<string> texts, string fromLan, string toLan, string token = null)
        {
            throw new NotImplementedException();
        }
    }
}
