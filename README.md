## Combined Letters Console App
This is a console application designed to process admission and scholarship letters, combine them if the University IDs match, and move the combined letters to the Output folder and give a report for all the Matching ID's and move all the files to arcive folder

## Testing the Application
First, create sample admission and scholarship letter files in the "Input" folder. Use the naming convention admission-123456.txt and scholarship-123456.txt for each student's admission and scholarship letters, respectively.
Build and Run the app
After execution, check the "Output" folder. The app should have processed the admission and scholarship letters for students with matching University IDs. we can find combined letter files named as combined_<universityId>.txt.
In the "Output" folder, we also see a file named Report.txt. it contains a list of all the combined University IDs.
The "Archive" folder. All input files (admission and scholarship letters)have been moved there.

## Testing the Console App

During the manual testing, I executed the application multiple times under various scenarios to ensure its proper functionality. Firstly, I tested the archive feature to confirm that files from the Input folder were successfully moved to the Archive folder and I ran the app with a mix of admission and scholarship letters in the Input folder to verify if it correctly identifies matching University IDs and combines the letters into the Output folder. Next, I tested the app with different file formats and sizes to check for any unexpected behavior or errors. Lastly, I verified the report generation by checking the output report file to ensure that it contains the expected list of combined University IDs.

In addition to manual testing, I created a comprehensive set of unit test cases using NUnit and Moq. These unit tests cover different methods and functionalities of the app, including the CombineTwoLetters method, the ProcessLettersForDepartment method, and the ArchiveFiles method. By using unit tests, I was able to test the individual components of the app in isolation, making it easier to identify and fix any potential bugs. Overall, the combination of manual testing and unit testing helped me ensure the reliability and correctness of the Console app under various scenarios.

# Questions to Consider

If the Console app run before the scheduled time, it will process the files as usual. However, since it's not the scheduled time, there might not be any new files to process.
If the Console app run after the scheduled time, it will process the files for that day. However, it might miss processing files that were added after the scheduled time, but before the manual run.

If the Console app wasn't run the previous day, it means that the files for the previous day are still in the Input folder.
When the Console app runs on the current day, it will process both the files from the previous day and the current day, combining the matching admission and scholarship letters and moving them to the Output folder. However, it will still archive all the files, including those from the previous day.

## Hours Spent:

During the implementation of the Combined Letters Console app, I estimated that it would take around 3 hours to complete. However, the actual time spent on the implementation turned out to be 4 hours. The additional hour was primarily on Testing and Debugging While writing the code, I encountered a few issues with file handling and combining letters. This required some additional time for testing and debugging to ensure that the application worked as expected and Writing Unit Tests and to ensure the reliability and correctness of the application, I added unit tests using NUnit and Moq. Writing and verifying the test cases took some additional time to cover various scenarios and edge cases.

## Comments 
While Implementing I assumed that the input files would always follow the naming convention "admission-.txt" and "scholarship-.txt" for admission and scholarship letters, respectively.
Problems encountered during development are the initial implementation had an issue with moving files to the archive. I corrected it by modifying the ArchiveFiles method to use System.IO.Directory directly instead of using IDirectoryWrapper.
I faced an issue with missing package references (Moq and NUnit) in the test project. I ensured that the required package references were added to the test project, and the test cases were written correctly to verify the functionalities

