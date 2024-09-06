namespace TestingGround
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string defaultStartPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string customStartPath = "Custom start path is not set";
            string defaultEndPath = Path.Combine(defaultStartPath, "movedItems");
            string customEndPath = "Custom end path is not set";
            int switchPoints = 0;

            try
            {
                if (!Directory.Exists(defaultEndPath))
                {
                    Directory.CreateDirectory(defaultEndPath);
                }
            }
            catch (Exception createDir)
            {
                Console.WriteLine("Failed to create default directory: " + createDir.Message);
                return;
            }

            try
            {
                Console.WriteLine("Enter the path you wish to move from (Press enter for default)");
                Console.WriteLine($"Default path is '{defaultStartPath}'");
                string? input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    customStartPath = Path.GetFullPath(input.Trim());
                    if (!Directory.Exists(customStartPath))
                    {
                        Console.WriteLine("Source directory does not exist.");
                        return;
                    }
                    switchPoints += 1;
                }

                Console.WriteLine("Enter the path you wish to move into (Press enter for default)");
                Console.WriteLine($"Default path is '{defaultEndPath}'");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    customEndPath = Path.GetFullPath(input.Trim());
                    if (!Directory.Exists(customEndPath))
                    {
                        Directory.CreateDirectory(customEndPath);
                    }
                    switchPoints += 2;
                }

                string sourcePath = switchPoints == 1 || switchPoints == 3 ? customStartPath : defaultStartPath;
                string destinationPath = switchPoints == 2 || switchPoints == 3 ? customEndPath : defaultEndPath;

                if (sourcePath.Equals(destinationPath, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Source and destination paths cannot be the same.");
                    return;
                }

                DirectoryInfo sourceDirectory = new DirectoryInfo(sourcePath);
                FileInfo[] files = sourceDirectory.GetFiles();
                DirectoryInfo[] directories = sourceDirectory.GetDirectories();

                foreach (FileInfo file in files)
                {
                    string destFile = Path.Combine(destinationPath, file.Name);

                    if (File.Exists(destFile))
                    {
                        Console.WriteLine($"File '{file.Name}' already exists at destination.");
                        continue;
                    }

                    file.MoveTo(destFile);
                }

                foreach (DirectoryInfo dir in directories)
                {
                    string destDir = Path.Combine(destinationPath, dir.Name);

                    if (dir.FullName.Equals(destinationPath, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (Directory.Exists(destDir))
                    {
                        Console.WriteLine($"Directory '{dir.Name}' already exists at destination.");
                        continue;
                    }

                    dir.MoveTo(destDir);
                }

                Console.WriteLine("Files and directories moved successfully.");
            }
            catch (Exception moveFiles)
            {
                Console.WriteLine("Failed to move files: " + moveFiles.Message);
            }

            Console.WriteLine("Process finished, press any key to exit...");
            Console.ReadKey();
        }
    }
}
