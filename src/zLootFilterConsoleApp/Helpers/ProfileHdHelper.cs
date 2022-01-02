using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace zLootFilterConsoleApp.Helpers
{
    internal static class ProfileHdHelper
    {
        public static async Task ProcessProfileHdFile(string profileHdFilePath)
        {
            var originalLines = await File.ReadAllLinesAsync(profileHdFilePath, Encoding.UTF8);

            if (originalLines is null || originalLines.Length == 0)
            {
                throw new Exception("File is empty");
            }

            var indexTextColorPresets = Array.FindIndex(originalLines, x => !String.IsNullOrWhiteSpace(x) && x.EndsWith("// Text color presets"));
            if (indexTextColorPresets == -1)
            {
                throw new Exception("Text color presets line not found");
            }

            var lines = originalLines.ToList();

            // Insert custom colours
            lines.Insert(indexTextColorPresets - 1, String.Empty);
            lines.Insert(indexTextColorPresets, "    \"FontColorCustomSocketed\": {\"r\": 64, \"g\": 224, \"b\": 208, \"a\": 255 },");
            lines.Insert(indexTextColorPresets + 1, "    \"FontColorCustomEthereal\": {\"r\": 102, \"g\": 66, \"b\": 77, \"a\": 255 },");
            lines.Insert(indexTextColorPresets + 2, "    \"FontColorCustomGold\": {\"r\": 153, \"g\": 101, \"b\": 21, \"a\": 255 },");

            // Find and replaces colours for socketed/ethereal items to custom
            lines.ReplaceColor("SocketedColor", "FontColorCustomSocketed");
            lines.ReplaceColor("EtherealColor", "FontColorCustomEthereal");
            lines.ReplaceColor("UniqueColor", "FontColorCustomGold");
            lines.ReplaceColor("GoldColor", "FontColorGrey");

            await File.WriteAllLinesAsync(profileHdFilePath, lines, new UTF8Encoding(true));
        }

        private static void ReplaceColor(this List<string> lines, string colorField, string newColor)
        {
            var regex = new Regex($"\\\"{colorField}\\\":\\s\\\".+\\\",$", RegexOptions.Compiled);
            var index = lines.FindIndex(x => !String.IsNullOrWhiteSpace(x) && regex.IsMatch(x));

            if (index == -1)
            {
                throw new Exception($"[{colorField}] not found");
            }

            lines[index] = $"        \"{colorField}\": \"${newColor}\",";
        }
    }
}
