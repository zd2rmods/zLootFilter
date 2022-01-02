using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace zLootFilterConsoleApp.Helpers
{
    internal static class FilesHelper
    {
        private static byte[] ReadResourceAsBytes(String filename)
        {
            var resourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Resources/{filename}");

            ValidationHelper.ValidateFile(resourceFilePath);

            return File.ReadAllBytes(resourceFilePath);
        }

        public static string ReadResourceAsString(string resourceFilename)
        {
            var bytes = ReadResourceAsBytes(resourceFilename);

            return Encoding.UTF8.GetString(bytes);
        }

        public static void CreateFileFromResourceFile(string resourceFilename, string destinationDirectoryPath)
        {
            var bytes = ReadResourceAsBytes(resourceFilename);

            var destinationFilePath = Path.Combine(destinationDirectoryPath, resourceFilename);

            if (File.Exists(destinationFilePath))
            {
                File.Delete(destinationFilePath);
                Console.WriteLine($"File [{destinationFilePath}] deleted");
            }

            File.WriteAllBytes(destinationFilePath, bytes);

            ValidationHelper.ValidateFile(destinationFilePath);
        }

        public static void RestoreToOriginal(string sourceDirectoryPath, string destinationDirectoryPath, string filename)
        {
            ValidationHelper.ValidateDirectory(sourceDirectoryPath);
            ValidationHelper.ValidateDirectory(destinationDirectoryPath);

            if (String.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            var sourceFilePath = Path.Combine(sourceDirectoryPath, filename);
            ValidationHelper.ValidateFile(sourceFilePath);

            var destinationFilePath = Path.Combine(destinationDirectoryPath, filename);

            if (File.Exists(destinationFilePath))
            {
                File.Delete(destinationFilePath);
                Console.WriteLine($"File [{destinationFilePath}] deleted");
            }

            File.Copy(sourceFilePath, destinationFilePath, false);

            ValidationHelper.ValidateFile(destinationFilePath);
        }
    }
}
