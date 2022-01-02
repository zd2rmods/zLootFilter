using System.Text.Json.Serialization;

namespace zLootFilterConsoleApp.Models
{
    internal class ItemModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        public string Key { get; set; }

        [JsonPropertyName("enUS")]
        public string EnUS { get; set; }

        [JsonPropertyName("zhTW")]
        public string ZhTW { get; set; }

        [JsonPropertyName("deDE")]
        public string DeDE { get; set; }

        [JsonPropertyName("esES")]
        public string EsES { get; set; }

        [JsonPropertyName("frFR")]
        public string FrFR { get; set; }

        [JsonPropertyName("itIT")]
        public string ItIT { get; set; }

        [JsonPropertyName("koKR")]
        public string KoKR { get; set; }

        [JsonPropertyName("plPL")]
        public string PlPL { get; set; }

        [JsonPropertyName("esMX")]
        public string EsMX { get; set; }

        [JsonPropertyName("jaJP")]
        public string JaJP { get; set; }

        [JsonPropertyName("ptBR")]
        public string PtBR { get; set; }

        [JsonPropertyName("ruRU")]
        public string RuRU { get; set; }

        [JsonPropertyName("zhCN")]
        public string ZhCN { get; set; }
    }
}
