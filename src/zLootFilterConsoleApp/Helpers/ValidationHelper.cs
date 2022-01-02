using System;
using System.IO;
using zLootFilterConsoleApp.Common;

namespace zLootFilterConsoleApp.Helpers
{
    internal static class ValidationHelper
    {
        public static void ValidateDirectory(string directoryPath, bool createIfNotExists = false)
        {
            if (String.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentNullException(nameof(directoryPath));
            }

            var directoryExists = Directory.Exists(directoryPath);

            if (!directoryExists && createIfNotExists)
            {
                Directory.CreateDirectory(directoryPath);
            }
            else if (!directoryExists)
            {
                Console.WriteLine($"[{directoryPath}] directory does not exists!");
                Environment.Exit(ExitCodes.DirectoryDoesNotExists);
            }
        }

        public static void ValidateFile(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[{filePath}] file does not exists!");
                Environment.Exit(ExitCodes.FileDoesNotExists);
            }
        }
    }
}
