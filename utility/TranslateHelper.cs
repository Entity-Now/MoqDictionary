using MoqDictionary.Model.Enum;
using MoqDictionary.NetworkTranslates.Interfaces;
using MoqDictionary.NetworkTranslates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoqDictionary.utility
{
    public static class TranslateHelper
    {
        static Dictionary<TranslateType, Type> translates = new Dictionary<TranslateType, Type>
        {
            [TranslateType.Bing] = typeof(EdgeTranslate),
            [TranslateType.Google] = typeof(GoogleTranslate),
            [TranslateType.Deep] = typeof(DeepTranslate),
            [TranslateType.Yandex] = typeof(YandexTranslate),
        };
        public static async Task<List<string>> getTranslateAsync(TranslateType translateType, List<string> text)
        {
            var type = translates[translateType];
            var NetworkTranslate = (ANetworkTranslate)Activator.CreateInstance(type);


            return await NetworkTranslate.Translate(text, "eng", "zho-CN");
        }
    }
}
