using System.Runtime.CompilerServices;

namespace TestingGround
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string defaultStartPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string customStartPath = "Custom start path is not set";
            string defaultEndPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "movedItems");
            string customEndPath = "Custom end path is not set";
            int switchPoints = 0;
            try
            {
                Directory.CreateDirectory(defaultEndPath);
            }
            catch (Exception createDir)
            {
                Console.WriteLine("Failed to create default directory, " + createDir);
            }

            try
            {
                Console.WriteLine("Enter the path you wish to move from (Press enter for default)");
                Console.WriteLine("Default path is '" + defaultStartPath + "'");
                string? input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    customStartPath = input;
                    switchPoints = switchPoints + 1;
                }

                Console.WriteLine("Enter the path you wish to move into (Press enter for default)");
                Console.WriteLine("Default path is '" + defaultEndPath + "'");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    customEndPath = input;
                    switchPoints = switchPoints + 2;
                }

                string sourcePath = switchPoints == 1 || switchPoints == 3 ? customStartPath : defaultStartPath;
                string destinationPath = switchPoints == 2 || switchPoints == 3 ? customEndPath : defaultEndPath;

                string[] files = Directory.GetFiles(sourcePath);

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    string destPath = Path.Combine(destinationPath, fileName);
                    File.Move(file, destPath);
                }

                Console.WriteLine("Files moved successfully.");
                Console.WriteLine("Process finished, press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception moveFiles)
            {
                Console.WriteLine("Failed to move files, " + moveFiles.Message);
            }
        }
    }
}
