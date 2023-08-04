using System;
using System.Collections.Generic;
using System.IO;

namespace CombinedLetters
{
    class Program
    {
        static void Main()
        {
            string inputFolder = "Input";
            string archiveFolder = "Archive";

            // Feature 1: Archive files from Input folder to Archive folder
            ArchiveFiles(inputFolder, archiveFolder);

            Console.WriteLine("All tasks completed.");
        }

        static void ArchiveFiles(string sourceFolder, string destinationFolder)
        {
            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine($"Source folder '{sourceFolder}' not found.");
                return;
            }

            // Get all files in the source folder with extension .txt
            var files = Directory.GetFiles(sourceFolder, "*.txt", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                // to get the relative path of the file from the source folder
                var relativePath = Path.GetRelativePath(sourceFolder, file);

                // Creating corresponding subdirectory in the destination folder
                var destinationSubFolder = Path.GetDirectoryName(Path.Combine(destinationFolder, relativePath));
                Directory.CreateDirectory(destinationSubFolder);

                // Moving files to destination folder
                var destinationFilePath = Path.Combine(destinationFolder, relativePath);
                File.Move(file, destinationFilePath);

                Console.WriteLine($"Archived: {file} -> {destinationFilePath}");
            }

            Console.WriteLine("All files have been archived.");
        }

    }
}
