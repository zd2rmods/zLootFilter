using System;
using System.IO;
using zLootFilterConsoleApp.Common;
using zLootFilterConsoleApp.Helpers;

const string StringsSubDirectoryPath = @"data/local/lng/strings";
const string LayoutsSubDirectoryPath = @"data/global/ui/layouts";
const string ModInfoFilename = "ModInfo.json";
const string ItemNamesFilename = @"item-names.json";
const string ItemNameAffixesFilename = @"item-nameaffixes.json";
const string ItemRunesFilename = @"item-runes.json";
const string ProfileHdFilename = @"_profilehd.json";

#region Arguments Validation

if (args is null || args.Length != 2)
{
    Console.WriteLine("Invalid application arguments");
    Environment.Exit(ExitCodes.InvalidApplicationArguments);
}

var originalDirectoryPath = args[0];
ValidationHelper.ValidateDirectory(originalDirectoryPath);

var modDirectoryPath = args[1];
ValidationHelper.ValidateDirectory(modDirectoryPath);

#endregion

#region Preparation

// Strings directory
var originalStringsDirectoryPath = Path.Combine(originalDirectoryPath, StringsSubDirectoryPath);
ValidationHelper.ValidateDirectory(originalStringsDirectoryPath);

var modStringsDirectoryPath = Path.Combine(modDirectoryPath, StringsSubDirectoryPath);
ValidationHelper.ValidateDirectory(modStringsDirectoryPath, true);

// Layouts directory
var originalLayoutsDirectoryPath = Path.Combine(originalDirectoryPath, LayoutsSubDirectoryPath);
ValidationHelper.ValidateDirectory(originalLayoutsDirectoryPath);

var modLayoutDirectoryPath = Path.Combine(modDirectoryPath, LayoutsSubDirectoryPath);
ValidationHelper.ValidateDirectory(modLayoutDirectoryPath, true);

// Restore original files
FilesHelper.CreateFileFromResourceFile(ModInfoFilename, modDirectoryPath);
FilesHelper.RestoreToOriginal(originalStringsDirectoryPath, modStringsDirectoryPath, ItemNamesFilename);
FilesHelper.RestoreToOriginal(originalStringsDirectoryPath, modStringsDirectoryPath, ItemNameAffixesFilename);
FilesHelper.RestoreToOriginal(originalStringsDirectoryPath, modStringsDirectoryPath, ItemRunesFilename);
FilesHelper.RestoreToOriginal(originalLayoutsDirectoryPath, modLayoutDirectoryPath, ProfileHdFilename);

// Build file paths
var itemNamesFilePath = Path.Combine(modStringsDirectoryPath, ItemNamesFilename);
ValidationHelper.ValidateFile(itemNamesFilePath);

var itemNameAffixesFilePath = Path.Combine(modStringsDirectoryPath, ItemNameAffixesFilename);
ValidationHelper.ValidateFile(itemNameAffixesFilePath);

var itemRunesFilePath = Path.Combine(modStringsDirectoryPath, ItemRunesFilename);
ValidationHelper.ValidateFile(itemRunesFilePath);

var profileHdFilePath = Path.Combine(modLayoutDirectoryPath, ProfileHdFilename);
ValidationHelper.ValidateFile(profileHdFilePath);

#endregion

#region Processing logic

await ItemNamesHelper.ProcessItemNamesFile(itemNamesFilePath);
await ItemNameAffixesHelper.ProcessItemNameAffixesFile(itemNameAffixesFilePath);
await RunesHelper.ProcessRunesFile(itemRunesFilePath);
await ProfileHdHelper.ProcessProfileHdFile(profileHdFilePath);

#endregion
