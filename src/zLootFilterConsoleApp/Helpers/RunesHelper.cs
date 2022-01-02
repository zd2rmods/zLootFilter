using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using zLootFilterConsoleApp.Models;

namespace zLootFilterConsoleApp.Helpers
{
    internal static class RunesHelper
    {
        public static async Task ProcessRunesFile(string itemRunesFilePath)
        {
            ItemModel[] runesArray;
            using (var stream = File.OpenRead(itemRunesFilePath))
            {
                runesArray = await JsonSerializer.DeserializeAsync<ItemModel[]>(stream);
            }

            if (runesArray is null || runesArray.Length == 0)
            {
                throw new Exception("Runes not found");
            }

            var runeIndexRegex = new Regex(@"^r(?<index>\d+)$");

            foreach(var rune in runesArray)
            {
                var match = runeIndexRegex.Match(rune.Key);

                if (match.Success)
                {
                    var index = Int32.Parse(match.Groups["index"].Value);

                    ProcessRune(rune, index);
                }
            }

            var json = JsonSerializer.Serialize(runesArray, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
            });

            await File.WriteAllTextAsync(itemRunesFilePath, json, new UTF8Encoding(true));
        }

        private static void ProcessRune(ItemModel rune, int index)
        {
            rune.EnUS = $"ÿc8[rune] {rune.EnUS} ({index})";

            if (index > 25)
            {
                rune.EnUS = $"ÿc1•••••{rune.EnUS}ÿc1•••••";
            }
            else if (index > 20)
            {
                rune.EnUS = $"ÿc1•••{rune.EnUS}ÿc1•••";
            }
            else if (index >= 15)
            {
                rune.EnUS = $"ÿc1•{rune.EnUS}ÿc1•";
            }
        }
    }
}
