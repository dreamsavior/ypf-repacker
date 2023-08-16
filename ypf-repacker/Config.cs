﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Ypf_Manager
{
    class Config
    {

        //
        // Enum(s)
        //

        public enum OperationMode
        {
            CreateArchive = 0,
            ExtractArchive = 1,
            PrintArchiveInfo = 2,
            Help = 3
        }


        //
        // Variable(s)
        //

        public static String ExecutableLocation()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        }
        public OperationMode Mode { get; set; }

        public Boolean WaitForUserInputBeforeExit { get; set; }

        public Boolean SkipDataIntegrityValidationOnPrintInfo { get; set; }

        public Int32 EngineVersion { get; set; }

        public List<String> FilesToProcess { get; set; }

        public List<String> FoldersToProcess { get; set; }


        //
        // Constructor(s)
        //

        public Config()
        {
            //
            // Set initial values
            //

            Mode = OperationMode.Help;

            WaitForUserInputBeforeExit = false;

            SkipDataIntegrityValidationOnPrintInfo = false;

            EngineVersion = 0;

            FilesToProcess = new List<String>();
            FoldersToProcess = new List<String>();
        }


        //
        // Function(s)
        //
        static string FilterVersion(string input)
        {
            // Replace commas with periods and remove spaces
            string modifiedInput = input.Replace(",", ".").Replace(" ", "");

            return modifiedInput;
        }
        static int GetSecondPartOfVersion(string input)
        {
            input = FilterVersion(input);
            int secondPart;

            if (int.TryParse(input, out secondPart))
            {
                // If the input is already an integer, return it immediately
                return secondPart;
            }
            else
            {
                string[] parts = input.Split('.');
                if (parts.Length >= 2 && int.TryParse(parts[1], out secondPart))
                {
                    // If the input is a version string with at least two parts, return the second part
                    return secondPart;
                }
                else
                {
                    throw new ArgumentException("Invalid input format.");
                }
            }
        }

        // Scan args to set options
        public void Set(String[] args)
        {
            //
            // Scan arguments
            //

            for (int i = 0; i < args.Length; i++)
            {
                String currentArg = args[i];

                if (currentArg == "-c")
                {
                    // Create archive
                    Mode = OperationMode.CreateArchive;
                }
                else if (currentArg == "-e")
                {
                    // Extract archive
                    Mode = OperationMode.ExtractArchive;
                }
                else if (currentArg == "-p")
                {
                    // Print archive info
                    Mode = OperationMode.PrintArchiveInfo;
                }
                else if (currentArg == "-v")
                {
                    //
                    // Set engine version
                    //

                    // When -v is provided, the next argument is the engine version
                    i++;

                    if (i == args.Length)
                    {
                        // -v is the last argument and no version is provided
                        throw new Exception("Can't find engine version");
                    }

                    // Try parsing the provided engine version
                    EngineVersion = GetSecondPartOfVersion(args[i]);

                }
                else if (currentArg == "-w")
                {
                    // Wait for user input before exit
                    WaitForUserInputBeforeExit = true;
                }
                else if (currentArg == "-sdc")
                {
                    // Skip data check
                    SkipDataIntegrityValidationOnPrintInfo = true;
                }
                else if (currentArg.EndsWith(".ypf") && File.Exists(currentArg))
                {
                    // Detected file
                    FilesToProcess.Add(Path.GetFullPath(currentArg));
                }
                else if (Directory.Exists(currentArg))
                {
                    // Detected folder
                    FoldersToProcess.Add(Path.GetFullPath(currentArg));
                }
            }


            //
            // Detect edge cases
            //

            if ((Mode == OperationMode.ExtractArchive || Mode == OperationMode.PrintArchiveInfo) && FilesToProcess.Count == 0)
            {
                throw new Exception("Can't find files to process");
            }
            else if (Mode == OperationMode.CreateArchive && FoldersToProcess.Count == 0)
            {
                throw new Exception("Can't find folders to process");
            }
        }

    }
}
