﻿using System;
using System.IO;
using System.Text;

namespace Ypf_Manager
{
    class Log
    {

        //
        // Variable(s)
        //

        private readonly StringBuilder sb;


        //
        // Constructor(s)
        //

        public Log()
        {
            sb = new StringBuilder();
        }


        //
        // Function(s)
        //

        // Add a new message to the log
        public void Add(String message)
        {
            sb.Append(DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss] "));
            sb.Append(message);
            sb.Append('\n');
        }


        // Save the log into a file
        public void Save()
        {
            String outputFile = $@"{Config.ExecutableLocation()}\log.txt";

            File.AppendAllText(outputFile, sb.ToString(), new UTF8Encoding(false));
        }

    }
}
