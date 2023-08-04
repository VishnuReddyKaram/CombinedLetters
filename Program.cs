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
            string outputFolder = "Output";

            List<string> combinedUniversityIds = new List<string>();
            ProcessLetters(inputFolder, outputFolder, combinedUniversityIds);

            GenerateReport(outputFolder, combinedUniversityIds);
            // Feature 1: Archive files from Input folder to Archive folder
            ArchiveFiles(inputFolder, archiveFolder);

            Console.WriteLine("task completed.");
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
        // processesing all the matching letters in whic has same University ID in the input folder and combines the admission and scholarship letters.
        public static void ProcessLetters(string inputFolder, string outputFolder, List<string> combinedUniversityIds)
        {
            // Processing in the "Admission" department.
            ProcessLettersForDepartment(inputFolder, outputFolder, "Admission", combinedUniversityIds);

            // Processing in the "Scholarship" department.
            ProcessLettersForDepartment(inputFolder, outputFolder, "Scholarship", combinedUniversityIds);
        }

        // to processes all the letters in the input folder for a given department and combines the admission and scholarship letters for each university ID and move to Output.
        public static void ProcessLettersForDepartment(string inputFolder, string outputFolder, string department, List<string> combinedUniversityIds)
        {
            // Getting the directory of department.
            string departmentFolder = Path.Combine(inputFolder, department);

            // Geting all the dated folders.
            string[] datedFolders = Directory.GetDirectories(departmentFolder);

            // Iterating over the dated folders and process the admission and scholarship letters for each folder.
            foreach (string datedFolder in datedFolders)
            {
                // Get the admission and scholarship files for the dated folder.
                string[] admissionFiles = Directory.GetFiles(datedFolder, "admission-*.txt");
                string[] scholarshipFiles = Directory.GetFiles(Path.Combine(inputFolder, "Scholarship", Path.GetFileName(datedFolder)), "scholarship-*.txt");
                
                // Iterate over the admission files and combine them with the scholarship files.
                foreach (string admissionFile in admissionFiles)
                {
                    string universityId = Path.GetFileNameWithoutExtension(admissionFile).Substring("admission-".Length);
                    string scholarshipFile = scholarshipFiles.FirstOrDefault(file => file.Contains(universityId));
                    if (scholarshipFile != null)
                    {
                        // Combine the letters using the LetterService.
                        string resultFileName = $"combined_{universityId}.txt";
                        string resultFilePath = Path.Combine(outputFolder, resultFileName);

                        // Using the LetterService to combine the admission and scholarship letters.
                        LetterService letterService = new LetterService();
                        letterService.CombineTwoLetters(admissionFile, scholarshipFile, resultFilePath);

                        // Adding the combined university ID to the list.
                        combinedUniversityIds.Add(universityId);
                    }
                    else
                    {
                        Console.WriteLine($"No matching Scholarship and Admission is found for University ID: {universityId}");
                    }
                }
            }

            Console.WriteLine($"Processing to output folder completed.");
        }
       // to generate the report of combined letters.
    private static void GenerateReport(string outputFolder, List<string> combinedUniversityIds)
        {
            string reportFileName = "Report.txt";
            string reportFilePath = Path.Combine(outputFolder, reportFileName);

            using (StreamWriter writer = new StreamWriter(reportFilePath))
            {
                writer.WriteLine($"{DateTime.Now:d} Report");
                writer.WriteLine("--------------------------------------");
                writer.WriteLine($"Number of Combined Letters : {combinedUniversityIds.Count}");
                foreach (string universityId in combinedUniversityIds)
                {
                    writer.WriteLine(universityId);
                }
            }

            Console.WriteLine("Report generated successfully.");
        }
    }
}
