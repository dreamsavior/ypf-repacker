using System;
using System.IO;

namespace Ypf_Manager
{
    class Program
    {

        //
        // Function(s)
        //
        static string RemoveTrailingSlashes(string path)
        {
            // Remove trailing forward slashes
            path = path.TrimEnd('/');

            // Remove trailing backslashes
            path = path.TrimEnd('\\');

            return path;
        }

        static void Main(string[] args)
        {
            // Set variables
            Log log = new Log();
            Config config = new Config();

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            // Header
            Console.WriteLine("YPF-Repacker v" + version);
            Console.WriteLine("Based on YPF-Manager");
            Console.WriteLine("==============================================");
            Console.WriteLine();

            try
            {
                // Scan args
                config.Set(args);

                // Process current operation mode
                switch (config.Mode)
                {
                    case Config.OperationMode.CreateArchive:

                        foreach (String f in config.FoldersToProcess)
                        {
                            string path = RemoveTrailingSlashes(f);
                            YPFArchive.Create(path, $"{path}.ypf", config.EngineVersion);
                        }
                        break;

                    case Config.OperationMode.ExtractArchive:

                        foreach (String f in config.FilesToProcess)
                        {
                            YPFArchive.Extract(f, $@"{Path.GetDirectoryName(f)}\{Path.GetFileNameWithoutExtension(f)}");
                        }
                        break;

                    case Config.OperationMode.PrintArchiveInfo:

                        foreach (String f in config.FilesToProcess)
                        {
                            YPFArchive.PrintInfo(f, config.SkipDataIntegrityValidationOnPrintInfo);
                        }
                        break;

                    case Config.OperationMode.Help:

                        Console.WriteLine("[DESCRIPTION]");
                        Console.WriteLine("Manage your YPF archives with this tool.");
                        Console.WriteLine();

                        Console.WriteLine("[USAGE]");
                        Console.WriteLine("Create archive:\t\t-c <folders_list> -v <version> [options]");
                        Console.WriteLine("Extract archive:\t-e <files_list> [options]");
                        Console.WriteLine("Print info:\t\t-p <files_list> [options]");
                        Console.WriteLine();

                        Console.WriteLine("[OPTIONS]");
                        Console.WriteLine("\t-c <folders_list>\tSet create archive mode");
                        Console.WriteLine("\t-e <files_list>\t\tSet extract archive mode");
                        Console.WriteLine("\t-p <files_list>\t\tSet print archive info mode");
                        Console.WriteLine("\t-v <version>\t\tSet the YU-RIS engine target version of the archive file");
                        Console.WriteLine("\t-w\t\t\tWait for user input before exit");
                        Console.WriteLine("\t-sdc\t\t\tSkip data integrity validation (Print info only)");
                        Console.WriteLine();

                        Console.WriteLine("[EXAMPLE]");
                        Console.WriteLine("Unpacking:");
                        Console.WriteLine("\t" + System.AppDomain.CurrentDomain.FriendlyName + " -e C:\\Some\\YU-RIS\\pac\\ysbin.ypf");
                        Console.WriteLine();

                        Console.WriteLine("Repacking:");
                        Console.WriteLine("\t" + System.AppDomain.CurrentDomain.FriendlyName + " -c C:\\Some\\YU-RIS\\folder -v 0.479");
                        Console.WriteLine();

                        break;
                }
            }
            catch (Exception ex)
            {
                // Save error to log
                log.Add(ex.Message);
                log.Save();

                // Print error to console
                Console.WriteLine(ex.Message);
            }

            // Wait for user input if -w argument is provided
            if (config.WaitForUserInputBeforeExit)
            {
                Console.WriteLine();
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }

    }
}
