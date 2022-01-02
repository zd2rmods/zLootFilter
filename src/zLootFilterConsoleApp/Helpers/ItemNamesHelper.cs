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
    internal static class ItemNamesHelper
    {
        private static readonly IDictionary<string, string> ItemNamesMap;

        static ItemNamesHelper()
        {
            var json = FilesHelper.ReadResourceAsString("ItemNames.json");
            ItemNamesMap = JsonSerializer.Deserialize<Dictionary<string, string>>(json, new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip
            });
        }

        public static async Task ProcessItemNamesFile(string itemNamesFilePath)
        {
            ItemModel[] itemsArray;
            using (var stream = File.OpenRead(itemNamesFilePath))
            {
                itemsArray = await JsonSerializer.DeserializeAsync<ItemModel[]>(stream);
            }

            if (itemsArray is null || itemsArray.Length == 0)
            {
                throw new Exception("Items not found");
            }

            foreach(var item in itemsArray)
            {
                if (ItemNamesMap.ContainsKey(item.Key))
                {
                    item.EnUS = ItemNamesMap[item.Key];
                }
            }

            var json = JsonSerializer.Serialize(itemsArray, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
            });

            await File.WriteAllTextAsync(itemNamesFilePath, json, new UTF8Encoding(true));
        }
    }
}
