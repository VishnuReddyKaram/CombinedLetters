using System.IO;

namespace CombinedLetters
{
    public class LetterService : ILetterService
    {
        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile)
        {
            string content1 = File.ReadAllText(inputFile1);
            string content2 = File.ReadAllText(inputFile2);
             string mergedContent = $"{content1}\n{content2}";

        // Write the merged content to the output file
        File.WriteAllText(resultFile, mergedContent);
        }
    }
}
