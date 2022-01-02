using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using zLootFilterConsoleApp.Models;

namespace zLootFilterConsoleApp.Helpers
{
    internal static class ItemNameAffixesHelper
    {
        private static readonly IDictionary<string, string> ItemNameAffixesMap = new Dictionary<string, string>
        {
            ["Low Quality"] = "LowQ",
            ["Damaged"] = "Dmg",
            ["Hiquality"] = "Sup"
        };

        public static async Task ProcessItemNameAffixesFile(string itemNameAffixesFilePath)
        {
            ItemModel[] itemAffixesArray;
            using (var stream = File.OpenRead(itemNameAffixesFilePath))
            {
                itemAffixesArray = await JsonSerializer.DeserializeAsync<ItemModel[]>(stream);
            }

            if (itemAffixesArray is null || itemAffixesArray.Length == 0)
            {
                throw new Exception("Items not found");
            }

            foreach (var item in itemAffixesArray)
            {
                if (ItemNameAffixesMap.ContainsKey(item.Key))
                {
                    item.EnUS = ItemNameAffixesMap[item.Key];
                }
            }

            var json = JsonSerializer.Serialize(itemAffixesArray, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
            });

            await File.WriteAllTextAsync(itemNameAffixesFilePath, json, new UTF8Encoding(true));
        }
    }
}
